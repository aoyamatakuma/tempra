using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBool : MonoBehaviour
{
    StageRule stageRule;
    StageState stageState;
    [SerializeField]
     private  GameRule gameRule;
    GameObject[] stages;
    


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        boolStage();
    }

    void Initialize()
    {
        stages = GameObject.FindGameObjectsWithTag("StageBox");
    }

    void boolStage()
    { 
        int cnt = 0;
        for (int i = 0; i < stages.Length; i++)
        {
            stageRule = stages[i].GetComponent<StageRule>();
            if (stageRule.currentStageState == StageState.Fly || stageRule.currentStageState == StageState.Down || stageRule.currentStageState == StageState.Wind_hit)
            {       
                cnt++;
                gameRule.falsePlay();
             
            }
            if (cnt == 0)
            {
                gameRule.truePlay();         
            }
        }
    }    
}
