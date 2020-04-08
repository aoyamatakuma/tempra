using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRule : MonoBehaviour
{
    [SerializeField]
    private playerao player;

    //ステージが浮く時の泡の数
    [SerializeField]
    private int limit_bubble =3;
    //現在の泡の数
    public int current_bubble;

    
    // Start is called before the first frame update
    void Start()

    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BubbleCount();
        FlyRule();
    }

    //ステージ内のbubbleタグのついた子オブジェクトの数取得
    void BubbleCount()
    {
        if (!player.awaCreate) return;
        List<GameObject> allChildren = new List<GameObject>();
        GameObject child;
        int countBubble = 0;
       
     
        for(int i=0;i< gameObject.transform.childCount; i++)
        {
            child = gameObject.transform.GetChild(i).gameObject;
            allChildren.Add(child);
        }
        
        for(int i = 0; i < allChildren.Count; i++)
        {
            if(allChildren[i].tag == "bubble")
            {
                countBubble++;
               
            }
        }
        current_bubble = countBubble;
        player.awaCreate = false;
    }

    //浮く
    void FlyRule()
    {
       if(limit_bubble <= current_bubble)
        {
            Debug.Log("浮いたあああああああああああああああああ");
            current_bubble = 0;
        }

    }
}
