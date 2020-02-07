using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed =10f;
    [SerializeField]
    private float jumpSpeed = 1000f;

    bool jumpFlag;
    SpriteRenderer sprite;
<<<<<<< HEAD
    Player player;
=======
    Rigidbody2D rig;
>>>>>>> origin/kikuchi
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
<<<<<<< HEAD
      
=======
        rig = gameObject.GetComponent<Rigidbody2D>();
        jumpFlag = false;
>>>>>>> origin/kikuchi
    }


    void Move()
    {
        float hor = Input.GetAxis("Horizontal");
        float mov = hor * moveSpeed;
  
       Vector2 force = new Vector2(mov, 0);   
        if (hor != 0 && !jumpFlag)
        {
            rig.velocity = force;
        }   
        else if(hor != 0 && jumpFlag)
        {
            rig.AddForce(force);
        }
        
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
