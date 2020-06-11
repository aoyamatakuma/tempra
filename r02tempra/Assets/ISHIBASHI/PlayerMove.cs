using System.Collections;
using UnityEngine;

public enum PlayerState
{
    Stop,
    Normal,
    Division,
    Head,
    Goal,
    Warp
}

public class PlayerMove : MonoBehaviour
{
    public AudioClip jumpSE;
    public AudioClip shutterSE;
    public AudioClip bubbleSE;
    private AudioSource jump;
    private AudioSource shutter;
    private AudioSource bubble;
    //現在の状態
  public PlayerState currentPlayerState;
    //１つ前の状態
    private PlayerState previousPlayerState;

    //追加　小野
    public GameObject HaretuEffect;
    //public GameObject HaretuEffect2;


    Rigidbody2D rigidPlayer;//物理演算
    public float jumpForce = 250.0f;//ジャンプの力
    public float speed = 2.0f;//地上での移動速度
    bool jumpFlag;
    SpriteRenderer sprite;

    //生成するものと数
    public int foamCount;
    public int babulimit;
    public GameObject foam;
    //バブル座標
    private Vector3 BaburuPosition;

    [HideInInspector]
    public bool awaCreate;

    //泡の生成場所
    GameObject stage;
    [SerializeField] Transform stageParent;

    //プレイヤーの子取得
    [SerializeField] private Transform _playerHead;
    CircleCollider2D playerHeadCollider;
    Rigidbody2D playerHeadRigidbody;
    [SerializeField]
    private PlayerHeadMove playerHead;

    private bool cameraCheck;
    public bool isYcheck;

    //分裂体の初期値保管
    [SerializeField]
    private Vector3 headPosition;

    [SerializeField]
    private StageRule rule;

    Animator playerAnime;
    Animator headAnime;


    private bool headScale = false;
    private Vector3 headvec;
    private GameRule gameRule;
    //ワープフラグ
    public float warpangle = 10;
    public bool warpflag;
    public GameObject effectPrefab;
    public bool warpscale;
    private int warpcount;
    
    // Start is called before the first frame update
    void Start()
    {
        jump = gameObject.GetComponent<AudioSource>();
        shutter = gameObject.GetComponent<AudioSource>();
        bubble = gameObject.GetComponent<AudioSource>();
        rigidPlayer = GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        jumpFlag = false;
        awaCreate = false;
        warpflag = false; //ワープフラグ
        playerHeadCollider = _playerHead.GetComponent<CircleCollider2D>();
        playerHead = _playerHead.GetComponent<PlayerHeadMove>();
        playerHeadRigidbody = _playerHead.GetComponent<Rigidbody2D>();

        playerAnime = gameObject.GetComponent<Animator>();
        headAnime = _playerHead.GetComponent<Animator>();

        gameRule = GameObject.Find("StageManager").GetComponent<GameRule>();

        headPosition = _playerHead.transform.localPosition;
        headvec = _playerHead.transform.localScale;

        cameraCheck = false;
        isYcheck = false;

        SetCurrentState(PlayerState.Stop);

      
       
    }

    // Update is called once per frame
    void Update()
    {
      
        OnPlayerStateChanged(currentPlayerState);
        if(Input.GetButtonDown("Y_BUTTON") && currentPlayerState != PlayerState.Head && currentPlayerState != PlayerState.Stop && currentPlayerState != PlayerState.Warp)
        {
            shutter.clip = shutterSE;
            shutter.Play();

            if (currentPlayerState != PlayerState.Normal)
            {
                SetCurrentState(PlayerState.Normal);               
            }
            else if(currentPlayerState != PlayerState.Division)
            {
                SetCurrentState(PlayerState.Division);
            }
        }

        if(headScale && headvec.y < _playerHead.localScale.y)
        {
            _playerHead.transform.localScale -= new Vector3(0.02f, 0.02f, 0);
        }
        if (currentPlayerState != PlayerState.Head)
        {
            Pause();
        }
    }

   

    // 状態が変わったら何をするか
    void OnPlayerStateChanged(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Stop:
                StopMove();
                break;
            case PlayerState.Normal:
                NormalMove();
                break;
            case PlayerState.Division:
                DivisionMove();
                break;
            case PlayerState.Head:
                HeadMove();
                break;
            case PlayerState.Goal:
                GoalMove();
                break;
            case PlayerState.Warp:
                WarpMove();
                break;
            default:
                break;
        }
    }

    // 外からこのメソッドを使って状態を変更
    public void SetCurrentState(PlayerState state)
    {
        currentPlayerState = state;
        OnPlayerStateChanged(currentPlayerState);
    }

    void GoalMove()
    {

    }
    //ワープフラグ
    void WarpMove()
    {
        WarpFlag();
    }

    void StopMove()
    {
        playerAnime.SetBool("Move", false);
    }

    //通常状態の処理
    void NormalMove()
    {
        warpflag = false;//ワープ演出回転
        Jump();
        Move();
        if (rule.current_bubble < rule.limit_bubble && !awaCreate)
        {
            Baburu();
        }
        if (warpcount != 0)
        {
            WarpFlag();
        }
    }

    //ズームアウト時の処理
    void DivisionMove()
    {
        playerAnime.SetBool("Move", false);
        if (Input.GetButtonDown("B_BUTTON"))
        {
            SetCurrentState(PlayerState.Head);
            playerHeadCollider.enabled = true;
            playerHeadRigidbody.simulated = true;
            playerHead.enabled = true;
            headAnime.SetBool("Stop", true);
            playerHead.StartCoroutine("Coroutine");
            Invoke("rotationStop", 1f);
        }
    }

    //分裂後の頭？のみの処理予定
    void HeadMove()
    {
        if (rule.current_bubble < rule.limit_bubble  && !isYcheck && !awaCreate)
        {
            Baburu();
        }

        if (Input.GetButtonDown("Y_BUTTON") && !isYcheck)
        {
            CameraCheck();
        }
    }

    void Jump()//ジャンプ系
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("A_BUTTON") && jumpFlag == false)//ジャンプボタンを押してなおかつジャンプ中じゃないとき
        {
            jump.clip = jumpSE;
            jump.Play();
            rigidPlayer.AddForce(Vector2.up * jumpForce);
            jumpFlag = true;
        }
    }
    void Baburu()
    {
        //マウス入力で左クリックしたとき
        if (Input.GetButtonDown("X_BUTTON") && foamCount < babulimit && currentPlayerState == PlayerState.Normal && gameRule.getIsPlay())
        {
            bubble.clip = bubbleSE;
            bubble.Play();
            stage = (GameObject)Instantiate(foam, transform.position, Quaternion.identity,stageParent);
            foamCount++;
            rule.ListBubble(stage);
        }

        //マウス入力で左クリックしたとき
        if (Input.GetButtonDown("X_BUTTON") && foamCount < babulimit && currentPlayerState == PlayerState.Head && gameRule.getIsPlay())
        {
            bubble.clip = bubbleSE;
            bubble.Play();
            stage = (GameObject)Instantiate(foam, _playerHead.transform.position, Quaternion.identity, _playerHead.parent);
            foamCount++;
            rule.ListBubble(stage);
        }

    }
    void Move()//移動系
    {
        float h = Input.GetAxis("Horizontal");
        rigidPlayer.velocity = new Vector2(speed * h, rigidPlayer.velocity.y);
        if (h > 0)
        {
            playerAnime.SetBool("Move", true);
            headAnime.SetBool("Stop", false);
            transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
        else if (h < 0)
        {
            playerAnime.SetBool("Move", true);
            headAnime.SetBool("Stop", false);
            transform.localRotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            playerAnime.SetBool("Move", false);
            headAnime.SetBool("Stop", true);
        }
        

    }
    void WarpFlag()
    {
        // transform.rotation *= Quaternion.AngleAxis(warpangle, Vector3.forward);
        if (warpflag == true)
        {
            //1秒間に回る
            transform.Rotate(new Vector3(0, 0, 1080) * Time.deltaTime, Space.World);
            PlayerWarpScale();
           // StartCoroutine("Warolocal");
        }
        //ワープフラグ
        if (warpflag == false)
        {
            awaCreate = false;
            transform.rotation=new Quaternion(0,0,0,0);
            transform.localScale = new Vector3(8.0f, 8.0f, 5.6752f);//追加
            rigidPlayer.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    void PlayerWarpScale()
    {
        if (warpcount == 1)
        {
            transform.localScale -= new Vector3(0.05f, 0.05f, 0);
        }
        if (warpcount == 2)
        {
            transform.localScale += new Vector3(0.05f, 0.05f, 0);
        }
    }
    void CameraCheck()
    {
        cameraCheck = !cameraCheck;
    }

    public void BubbleCount(int bubble)
    {
        foamCount = foamCount - bubble;
    }

    public void GetStage(StageRule rule)
    {
        this.rule = rule;
    }

    public Vector3 GetHeadPosition()
    {
        return headPosition;
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if(col.gameObject.CompareTag("PlayerHead"))
        {
            if (cameraCheck == false)
            {
                SetCurrentState(PlayerState.Division);
            }
            else if(cameraCheck == true)
            {
                CameraCheck();
                SetCurrentState(PlayerState.Normal);
            }

            StartCoroutine("Coroutine");
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("StageArea"))
        {
            stageParent = col.gameObject.transform.root;
            transform.parent = col.gameObject.transform.root;
        }

        if (col.gameObject.CompareTag("StageBox"))
        {
            GetStage(col.gameObject.GetComponent<StageRule>());
        }

        if (col.gameObject.CompareTag("Stage"))
        {
            jumpFlag = false;
        }
        //ワープフラグ
        if (col.gameObject.CompareTag("WarpPoint"))
        {
            warpflag = true;
            transform.position = col.gameObject.transform.position;
            rigidPlayer.constraints = RigidbodyConstraints2D.FreezePosition;
           
            // エフェクトを出す。（posでエフェクトの出現位置を調整する。）
            Vector3 pos = col.transform.position;
            GameObject effect = (GameObject)Instantiate(effectPrefab, new Vector3(pos.x, pos.y + 1, pos.z - 1), Quaternion.identity);

            // エフェクトを２秒後に消す。
            Destroy(effect, 1.0f);

        }
  
        if(col.gameObject.CompareTag("AwaCreate") )
        {
            awaCreate = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("AwaCreate"))
        {
            awaCreate = true;
        }
    }
    private IEnumerator Coroutine()
    {
        //処理１
        headScale = true;
        //１秒待機
        yield return new WaitForSeconds(1.0f);
        headScale = false;
        //コルーチンを終了
        yield break;
    }
    public IEnumerator Warolocal()
    {
     
        //処理１
         warpcount++;
        //１秒待機
        yield return new WaitForSeconds(1.0f);
        Debug.Log("nnnnn");
         warpcount++;
        yield return new WaitForSeconds(1.0f);
        warpcount = 0;
        //コルーチンを終了
        yield break;
        
    }
    
    private void rotationStop()
    {
        transform.localRotation = new Quaternion(0, 0, 0, 0);
    }

    private void Pause()
    {
        if (Input.GetKeyDown("joystick button 7") && currentPlayerState != PlayerState.Stop && currentPlayerState != PlayerState.Warp)
        {
            previousPlayerState = currentPlayerState;
            SetCurrentState(PlayerState.Stop);          
        }
        else if(Input.GetKeyDown("joystick button 7") && currentPlayerState == PlayerState.Stop)
        {
            SetCurrentState(previousPlayerState);
        }
    }
}
