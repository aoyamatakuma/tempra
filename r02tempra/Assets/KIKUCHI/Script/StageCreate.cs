using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StageCreate : MonoBehaviour
{
    public StageData stageData;
    private  int width;//盤上の行
    private  int height;//盤上の列
    private int StageNumbers;
    [SerializeField] 
    private float magnification = 36;
    private int[,]posStage;
    Dictionary<int, GameObject> stageObject = new Dictionary<int, GameObject>();


    void Awake()
    {

        width  = stageData.Xmax;
        height = stageData.Ymax;
      
          posStage = new int[3,3];
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                posStage[i, j] = 0;
            }
        }
        for(int i = 0; i < stageData.StageObjList.Count; i++)
        {
            stageObject.Add(i + 1, stageData.StageObjList[i]);
        }
       
        //ステージ管理
        Arrrayswitch();
      

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
                    Instantiate(stageObject[posStage[i, j]]);
                    stageObject[posStage[i, j]].transform.position = new Vector3(j * magnification, i*magnification, 0);

                }
            }
        }
    }
    void Arrrayswitch()
    {
        StageNumbers = stageData.Number_S;
        //ステージ管理
        switch (StageNumbers)
        {
            //名前が「Sphere」のとき
            case 0:
                ArraryNum(2, 2, 2);
                ArraryNum(1, 0, 1);
                ArraryNum(0, 0, 1);
                //break文
                break;
            case 1:
                ArraryNum(2, 2, 2);
                ArraryNum(1, 2, 1);
                ArraryNum(0, 0, 1);
                //break文
                break;
        }

    }
    void ArraryNum(int x,int y,int value)
    {
        posStage[y, x] = value;
    }
    



}
