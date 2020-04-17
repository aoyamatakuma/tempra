using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreate : MonoBehaviour
{
    public enum eStageState
    {
        EMPTY,
        STAGE
    };

    public GameObject Squares;
    [SerializeField]
    private  int width = 8;//盤上の行
    [SerializeField]
    private  int height = 8;//盤上の列
    private float magnification;
   // private int[][] posStage = new int[][];

    void Awake()
    {
        SquaresCreate();
    }
    void Start()
    {
        
    }

    void Update()
    {

    }


    void SquaresCreate()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                GameObject.Instantiate(Squares);
                magnification = Squares.transform.localScale.x;
                Squares.transform.position = new Vector3(i * magnification, j * magnification,0);
            }
        }
    }




}
