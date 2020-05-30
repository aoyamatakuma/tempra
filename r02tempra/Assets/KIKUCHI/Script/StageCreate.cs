using System.Collections;
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
    [SerializeField]
    private GameObject back_mass;
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
                back_mass.transform.position = new Vector3(j * magnification, i * magnification, 0);
                Instantiate(back_mass);
            }
        } 

        for (int i = 0; i < stageData.StageObjList.Count; i++)
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
                ArraryNum(0, 2, 3);
              
                //break文
                break;
            case 1:
                ArraryNum(2, 0, 2);
                ArraryNum(0, 0, 3);
                ArraryNum(1, 0, 1);
                ArraryNum(0, 1, 1);
               
                //break文
                break;
            case 2:
                ArraryNum(2, 2, 2);
                ArraryNum(1, 1, 1);
                ArraryNum(0, 0, 1);
                ArraryNum(1, 0, 4);
                break;
            case 3:
                ArraryNum(0, 0, 1);
                ArraryNum(1, 2, 2);
                ArraryNum(1, 1, 3);
                ArraryNum(2, 1, 3);
                ArraryNum(2, 0, 2);
                ArraryNum(3, 0, 4);
             
                break;
            case 4:
                ArraryNum(0, 0, 1);
                ArraryNum(0, 2, 2);
                ArraryNum(0, 1, 4);
                ArraryNum(1, 2, 3);
                ArraryNum(1, 1, 4);
                ArraryNum(1, 0, 1);
                ArraryNum(2, 2, 4);
                ArraryNum(2, 0, 1);
                ArraryNum(3, 2, 3);
                ArraryNum(3, 1, 5);
                break;
            case 5:
                ArraryNum(0, 2, 2);
                ArraryNum(0, 1, 4);
                ArraryNum(1, 0, 1);
                ArraryNum(2, 0, 1);
                ArraryNum(3, 2, 3);
                break;
            case 6:
                ArraryNum(0, 0, 1);
                ArraryNum(1, 2, 3);
                ArraryNum(1, 0, 1);
                ArraryNum(2, 2, 1);
                ArraryNum(2, 1, 4);
                ArraryNum(2, 0, 2);
                ArraryNum(3, 0, 5);
                break;
            case 7:
                ArraryNum(0, 0, 2);
                ArraryNum(1, 3, 1);
                ArraryNum(1, 2, 1);
                ArraryNum(1, 0, 5);
                ArraryNum(2, 3, 4);
                ArraryNum(2, 2, 1);
                ArraryNum(2, 0, 2);
                ArraryNum(3, 3, 1);
                ArraryNum(3, 1, 3);
                ArraryNum(3, 0, 4);
               
                break;
            case 8:
                ArraryNum(0, 4, 3);
                ArraryNum(0, 2, 2);
                ArraryNum(1, 4, 3);
                ArraryNum(2, 4, 3);
                ArraryNum(2, 0, 1);
                ArraryNum(3, 4, 2);
                ArraryNum(3, 3, 6);
                ArraryNum(3, 2, 3);
                ArraryNum(3, 0, 4);
                ArraryNum(4, 3, 4);
                ArraryNum(4, 1, 6);
                ArraryNum(4, 0, 7);
                break;
            case 9:
                ArraryNum(1, 4, 2);
                ArraryNum(1, 3, 3);
                ArraryNum(1, 0, 1);
                ArraryNum(2, 4, 1);
                ArraryNum(2, 3, 3);
                ArraryNum(2, 1, 5);
                ArraryNum(2, 0, 1);
                ArraryNum(3, 4, 6);
                ArraryNum(3, 0, 1);
                ArraryNum(4, 2, 4);
                break;
            case 10:
                break;
                
              
        }
    }
}
