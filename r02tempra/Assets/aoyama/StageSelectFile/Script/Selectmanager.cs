using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Selectmanager : MonoBehaviour
{
    static int targetFrameRate;
    public Animator targetAnimator;
    public Animator targetAnimatorLeft;
    public Animator effect;
    public Image rightSlect;
    public Image leftSlect;
    public Image stage2;
    public Image stage3;
    private bool rightMoveFlag;
    private bool leftMoveFlag;
    

    int cntMove;
    int cntnumber;
    int cntStage;

    void Awake()
    {
        Application.targetFrameRate =23; //60FPSに設定
    }

    // Start is called before the first frame update
    void Start()
    {
        
        cntStage = 0;
        cntnumber=0;
        cntMove = 0;
        rightMoveFlag = false;
        leftMoveFlag = false;
        if (cntStage == 0)
        {
            targetAnimatorLeft.SetTrigger("Invisible");
        }
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
            if (cntnumber == 400)
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
        if (cntStage != 2 && Input.GetAxis("Horizontal") > 0.9f && rightMoveFlag == false && leftMoveFlag == false)
        {
            targetAnimator.SetTrigger("Right");
            if (cntStage == 0)
            {
                targetAnimatorLeft.SetTrigger("Lightsup");
            }
            cntStage++;
            rightMoveFlag = true;
            if (cntStage == 2)
            {
                targetAnimator.SetTrigger("Invisible");
            }
        }

        if (cntStage != 0 && Input.GetAxis("Horizontal") < -0.9f && rightMoveFlag == false && leftMoveFlag == false)
        {
            targetAnimatorLeft.SetTrigger("Right");
            if (cntStage == 2)
            {
                targetAnimator.SetTrigger("Lightsup");
            }
            cntStage--;

            leftMoveFlag = true;
            if (cntStage == 0)
            {
                targetAnimatorLeft.SetTrigger("Invisible");
            }
        }
    }

    void Select()
    {
        if (Input.GetButtonDown("A_BUTTON")&& rightMoveFlag == false && leftMoveFlag == false)
        {
            if (cntStage == 0)
            {
                SceneManager.LoadScene("StageExample");
            }
            if (cntStage == 1)
            {
                SceneManager.LoadScene("stage02");
            }
            if (cntStage == 2)
            {
            }
        }
    }
}
