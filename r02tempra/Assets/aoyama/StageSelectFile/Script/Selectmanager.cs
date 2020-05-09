using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Selectmanager : MonoBehaviour
{

    public Image stage1;
    public Image stage2;
    public Image stage3;
    private bool rightMoveFlag;
    private bool leftMoveFlag;

    int cntMove;
    int cntnumber;
    int cntStage;

    // Start is called before the first frame update
    void Start()
    {
        cntStage = 0;
        cntnumber=0;
        cntMove = 0;
        rightMoveFlag = false;
        leftMoveFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Transform myTransform = this.transform;
        //Vector3 pos = myTransform.position;
        transform.localPosition = new Vector3(cntMove, 0, 0);
        // 座標を取得
        //pos= myTransform.localPosition;
        //pos.x = 400;

        if (rightMoveFlag == true)
        {
            cntMove -= 40;
            cntnumber += 40;
            if (cntnumber==400)
            {
                rightMoveFlag = false;
                cntnumber = 0;
            }
        }

        if (leftMoveFlag == true)
        {
            cntMove += 40;
            cntnumber += 40;
            if (cntnumber == 400)
            {
                leftMoveFlag = false;
                cntnumber = 0;
            }
        }

        //myTransform.position =pos;
        Select();
        if (Input.GetAxis("Horizontal") > 0.9f&&rightMoveFlag == false && leftMoveFlag == false)
        {
            cntStage++;
            if (cntStage >= 1)
            {
                cntStage = 1;
            }
            rightMoveFlag = true;
        }

        if (cntMove!=0&&Input.GetAxis("Horizontal") < -0.9f && rightMoveFlag == false && leftMoveFlag == false)
        {
            cntStage--;
            if (cntStage <= 0)
            {
                cntStage = 0;
            }
            leftMoveFlag = true;
        }
    }

    void Select()
    {
        if (Input.GetButtonDown("A_BUTTON"))
        {
            if (cntStage == 0)
            {
                SceneManager.LoadScene("StageExample");
            }
            else
            {
                SceneManager.LoadScene("stage02");
            }
        }
    }
}
