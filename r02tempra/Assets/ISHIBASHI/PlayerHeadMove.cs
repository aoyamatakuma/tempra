using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadMove : MonoBehaviour
{
    bool playerHitFlag;
    public float speed;
    Rigidbody2D rigidPlayer;
    CircleCollider2D playerHeadCollider;
    [SerializeField] Transform stageParent;
    // Start is called before the first frame update
    void Start()
    {
        rigidPlayer = GetComponent<Rigidbody2D>();
        playerHeadCollider = GetComponent<CircleCollider2D>();
        playerHitFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("StageArea") && playerHitFlag == false)
        {
            stageParent = col.gameObject.transform.root;
            transform.parent = stageParent;
            playerHitFlag = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            rigidPlayer.simulated = false;
            playerHeadCollider.enabled = false;
            transform.parent = GameObject.Find("Player").transform;
            GetComponent<PlayerHeadMove>().enabled = false;
        }
    }
}
