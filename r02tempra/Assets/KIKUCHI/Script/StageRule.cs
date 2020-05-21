using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageState{
    Normal,
    Fly,
    Down,
    Wind
}

public class StageRule : MonoBehaviour {
    //[SerializeField]
    //private playerao player;

    [SerializeField]
    private PlayerMove player;

    private StageState currentStageState;

    [SerializeField]
    private bool isWind;
    [SerializeField]
    private bool leftWind;
    [SerializeField]
    private bool rightWind;

    //ステージが浮く時の泡の数

    public int limit_bubble = 3;
    //現在の泡の数
    public int current_bubble;

    private Vector2 firstPos;
    [SerializeField]
    private Vector2 up_position;
    [SerializeField]
    private Vector2 wind_position;

    bool flyBool;
    bool downBool;

    [SerializeField]
    private bool isGoal;

    private int limit_touchBubble;
    
    [SerializeField]
    private int current_touchBubble;

    public List<GameObject> Bubblehub;

    public GameObject stage_Left;

    public GameObject stage_Right;



    
    //   public bool playerbool;

    // Start is called before the first frame update
    void Start ()

    {
        SetCurrentState(StageState.Normal);
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
        BubbleCount_Start();
    }

    // Update is called once per frame
    void Update()
    {
        BubbleCount();
        FlyRule();OnStageStateChanged(currentStageState);
        //    FlyMove (up_position);
        //    DownMove(firstPos);
    }

    public void SetCurrentState(StageState state)
    {
        currentStageState = state;
        OnStageStateChanged(currentStageState);
    }

    void OnStageStateChanged(StageState state)
    {
        switch (state)
        {
            case StageState.Normal:
                
                break;
            case StageState.Fly:
                FlyMove(up_position);
                break;
            case StageState.Down:
                DownMove(firstPos);
                break;
            case StageState.Wind:
               
                break;
            default:
                break;
        }
    }



    void BubbleCount_Start()
    {
        List<GameObject> allChildren = new List<GameObject>();
        GameObject child;


        for (int i = 0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i).gameObject;
            allChildren.Add(child);

        }

        for (int i = 0; i < allChildren.Count; i++)
        {
            if (allChildren[i].tag == "bubble")
            {
                Bubblehub.Add(allChildren[i]);
            }
        }
     
    }

    //ステージ内のbubbleタグのついた子オブジェクトの数取得
    void BubbleCount () {
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
            }
        }

        if (countBubble < limit_bubble  && flyBool ) {

            downBool = true;
        }

        current_bubble = countBubble;
        
    }

    //浮く
    void FlyRule () {

        if (limit_touchBubble <= current_touchBubble && !isGoal ) {
            flyBool = true;
            downBool = false;
            SetCurrentState(StageState.Fly);
        }

        if (downBool && !isGoal ) {
            flyBool = false;
            SetCurrentState(StageState.Down);
        }

    }

    void FlyMove (Vector2 nextPos) {
        if (!flyBool) return;
        if (transform.position.y <= nextPos.y ) {
            transform.position = Vector2.Lerp (transform.position, nextPos, Time.deltaTime * 1f);
        }
      

    }

    void DownMove (Vector2 nextPos) {
        if (!downBool ) return;
        if (transform.position.y >= nextPos.y) {
          
            transform.position = Vector2.Lerp (transform.position, nextPos, Time.deltaTime * 1f);
        }
      
    }

    void LeftWindMove(Vector2 nextPos,GameObject target)
    {
        if (!leftWind) return;
        if (target.transform.position.x >= nextPos.x)
        {
            target.transform.position = Vector2.Lerp(target.transform.position, nextPos, Time.deltaTime * 1f);
        }

    }

    void RightWindMove(Vector2 nextPos, GameObject target)
    {
        if (!rightWind) return;
        if (target.transform.position.x <= nextPos.x)
        {
           target.transform.position = Vector2.Lerp(target.transform.position, nextPos, Time.deltaTime * 1f);
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
        if(col.gameObject.CompareTag("Border_Left"))
        {
            Border_Bool(stage_Right, false);
        }

        if (col.gameObject.CompareTag("Border_Right"))
        {
            Border_Bool(stage_Left, false);
        }

        if (col.gameObject.CompareTag("Collision"))
        {
            downBool = false;
            flyBool = false;
        }
      
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Border_Left"))
        {
            Border_Bool(stage_Right, true);
        }

        if (col.gameObject.CompareTag("Border_Right"))
        {
            Border_Bool(stage_Left, true);
        }

    }

    void Border_Bool(GameObject obj,bool setBool)
    {
        obj.SetActive(setBool);
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