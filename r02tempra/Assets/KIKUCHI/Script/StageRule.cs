using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRule : MonoBehaviour
{
    [SerializeField]
    private playerao player;

    //ステージが浮く時の泡の数
    
    public int limit_bubble =3;
    //現在の泡の数
    public int current_bubble;

    
    private Vector2 firstPos;
    [SerializeField]
    private Vector2 up_position;

    bool flyBool;
    bool downBool;


    // Start is called before the first frame update
    void Start()

    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerao>();
        flyBool = false;
        downBool = false;
        current_bubble = 0;
        firstPos = transform.position;
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
        if (!player.awaCreate) return;


        List<GameObject> allChildren = new List<GameObject>();
        GameObject child;
        int countBubble = 0;
        
       
        for(int i=0;i< transform.childCount; i++)
        {
            child = transform.GetChild(i).gameObject;
            allChildren.Add(child);
           
        }
        Debug.Log(transform);
        
        for(int i = 0; i < allChildren.Count; i++)
        {
            if (allChildren[i].tag == "bubble")
            {
                countBubble++;
                downBool = false;
               
            }
        }

        if(countBubble == 0)
        {
           
            downBool = true;
        }
        
        current_bubble = countBubble;
        player.awaCreate = false;
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
            current_bubble = 0;
            flyBool = false;
            DownMove(firstPos);
        }

    }

    void FlyMove(Vector2 nextPos)
    {
        if (!flyBool) return;
        if (transform.position.y <= nextPos.y && flyBool)
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


}
