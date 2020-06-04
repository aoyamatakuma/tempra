using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageState{
    Normal,
    Fly,
    Down,
    Wind_Left,
    Wind_Right,
    hit_up,
    hit_bottom,
    hit_right,
    hit_left,
    hit_left_right,
    hit_bottom_up,
    Wind_hit
}

public class StageRule : MonoBehaviour {
    [SerializeField]
    private float speed =10f;
    [SerializeField]
    private PlayerMove player;
    
    
    public StageState currentStageState;
    public StageState previousStageState;
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
    private Vector2 wind_left_position;
    [SerializeField]
    private Vector2 wind_right_position;
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
        Bubblehub = new List<GameObject>();
        BubbleCount_Start();
        player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMove> ();
        flyBool = false;
        downBool = false;
        current_bubble = 0;      
        current_touchBubble = 0;
        SetPosition_Dawn();
        SetPosition_Up();
        wind_left_position= transform.position;
        wind_right_position.y = transform.position.y;
        wind_left_position.x = 0;      
      
     
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
            case StageState.Wind_Left:
                LeftWindMove(wind_left_position, wind_target);           
                break;
            case StageState.Wind_Right:
                RightWindMove(wind_right_position, wind_target);
                break;
            case StageState.Wind_hit:
              
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

        if (countBubble < limit_bubble   && currentStageState != StageState.hit_bottom && currentStageState != StageState.Wind_hit && currentStageState != StageState.hit_bottom_up) {

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

        if (limit_bubble <= current_bubble && !isGoal  && limit_touchBubble <= current_touchBubble && currentStageState != StageState.hit_up && currentStageState != StageState.Wind_hit && currentStageState != StageState.hit_bottom_up) {     
            flyBool = true;
            downBool = false;
            SetCurrentState(StageState.Fly);
        }

        if (downBool && !isGoal && currentStageState != StageState.hit_bottom && currentStageState != StageState.Wind_hit && currentStageState != StageState.hit_bottom_up) {
            flyBool = false;
            SetCurrentState(StageState.Down);
        }

        if(currentStageState == StageState.Normal || currentStageState == StageState.Wind_hit )
        {
            flyBool = false;
            downBool = false;
        }
    }

    void FlyMove (Vector2 nextPos) {
        if (!flyBool && currentStageState != StageState.Fly )return;
      //  if (currentStageState == StageState.hit_bottom_up) return;
        transform.position = Vector2.MoveTowards(transform.position, nextPos,Time.deltaTime*  speed);
       // transform.position = Vector2.Lerp (transform.position, nextPos, Time.deltaTime * 1f);     
      if(transform.position.y >= nextPos.y || currentStageState != StageState.Fly)
        {  
            SetCurrentState(StageState.Normal);
        }
    }

    void DownMove (Vector2 nextPos) {
        if (!downBool && currentStageState != StageState.Down ) return;
       // if (currentStageState == StageState.hit_bottom_up) return;
        transform.position = Vector2.MoveTowards(transform.position, nextPos, Time.deltaTime * speed);
        if (transform.position.y <= nextPos.y || currentStageState != StageState.Down)
        { 
            SetCurrentState(StageState.Normal);
        }   
    }

    void LeftWindMove(Vector2 nextPos,GameObject target)
    {
        
        if (currentStageState != StageState.Wind_Left && target.gameObject.GetComponent<StageRule>().currentStageState != StageState.Wind_hit ) return;
        target. transform.position = Vector2.MoveTowards(target.transform.position, nextPos, Time.deltaTime * speed);
        if (target.transform.position.x <= nextPos.x || target.gameObject.GetComponent<StageRule>().currentStageState != StageState.Wind_hit)
        {
            target.gameObject.GetComponent<StageRule>().SetPosition_Up();
            target.gameObject.GetComponent<StageRule>().SetPosition_Dawn();
            target.gameObject.GetComponent<StageRule>().SetCurrentState(StageState.Normal);
          //  Debug.Log(target.gameObject.GetComponent<StageRule>().currentStageState);
            SetCurrentState(StageState.Normal);
        }

    }
 
    void RightWindMove(Vector2 nextPos, GameObject target)
    {
        if (currentStageState != StageState.Wind_Right && target.gameObject.GetComponent<StageRule>().currentStageState != StageState.Wind_hit ) return;
        target.transform.position = Vector2.MoveTowards(target.transform.position, nextPos, Time.deltaTime * speed);
        if (target.transform.position.x >= nextPos.x || target.gameObject.GetComponent<StageRule>().currentStageState != StageState.Wind_hit)
        {
            target.gameObject.GetComponent<StageRule>().SetPosition_Up();
            target.gameObject.GetComponent<StageRule>().SetPosition_Dawn();
            target.gameObject.GetComponent<StageRule>().SetCurrentState(StageState.Normal);
            SetCurrentState(StageState.Normal);
        }
    }
   public void SetPosition_Up()
    {
        up_position.x = transform.position.x;
    }

   public void SetPosition_Dawn()
    {
        firstPos = transform.position;
        firstPos.y = 0;
    }

    public void HitStage(Collider2D col)
    {
        if (!isWind)
        {
            if (currentStageState == StageState.Wind_hit)
            {
                if (col.gameObject.CompareTag("Collision_left"))
                {   
                  
                   SetCurrentState(StageState.hit_right);
                                 
                }

                if (col.gameObject.CompareTag("Collision_right"))
                {
                  
                  SetCurrentState(StageState.hit_left);
                    
                }
            }
            else
            {
                if (col.gameObject.CompareTag("Collsion_up") && currentStageState !=StageState.hit_bottom && currentStageState != StageState.hit_up && currentStageState != StageState.hit_bottom_up)
                {
                  
                   SetCurrentState(StageState.hit_bottom);                                        
                }

                if (col.gameObject.CompareTag("Collision_bottom") && currentStageState != StageState.hit_up && currentStageState != StageState.hit_bottom && currentStageState != StageState.hit_bottom_up)
                {                  
                   SetCurrentState(StageState.hit_up);                  
                }

                if (col.gameObject.CompareTag("Collsion_up") && currentStageState == StageState.hit_up && currentStageState != StageState.hit_bottom_up)
                {               
                    SetCurrentState(StageState.hit_bottom_up);
                }
                if (col.gameObject.CompareTag("Collision_bottom") && currentStageState == StageState.hit_bottom && currentStageState != StageState.hit_bottom_up)
                {           
                    SetCurrentState(StageState.hit_bottom_up);
                }
            }
            
        }   
    }

    public void ExitStage(Collider2D col)
    {
       
        if(currentStageState == StageState.Wind_hit)
        {
            if (col.gameObject.CompareTag("Collision_left"))
            {
                SetCurrentState(StageState.Normal);
            }

            if (col.gameObject.CompareTag("Collision_right"))
            {
                SetCurrentState(StageState.Normal);
            }
        }
        else
        {
            if (col.gameObject.CompareTag("Collsion_up") && currentStageState == StageState.hit_bottom )
            {
                SetCurrentState(StageState.Normal);              
            }
            if (col.gameObject.CompareTag("Collision_bottom") && currentStageState == StageState.hit_up )
            {
                SetCurrentState(StageState.Normal);           
            }
            if (col.gameObject.CompareTag("Collsion_up") && currentStageState == StageState.hit_bottom_up)
            {
                SetCurrentState(StageState.hit_bottom);
            }
            if (col.gameObject.CompareTag("Collision_bottom") && currentStageState == StageState.hit_bottom_up)
            {
                SetCurrentState(StageState.hit_up);
            }
        }

    }

    public void Wind_Col(Collider2D col)
    {
        if (currentStageState == StageState.Wind_Left || currentStageState == StageState.Wind_Right || !isWind ) return;
              
            if (leftWind)
            {            
                if (col.gameObject.CompareTag("Border_Right"))
                {           
                    StageRule colObj = col.transform.root.gameObject.GetComponent<StageRule>();
                    if (colObj.currentStageState == StageState.Normal || colObj.currentStageState ==StageState.hit_bottom_up) return;
                    colObj.previousStageState = colObj.currentStageState;
                    if (colObj.currentStageState != StageState.Normal)
                    {
                        colObj.SetCurrentState(StageState.Wind_hit);
                    }                      
                    wind_target = colObj.gameObject;
                    SetCurrentState(StageState.Wind_Left);
                }
            }
            if (rightWind)
            {
                if (col.gameObject.CompareTag("Border_Left"))
                {              
                    StageRule colObj = col.transform.root.gameObject.GetComponent<StageRule>();
                    if (colObj.currentStageState == StageState.Normal || colObj.currentStageState == StageState.hit_bottom_up) return;               
                     colObj.previousStageState = colObj.currentStageState;                   
                    if (colObj.currentStageState != StageState.Normal)
                    {
                        colObj.SetCurrentState(StageState.Wind_hit);
                    }
                    wind_target = colObj.gameObject;
                    SetCurrentState(StageState.Wind_Right);
                }
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
       
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (!isWind)
        {
          
            if (currentStageState == StageState.Normal || currentStageState == StageState.hit_bottom || currentStageState == StageState.hit_up)
            {
                StageRule colStage = col.gameObject.transform.root.GetComponent<StageRule>();
                    if (col.gameObject.CompareTag("Border_Left"))
                    {                    
                        if (colStage.currentStageState == StageState.Normal || colStage.currentStageState == StageState.hit_bottom || colStage.currentStageState == StageState.hit_up)
                        {
                            Border_Bool(stage_Right, false);
                            Border_Bool(light_Right, true);
                        }                       
                    }
                    if (col.gameObject.CompareTag("Border_Right"))
                    {
                        if (colStage.currentStageState == StageState.Normal || colStage.currentStageState == StageState.hit_bottom || colStage.currentStageState == StageState.hit_up)
                        {
                            Border_Bool(stage_Left, false);
                            Border_Bool(light_Left, true);
                        }
                    }
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