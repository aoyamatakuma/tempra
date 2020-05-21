using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadMove : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigidPlayer;
    CircleCollider2D playerHeadCollider;
    [SerializeField] Transform stageParent;
    [SerializeField] PlayerMove player;

    private bool moveflag;

    // Start is called before the first frame update
    void Start()
    {
        rigidPlayer = GetComponent<Rigidbody2D>();
        playerHeadCollider = GetComponent<CircleCollider2D>();
        player = player.GetComponent<PlayerMove>();
        moveflag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveflag)
        {
            Move();
        }
        if (Input.GetButtonDown("Y_BUTTON"))
        {
            moveflag = !moveflag;
            rigidPlayer.velocity = Vector2.zero;
        }
    }

    void Move()//移動系
    {
        float h = Input.GetAxis("Horizontal");
        rigidPlayer.velocity = new Vector2(speed * h, rigidPlayer.velocity.y);
        if (h != 0)
        {
            if (h > 0)
            {
                transform.localScale = new Vector3(3, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-3, transform.localScale.y, transform.localScale.z);
            }

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("StageArea"))
        {
            stageParent = col.gameObject.transform.root;
            transform.parent = stageParent;
            //flag = true;
        }

        if (col.gameObject.CompareTag("StageBox"))
        {
            player.GetStage(col.gameObject.GetComponent<StageRule>());
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            moveflag = false;
            rigidPlayer.simulated = false;
            playerHeadCollider.enabled = false;
            transform.parent = col.gameObject.transform;
            GetComponent<PlayerHeadMove>().enabled = false;
        }
    }
}
