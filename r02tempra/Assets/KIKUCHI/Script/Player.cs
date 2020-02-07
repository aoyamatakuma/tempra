using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed =0.5f;
    [SerializeField]
    private float jumpSpeed = 1000f;
    bool jumpFlag;
    SpriteRenderer sprite;
    Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        rig = gameObject.GetComponent<Rigidbody2D>();
        jumpFlag = false;
    }


    void Move()
    {
        float hor = Input.GetAxis("Horizontal");
        Vector2 position = transform.position;
        if (hor != 0)
        {
            position.x += moveSpeed * hor;
        }
        transform.position = position;
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpFlag == false)
        {
            rig.AddForce(Vector2.up * jumpSpeed);
            jumpFlag = true;
        }
    }

    void ModeChange()
    {
        sprite.color = new Color(0, 0, 0, 1);
    }
  

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Abura"))
        {
            ModeChange();
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
