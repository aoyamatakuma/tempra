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
    [SerializeField]
    public int current_bubble;

    List<Transform> allChildren = new List<Transform>();
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
        Transform chiledTransform = transform.GetComponentInChildren<Transform>();
        foreach(var child in chiledTransform)
        {
            allChildren.Add(chiledTransform);
        }
        for(int i = 0; i <= allChildren.Count; i++)
        {
            if(allChildren[i].tag == "bubble")
            {
                current_bubble++;
            }
        }

        player.awaCreate = false;
    }

    //浮く
    void FlyRule()
    {
       if(limit_bubble <= current_bubble)
        {
            Debug.Log("浮いたあああああああああああああああああ");
        }

    }
}
