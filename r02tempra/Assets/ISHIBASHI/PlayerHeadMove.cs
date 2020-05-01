using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadMove : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigidPlayer;
    CircleCollider2D playerHeadCollider;
    public GameObject foam;
    [SerializeField] Transform stageParent;
    GameObject stage;
   // bool flag;
    // Start is called before the first frame update
    void Start()
    {
        rigidPlayer = GetComponent<Rigidbody2D>();
        playerHeadCollider = GetComponent<CircleCollider2D>();
        //flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Baburu();
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

    void Baburu()
    {
        //マウス入力で左クリックしたとき
        if (Input.GetButtonDown("X_BUTTON"))
        {
            //awaCreate = true;
            //BaburuPosition = transform.position;
            //BaburuPosition.z = 10f;
            stage = (GameObject)Instantiate(foam, transform.position, Quaternion.identity, stageParent);
            //foamCount++;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("StageArea") )
        {
            stageParent = col.gameObject.transform.root;
            transform.parent = stageParent;
           // flag = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            rigidPlayer.simulated = false;
            playerHeadCollider.enabled = false;
            transform.parent = col.gameObject.transform;
            GetComponent<PlayerHeadMove>().enabled = false;
        }
    }
}
