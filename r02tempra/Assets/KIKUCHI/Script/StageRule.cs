using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRule : MonoBehaviour {
    //[SerializeField]
    //private playerao player;

    [SerializeField]
    private PlayerMove player;

    //ステージが浮く時の泡の数

    public int limit_bubble = 3;
    //現在の泡の数
    public int current_bubble;

    private Vector2 firstPos;
    [SerializeField]
    private Vector2 up_position;

    public bool flyBool;
    public bool downBool;

    [SerializeField]
    private bool isGoal;

    private int limit_touchBubble;
    [SerializeField]
    private int current_touchBubble;

    public List<GameObject> Bubblehub;

    public GameObject stageBorder;

    //   public bool playerbool;

    // Start is called before the first frame update
    void Start ()

    {
       
        player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMove> ();
        flyBool = false;
        downBool = false;
        current_bubble = 0;
        limit_touchBubble = limit_bubble;
        current_touchBubble = 0;
        firstPos = transform.position;
        up_position.x = transform.position.x;
        firstPos.y = 0;

        Bubblehub = new List<GameObject>();
    }

    // Update is called once per frame
    void Update () {
        BubbleCount ();
        FlyRule ();
        FlyMove (up_position);
    }

    //ステージ内のbubbleタグのついた子オブジェクトの数取得
    void BubbleCount () {
        //  if (!player.awaCreate) return;

        //if(player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerao>())
        //{
        //    playerbool = true;
        //}

        List<GameObject> allChildren = new List<GameObject> ();
        GameObject child;
        int countBubble = 0;

        for (int i = 0; i < transform.childCount; i++) {
            child = transform.GetChild (i).gameObject;
            allChildren.Add (child);

        }

        for (int i = 0; i < allChildren.Count; i++) {
            if (allChildren[i].tag == "bubble") {
                countBubble++;
               
                //Bubblehub.Add(allChildren[i]);
            }
        }

        if (countBubble < limit_bubble  && flyBool) {

            downBool = true;
        }

        current_bubble = countBubble;
        // player.awaCreate = false;
    }

    //浮く
    void FlyRule () {

        if (limit_bubble <= current_bubble && !isGoal ) {
            flyBool = true;
            downBool = false;
        }

        if (downBool && !isGoal) {
            flyBool = false;
            DownMove (firstPos);
        }

    }

    void FlyMove (Vector2 nextPos) {
        if (!flyBool) return;
        if (transform.position.y <= nextPos.y && flyBool && limit_touchBubble <= current_touchBubble) {
            transform.position = Vector2.Lerp (transform.position, nextPos, Time.deltaTime * 1f);
        }

    }

    void DownMove (Vector2 nextPos) {
        if (!downBool) return;
        if (transform.position.y >= nextPos.y && downBool) {
            transform.position = Vector2.Lerp (transform.position, nextPos, Time.deltaTime * 1f);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("bubble"))
        {
            current_touchBubble++;

        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("bubble"))
        {
            current_touchBubble--;

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Border"))
        {
            stageBorder.SetActive(false);
        }

      
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Border"))
        {
            stageBorder.SetActive(true);
        }
    }


    public void Minus()
    {
        player.BubbleCount(current_bubble);
    }

    public void ListBubble(GameObject obj)
    {
        Bubblehub.Add(obj);
    }
}