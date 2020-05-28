using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Stop,
    Normal,
    Division,
    Head
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
    private PlayerState currentPlayerState;

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

    //分裂体の初期値保管
    [SerializeField]
    private Vector3 headPosition;

    [SerializeField]
    private StageRule rule;

    Animator playerAnime;
    Animator headAnime;

    //青山追加
    [SerializeField]
    private GameObject zoomInNaviPrefab;
    private GameObject zoomInNaviInstance;

    [SerializeField]
    private GameObject zoomOutNaviPrefab;
    private GameObject zoomOutNaviInstance;

   
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

        playerHeadCollider = _playerHead.GetComponent<CircleCollider2D>();
        playerHead = _playerHead.GetComponent<PlayerHeadMove>();
        playerHeadRigidbody = _playerHead.GetComponent<Rigidbody2D>();

        playerAnime = gameObject.GetComponent<Animator>();
        headAnime = _playerHead.GetComponent<Animator>();

        headPosition = _playerHead.transform.localPosition;

        cameraCheck = false;

        SetCurrentState(PlayerState.Stop);

        //Destroy(zoomOutNaviInstance);
        Destroy(zoomOutNaviInstance);
        Destroy(zoomInNaviInstance);
        zoomInNaviInstance = GameObject.Instantiate(zoomInNaviPrefab) as GameObject;
       
    }

    // Update is called once per frame
    void Update()
    {
        OnPlayerStateChanged(currentPlayerState);
        if(Input.GetButtonDown("Y_BUTTON") && currentPlayerState != PlayerState.Head)
        {
            shutter.clip = shutterSE;
            shutter.Play();

            if (currentPlayerState != PlayerState.Normal)
            {
                zoomInNaviInstance = GameObject.Instantiate(zoomInNaviPrefab) as GameObject;
                Destroy(zoomOutNaviInstance);

                SetCurrentState(PlayerState.Normal);
                
            }
            else if(currentPlayerState != PlayerState.Division)
            {
                zoomOutNaviInstance = GameObject.Instantiate(zoomOutNaviPrefab) as GameObject;
                Destroy(zoomInNaviInstance);

                SetCurrentState(PlayerState.Division);
            }
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

    void StopMove()
    {
        //Debug.Log("停止中");
    }

    //通常状態の処理
    void NormalMove()
    {
        Jump();
        Baburu();
        Move();

       
    }

    //ズームアウト時の処理
    void DivisionMove()
    {

        if (Input.GetButtonDown("B_BUTTON"))
        {
            SetCurrentState(PlayerState.Head);
            playerHeadCollider.enabled = true;
            playerHeadRigidbody.simulated = true;
            playerHead.enabled = true;
            headAnime.SetBool("Stop", true);
        }
    }

    //分裂後の頭？のみの処理予定
    void HeadMove()
    {
        Baburu();
        if (Input.GetButtonDown("Y_BUTTON"))
        {
            
            CameraCheck();
        }

        float h = Input.GetAxis("Horizontal");
        if (h != 0)
        {
            if (h > 0)
            {
                transform.localScale = new Vector3(5.6f, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-5.6f, transform.localScale.y, transform.localScale.z);
            }

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
        if (Input.GetButtonDown("X_BUTTON") && foamCount < babulimit && currentPlayerState == PlayerState.Normal)
        {
            bubble.clip = bubbleSE;
            bubble.Play();
            awaCreate = true;
            stage = (GameObject)Instantiate(foam, transform.position, Quaternion.identity,stageParent);
            foamCount++;
            rule.ListBubble(stage);
        }

        //マウス入力で左クリックしたとき
        if (Input.GetButtonDown("X_BUTTON") && foamCount < babulimit && currentPlayerState == PlayerState.Head)
        {
            bubble.clip = bubbleSE;
            bubble.Play();
            awaCreate = true;
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
            //rigidPlayer.velocity = new Vector2(speed * h, rigidPlayer.velocity.y);
            playerAnime.SetBool("Move", true);
            headAnime.SetBool("Stop", false);
            transform.localScale = new Vector3(5.6f, transform.localScale.y, transform.localScale.z);
        }
        else if (h < 0)
        {
            //rigidPlayer.velocity = new Vector2(speed * h, rigidPlayer.velocity.y);
            playerAnime.SetBool("Move", true);
            headAnime.SetBool("Stop", false);
            transform.localScale = new Vector3(-5.6f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            playerAnime.SetBool("Move", false);
            headAnime.SetBool("Stop", true);
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
            _playerHead.transform.localPosition = headPosition;
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
       
    }
  
}
