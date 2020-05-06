using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRule : MonoBehaviour
{
    //[SerializeField]
    //private playerao player;

    [SerializeField]
    private PlayerMove player;

    //ステージが浮く時の泡の数

    public int limit_bubble =3;
    //現在の泡の数
    public int current_bubble;

    
    private Vector2 firstPos;
    [SerializeField]
    private Vector2 up_position;

    private int limit_touchBubble;
    [SerializeField]
    private int current_touchBubble;

    bool flyBool;
    bool downBool;

 //   public bool playerbool;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        flyBool = false;
        limit_touchBubble = limit_bubble;
        current_bubble = 0;
        downBool = false;
        current_bubble = 0;
        firstPos = transform.position;
        firstPos.y = 0;
        up_position.x = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        BubbleCount();
        FlyRule();
        FlyMove(up_position);
    }

    //ステージ内のbubbleタグのついた子オブジェクトの数取得
    void BubbleCount()
    {
        List<GameObject> allChildren = new List<GameObject>();
        GameObject child;
        int countBubble = 0;
        
       
        for(int i=0;i< transform.childCount; i++)
        {
            child = transform.GetChild(i).gameObject;
            allChildren.Add(child);
           
        }

        
        for(int i = 0; i < allChildren.Count; i++)
        {
            if (allChildren[i].tag == "bubble")
            {
                countBubble++;
                downBool = false;             
            }
        }

        if(countBubble < limit_bubble && flyBool)
        {
           
            downBool = true;
        }
        
        current_bubble = countBubble;
       // player.awaCreate = false;
    }

    //浮く
    void FlyRule()
    {
      
        if (limit_bubble <= current_bubble)
        { 
            flyBool = true;
        }

        if( downBool)
        {
            flyBool = false;
            DownMove(firstPos);
        }

    }

    void FlyMove(Vector2 nextPos)
    {
        if (!flyBool) return;
        if (transform.position.y <= nextPos.y && flyBool && limit_touchBubble <= current_touchBubble)
        {
            transform.position = Vector2.Lerp(transform.position, nextPos, Time.deltaTime * 1f);
        }
        
    }

    void DownMove(Vector2 nextPos)
    {
        if (!downBool) return;
        if (transform.position.y >= nextPos.y && downBool)
        {
            transform.position = Vector2.Lerp(transform.position, nextPos, Time.deltaTime * 1f);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("bubble"))
        {
            current_touchBubble++;
        }
    }


}
