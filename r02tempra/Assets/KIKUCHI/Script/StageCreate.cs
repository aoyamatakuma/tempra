﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StageCreate : MonoBehaviour
{
    public StageData stageData;
    private  int width;//盤上の行
    private  int height;//盤上の列
    private int StageNumbers;//ステージの番号
    [SerializeField] 
    private float magnification = 36;
    private int[,]posStage;
    Dictionary<int, GameObject> stageObject = new Dictionary<int, GameObject>();

    //Start
    //Awake
    //OnEnable
    void Start()
    {

        width  = stageData.Xmax;
        height = stageData.Ymax;
        StageNumbers = stageData.Number_S;
        posStage = new int[height,width];
       
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                posStage[i, j] = 0;
            }
        } 

        for (int i = 0; i < stageData.StageObjList.Count; i++)
        {
            stageObject.Add(i + 1, stageData.StageObjList[i]);
        }
        //ステージ管理
        Arrrayswitch();
      
        Debug.Log("活動中");
        Create();

        
    }
    void Update()
    {
       
    }


    void Create()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if(posStage[i, j] != 0)
                {
                    //重要
                    stageObject[posStage[i, j]].transform.position = new Vector3(j * magnification, i*magnification, 0);
                    Instantiate(stageObject[posStage[i, j]]);
                  
                }
            }
        }
    }
    void ArraryNum(int x, int y, int value)
    {
        posStage[y, x] = value;
    }
    void Arrrayswitch()
    {
        //ステージ管理
        switch (StageNumbers)
        {
            //名前が「Sphere」のとき
            case 0:
                ArraryNum(2, 2, 2);
                ArraryNum(1, 0, 1);
                ArraryNum(0, 2, 1);
                Debug.Log("0の場合");
                //break文
                break;
            case 1:
                ArraryNum(2, 2, 2);
                ArraryNum(1, 0, 1);
                ArraryNum(0, 1, 1);
                Debug.Log("１の場合");
                //break文
                break;
            case 2:
                ArraryNum(2, 2, 2);
                ArraryNum(0, 2, 1);
                ArraryNum(0, 0, 1);
                Debug.Log("2の場合");
                //break文
                break;
        }
    }
}