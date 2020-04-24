using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Normal,
    Division,
    Head
}

public class PlayerMove : MonoBehaviour
{
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

    //生成するもの
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

    //分裂体の初期値保管
    public Vector3 headPosition;

    // Start is called before the first frame update
    void Start()
    {
        rigidPlayer = GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        jumpFlag = false;
        awaCreate = false;

        playerHeadCollider = _playerHead.GetComponent<CircleCollider2D>();
        playerHead = _playerHead.GetComponent<PlayerHeadMove>();
        playerHeadRigidbody = _playerHead.GetComponent<Rigidbody2D>();
        headPosition = _playerHead.transform.localPosition;

        SetCurrentState(PlayerState.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        OnPlayerStateChanged(currentPlayerState);
        if(Input.GetButtonDown("Y_BUTTON") && currentPlayerState != PlayerState.Head)
        {
            if (currentPlayerState != PlayerState.Normal)
            {
                SetCurrentState(PlayerState.Normal);
            }
            else if(currentPlayerState != PlayerState.Division)
            {
                SetCurrentState(PlayerState.Division);
            }
        }
    }

    // 状態が変わったら何をするか
    void OnPlayerStateChanged(PlayerState state)
    {
        switch (state)
        {
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
        }
    }

    //分裂後の頭？のみの処理予定
    void HeadMove()
    {
        //if (Input.GetButtonDown("Y_BUTTON"))
        //{
        //    SetCurrentState(PlayerState.Division);
        //}
    }

    void Jump()//ジャンプ系
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("A_BUTTON") && jumpFlag == false)//ジャンプボタンを押してなおかつジャンプ中じゃないとき
        {
            //rigidPlayer.velocity = Vector2.zero;
            rigidPlayer.AddForce(Vector2.up * jumpForce);
            jumpFlag = true;
        }
    }
    void Baburu()
    {
        //マウス入力で左クリックしたとき
        if (Input.GetButtonDown("B_BUTTON"))
        {
            awaCreate = true;
            //BaburuPosition = transform.position;
            //BaburuPosition.z = 10f;
            stage = (GameObject)Instantiate(foam, transform.position, Quaternion.identity, stageParent);
        }
    }
    void Move()//移動系
    {
        float h = Input.GetAxis("Horizontal");
        rigidPlayer.velocity = new Vector2(speed * h, rigidPlayer.velocity.y);
        //Debug.Log(h);
        if (Input.GetKey(KeyCode.LeftArrow) && transform.localScale.x > 0 || Input.GetKey(KeyCode.RightArrow) && transform.localScale.x < 0)
        {
            Vector2 pos = transform.localScale;
            pos.x *= -1;
            transform.localScale = pos;

        }


    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Stage"))
        {
            jumpFlag = false;
        }

        if(col.gameObject.CompareTag("PlayerHead"))
        {
            SetCurrentState(PlayerState.Division);
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
    }
}
