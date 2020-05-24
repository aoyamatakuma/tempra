﻿using System.Collections;
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
    Wind_hit
}

public class StageRule : MonoBehaviour {
    [SerializeField]
    private float speed =10f;
    [SerializeField]
    private PlayerMove player;
   
    public StageState currentStageState;
    private StageState previousStageState;
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
        if (isWind)
        {
            Debug.Log("風だよ");
        }
        SetCurrentState(StageState.Normal);   
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
            case StageState.Wind_Left:
                LeftWindMove(wind_left_position, wind_target);
                RightWindMove(wind_right_position, wind_target);
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

        if (countBubble < limit_bubble   && currentStageState != StageState.hit_bottom) {

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

        if (limit_bubble <= current_bubble && !isGoal  && limit_touchBubble <= current_touchBubble && currentStageState != StageState.hit_up && currentStageState != StageState.Wind_hit) {
            SetCurrentState(StageState.Fly);
            flyBool = true;
            downBool = false;       
        }

        if (downBool && !isGoal && currentStageState != StageState.hit_bottom && currentStageState != StageState.Wind_hit) {
            flyBool = false;
            SetCurrentState(StageState.Down);
        }

        if(currentStageState == StageState.Normal || currentStageState == StageState.Wind_hit)
        {
            flyBool = false;
            downBool = false;
        }
    }

    void FlyMove (Vector2 nextPos) {
        if (!flyBool && currentStageState != StageState.Fly )return;
        transform.position = Vector2.MoveTowards(transform.position, nextPos,Time.deltaTime*  speed);
       // transform.position = Vector2.Lerp (transform.position, nextPos, Time.deltaTime * 1f);     
      if(transform.position.y >= nextPos.y)
        {
           
            SetCurrentState(StageState.Normal);
        }
    }

    void DownMove (Vector2 nextPos) {
        if (!downBool && currentStageState != StageState.Down ) return;
        transform.position = Vector2.MoveTowards(transform.position, nextPos, Time.deltaTime * speed);
        if (transform.position.y <= nextPos.y)
        { 
            SetCurrentState(StageState.Normal);
        }   
    }

    void LeftWindMove(Vector2 nextPos,GameObject target)
    {
        if (!leftWind && currentStageState != StageState.Wind_Left) return;
        Debug.Log("左");
        target. transform.position = Vector2.MoveTowards(target.transform.position, nextPos, Time.deltaTime * speed);
        if (target.transform.position.x <= nextPos.x)
        {
            SetCurrentState(StageState.Normal);
            target.gameObject.GetComponent<StageRule>().SetPosition_Dawn();
            target.gameObject.GetComponent<StageRule>().SetPosition_Up();
            target.gameObject.GetComponent<StageRule>().SetCurrentState(previousStageState);
        }

    }
    void SetPosition_Up()
    {
        up_position.x = transform.position.x; 
    }

    void SetPosition_Dawn()
    {
        firstPos = transform.position;
        firstPos.y = 0;
    }
    void RightWindMove(Vector2 nextPos, GameObject target)
    {
        if (!rightWind && currentStageState != StageState.Wind_Right) return;
        Debug.Log("右");
        target.transform.position = Vector2.MoveTowards(target.transform.position, nextPos, Time.deltaTime * speed);
        if (target.transform.position.x >= nextPos.x)
        {
            SetCurrentState(StageState.Normal);
            target.gameObject.GetComponent<StageRule>().SetPosition_Dawn();
            target.gameObject.GetComponent<StageRule>().SetPosition_Up();
            target.gameObject.GetComponent<StageRule>().SetCurrentState(previousStageState);
        }

    }

    public void HitStage(Collider2D col)
    {
        if (col.gameObject.CompareTag("Collsion_up"))
        {
           
            SetCurrentState(StageState.hit_bottom);
        }

        if (col.gameObject.CompareTag("Collision_bottom"))
        {
         
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

    public void Wind_Col(Collider2D col)
    {
        if (isWind)
        {         
            if (leftWind)
            {            
                if (col.gameObject.CompareTag("Border_Right"))
                {
                    Debug.Log("風に触れた");
                    GameObject colObj = col.transform.root.gameObject;
                    previousStageState = colObj.GetComponent<StageRule>().currentStageState;
                    colObj.GetComponent<StageRule>().SetCurrentState(StageState.Wind_hit);
                    wind_target = colObj;
                    SetCurrentState(StageState.Wind_Left);
                }
            }
            if (rightWind)
            {
                if (col.gameObject.CompareTag("Border_Left"))
                {
                    Debug.Log("風に触れた");
                    GameObject colObj = col.transform.root.gameObject;
                    previousStageState = colObj.GetComponent<StageRule>().currentStageState;
                    colObj.GetComponent<StageRule>().SetCurrentState(StageState.Wind_hit);
                    wind_target = colObj;
                    SetCurrentState(StageState.Wind_Right);
                }
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