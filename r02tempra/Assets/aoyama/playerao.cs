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
    // Start is called before the first frame update
    void Start()
    {
        rigidPlayer = GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        jumpFlag = false;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()//移動系
    {
        float h = Input.GetAxis("Horizontal");
        Vector2 position = transform.localPosition;
        if(h != 0)
        {
            position.x += h * speed * 0.1f;
        }
        transform.localPosition = position;
       // rigidPlayer.velocity = new Vector2(speed * h, rigidPlayer.velocity.y);
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
    }
    void ModeChange()
    {
        sprite.color = new Color(0, 0, 0, 1);
        //追加　小野
        Instantiate(HaretuEffect, transform.position, transform.rotation);
        //Instantiate(HaretuEffect2, transform.position, transform.rotation);

    }
}
