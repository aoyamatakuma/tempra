using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerao : MonoBehaviour

{

    Rigidbody2D rigidPlayer;//物理演算
    private float jumpForce = 250.0f;//ジャンプの力
    private float speed = 2.0f;//地上での移動速度
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }

    void Move()//移動系
    {
        float h = Input.GetAxis("Horizontal");
        rigidPlayer.velocity = new Vector2(speed * h, rigidPlayer.velocity.y);
    }
}
