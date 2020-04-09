using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerao : MonoBehaviour

{
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
    [SerializeField]Transform stageParent;
    // Start is called before the first frame update
    void Start()
    {
        rigidPlayer = GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        jumpFlag = false;
        awaCreate = false;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Baburu();
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
            stage = (GameObject)Instantiate(foam,transform.position, Quaternion.identity,stageParent);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Stage"))
        {
            jumpFlag = false;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Abura"))
        {
            ModeChange();
        }

        if (col.gameObject.CompareTag("Border"))
        {
            stageParent = col.gameObject.transform.root;

            Debug.Log("うんち");
        }
    }
    void ModeChange()
    {
        sprite.color = new Color(0, 0, 0, 1);
        //追加　小野
        Instantiate(HaretuEffect, transform.position, transform.rotation);
        //Instantiate(HaretuEffect2, transform.position, transform.rotation);

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
}
