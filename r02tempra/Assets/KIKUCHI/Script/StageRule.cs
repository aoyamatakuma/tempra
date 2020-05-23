using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageState{
    Normal,
    Fly,
    Down,
    Wind,
    hit_up,
    hit_bottom
}

public class StageRule : MonoBehaviour {
    [SerializeField]
    private PlayerMove player;
    [SerializeField]
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
    bool colBool;
    [SerializeField]
    private bool isGoal;
    [SerializeField]
    private int limit_touchBubble =2; 
    [SerializeField]
    private int current_touchBubble;
    public List<GameObject> Bubblehub;
    public GameObject stage_Left;
    public GameObject stage_Right;
    public GameObject light_Left;
    public GameObject light_Right;
    private GameObject wind_target;



    //   public bool playerbool;

    // Start is called before the first frame update
    void Start ()

    {    
        SetCurrentState(StageState.Normal);   
        player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMove> ();
        flyBool = false;
        downBool = false;
        current_bubble = 0;      
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
        FlyRule();
        OnStageStateChanged(currentStageState);
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

        if (countBubble < limit_bubble  && flyBool && currentStageState != StageState.hit_bottom) {

            downBool = true;
        }

        current_bubble = countBubble;
        
    }

    void hitMove()
    {
        //SetCurrentState(StageState.Normal);
    }

    //浮く
    void FlyRule () {

        if (limit_bubble <= current_bubble && !isGoal  && limit_touchBubble <= current_touchBubble && currentStageState != StageState.hit_up) {
            flyBool = true;
            downBool = false;
            SetCurrentState(StageState.Fly);
        }

        if (downBool && !isGoal && currentStageState != StageState.hit_bottom) {
            flyBool = false;
            SetCurrentState(StageState.Down);
        }
    }

    void FlyMove (Vector2 nextPos) {
        if (!flyBool && currentStageState != StageState.Fly )return; 
        transform.position = Vector2.Lerp (transform.position, nextPos, Time.deltaTime * 1f);     
      if(transform.position.y >= nextPos.y)
        {
           
            SetCurrentState(StageState.Normal);
        }
    }

    void DownMove (Vector2 nextPos) {
        if (!downBool && currentStageState != StageState.Down ) return;        
        transform.position = Vector2.Lerp (transform.position, nextPos, Time.deltaTime * 1f);      
        if(transform.position.y <= nextPos.y)
        {
            SetCurrentState(StageState.Normal);
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

    public void HitStage(Collider2D col)
    {
        if (col.gameObject.CompareTag("Collsion_up"))
        {
            Debug.Log(currentStageState);
            SetCurrentState(StageState.hit_bottom);
        }

        if (col.gameObject.CompareTag("Collision_bottom"))
        {
            Debug.Log(currentStageState);
            SetCurrentState(StageState.hit_up);
        }
    }

    public void ExitStage(Collider2D col)
    {
        if (col.gameObject.CompareTag("Collsion_up"))
        {
            SetCurrentState(StageState.Normal);
        }
        if (col.gameObject.CompareTag("Collision_bottom"))
        {
            SetCurrentState(StageState.Normal);
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("bubble"))
        {
            current_touchBubble++;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
      
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
        if (isWind)
        {
            if (leftWind)
            {
                if (col.gameObject.CompareTag("Border_Left"))
                {
                    wind_target = col.gameObject;
                }
            }

            if(rightWind)
            {
                if (col.gameObject.CompareTag("Border_Right"))
                {
                    wind_target = col.gameObject;
                }
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (!isWind)
        {
            if (col.gameObject.CompareTag("Border_Left"))
            {
                Border_Bool(stage_Right, false);
                Border_Bool(light_Right, true);
            }
            if (col.gameObject.CompareTag("Border_Right"))
            {
                Border_Bool(stage_Left, false);
                Border_Bool(light_Left, true);
            }
        }
      
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (!isWind)
        {
            if (col.gameObject.CompareTag("Border_Left"))
            {
                Border_Bool(stage_Right, true);
                Border_Bool(light_Right, false);
            }

            if (col.gameObject.CompareTag("Border_Right"))
            {
                Border_Bool(stage_Left, true);
                Border_Bool(light_Left, false);
            }
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