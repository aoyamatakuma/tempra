using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHeadMove : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigidPlayer;
    CircleCollider2D playerHeadCollider;
    Animator anim;
    [SerializeField] Transform stageParent;
    [SerializeField] PlayerMove player;

    private bool headScale;

    private bool moveflag;

    // Start is called before the first frame update
    void Start()
    {
        rigidPlayer = GetComponent<Rigidbody2D>();
        playerHeadCollider = GetComponent<CircleCollider2D>();
        player = player.GetComponent<PlayerMove>();
        anim = GetComponent<Animator>();
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

        if(headScale)
        {
            transform.localScale += new Vector3(0.05f, 0.05f, 0);
        }
    }

    void Move()//移動系
    {
        float h = Input.GetAxis("Horizontal");
        rigidPlayer.velocity = new Vector2(speed * h, rigidPlayer.velocity.y);

        if (h != 0)
        {
            anim.SetBool("Move", true);
            if (h > 0)
            {
                transform.localRotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                transform.localRotation = new Quaternion(0, 180, 0, 0);
            }

        }
        else
        {
            anim.SetBool("Move", false);
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
            anim.SetBool("Stop", false);
            anim.SetBool("Move", false);
            anim.SetTrigger("StopTrigger");
            moveflag = false;
            rigidPlayer.simulated = false;
            playerHeadCollider.enabled = false;
            transform.parent = col.gameObject.transform;
            transform.localPosition = player.GetHeadPosition();
            transform.localRotation = new Quaternion(0, 0, 0, 0);
            GetComponent<PlayerHeadMove>().enabled = false;
        }
    }

    public IEnumerator Coroutine()
    {
        //処理１
        headScale = true;
        //１秒待機
        yield return new WaitForSeconds(1.0f);
        headScale = false;
        //コルーチンを終了
        yield break;
    }
}
