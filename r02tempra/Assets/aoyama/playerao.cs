using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerao : MonoBehaviour

{

    Rigidbody2D rigidPlayer;//物理演算
    public float jumpForce = 250.0f;//ジャンプの力
    public float speed = 2.0f;//地上での移動速度
    bool jumpFlag;
    // Start is called before the first frame update
    void Start()
    {
        rigidPlayer = GetComponent<Rigidbody2D>();
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
        rigidPlayer.velocity = new Vector2(speed * h, rigidPlayer.velocity.y);
    }
    void Jump()//ジャンプ系
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpFlag == false)//ジャンプボタンを押してなおかつジャンプ中じゃないとき
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
}
