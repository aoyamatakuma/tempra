using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum HeadState
{
    Stop,
    Move
}
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

    [HideInInspector]
    public bool headAwaCreate;
    [HideInInspector]
    public bool headGoalAwaDelete;

    //現在の状態
    private HeadState currentHeadState;

    // Start is called before the first frame update
    void Start()
    {
        rigidPlayer = GetComponent<Rigidbody2D>();
        playerHeadCollider = GetComponent<CircleCollider2D>();
        player = player.GetComponent<PlayerMove>();
        anim = GetComponent<Animator>();
        moveflag = false;
        headAwaCreate = true;
        headGoalAwaDelete = true;
        currentHeadState = HeadState.Move;
    }

    // Update is called once per frame
    void Update()
    {
        OnHeadStateChanged(currentHeadState);
        Pause();
        if (headScale)
        {
            transform.localScale += new Vector3(0.05f, 0.05f, 0);
        }
    }

    void OnHeadStateChanged(HeadState state)
    {
        switch (state)
        {
            case HeadState.Stop:
                Stop();
                break;
            case HeadState.Move:
                NormalMove();
                break;
        }
    }

    public void SetCurrentState(HeadState state)
    {
        currentHeadState = state;
        OnHeadStateChanged(currentHeadState);
    }

    void Stop()
    {
        rigidPlayer.velocity = Vector2.zero;
        anim.SetBool("Move", false);
    }

    void NormalMove()
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

        if (col.gameObject.CompareTag("AwaCreate"))
        {
            headAwaCreate = false;
        }

        if (col.gameObject.CompareTag("GoalAwaDelete"))
        {
            headGoalAwaDelete = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("AwaCreate"))
        {
            headAwaCreate = true;
        }

        if (col.gameObject.CompareTag("GoalAwaDelete"))
        {
            headGoalAwaDelete = true;
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
            headAwaCreate = true;
            headGoalAwaDelete = true;
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

    private void Pause()
    {
        if (Input.GetKeyDown("joystick button 7") && currentHeadState != HeadState.Stop)
        {
            SetCurrentState(HeadState.Stop);
        }
        else if (Input.GetKeyDown("joystick button 7") && currentHeadState == HeadState.Stop)
        {
            SetCurrentState(HeadState.Move);
        }
    }
}
