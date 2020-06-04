using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Selectmanager : MonoBehaviour
{
    [SerializeField]
    private GameObject fadeOutPrefab;
    private GameObject fadeOutInstance;

    [SerializeField]
    private GameObject fadeInPrefab;
    private GameObject fadeInInstance;


    public Animator targetAnimator;
    public Animator targetAnimatorLeft;
    public Animator effect;

    public AudioClip select1;
    public AudioClip cancel1;
    public AudioClip decision1;
    private AudioSource select;
    private AudioSource cancel;
    private AudioSource decision;



  
    
    public Image stage2;

    private bool rightMoveFlag;
    private bool leftMoveFlag;
    private bool endFlag;

    private Color SelectOn = new Color(255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
    private Color SelectOff = new Color(0 / 255.0f, 0 / 255.0f, 0 / 255.0f,0);

    static int unlockStage;

    int cntMove;
    int cntnumber;
    int cntStage=1;

    int stageMax=10;//最大ステージ数
    int stageMin=1;//最小ステージ数

    // Start is called before the first frame update
    void Start()
    {
        stageMax = 10;
        stageMin = 1;

        stage2.GetComponent<Image>().color = SelectOff;
        Destroy(fadeOutInstance);
        Destroy(fadeInInstance);
        endFlag = true;
        

        if (fadeOutInstance == null)
        {
            fadeOutInstance = GameObject.Instantiate(fadeOutPrefab) as GameObject;
        }


        //cntStage = 1;
        cntnumber=0;
        cntMove = 0;
        rightMoveFlag = false;
        leftMoveFlag = false;

        select = gameObject.GetComponent<AudioSource>();
        cancel = gameObject.GetComponent<AudioSource>();
        decision = gameObject.GetComponent<AudioSource>();
        if (cntStage == 1)
        {
            targetAnimatorLeft.SetTrigger("Invisible");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (endFlag == true)
        {
            StartCoroutine("Flag");
        }
        if (endFlag == true)
        {
            return;
        }

        if (fadeInInstance == null)
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
            if (cntStage != stageMax && Input.GetAxis("Horizontal") > 0.9f && rightMoveFlag == false && leftMoveFlag == false)
            {
                targetAnimator.SetTrigger("Right");
                select.clip = select1;
                select.Play();
                if (cntStage == stageMin)
                {
                    targetAnimatorLeft.SetTrigger("Lightsup");
                }
                cntStage++;
                rightMoveFlag = true;
                if (cntStage == stageMax)
                {
                    targetAnimator.SetTrigger("Invisible");
                }
            }

            if (cntStage != stageMin && Input.GetAxis("Horizontal") < -0.9f && rightMoveFlag == false && leftMoveFlag == false)
            {
                targetAnimatorLeft.SetTrigger("Right");
                select.clip = select1;
                select.Play();
                if (cntStage == stageMax)
                {
                    targetAnimator.SetTrigger("Lightsup");
                }
                cntStage--;

                leftMoveFlag = true;
                if (cntStage == stageMin)
                {
                    targetAnimatorLeft.SetTrigger("Invisible");
                }
            }
            if (cntStage == stageMax && Input.GetAxis("Horizontal") > 0.1f && rightMoveFlag == false && leftMoveFlag == false)
            {
                //cancel.clip = cancel1;
                //cancel.Play();
            }
            if (cntStage == stageMin && Input.GetAxis("Horizontal") < -0.1f && rightMoveFlag == false && leftMoveFlag == false)
            {
                //cancel.clip = cancel1;
                //cancel.Play();
            }




            if (cntStage == 1 || cntStage == 2 || cntStage == 3 || cntStage == 4 || cntStage == 5 || cntStage == 6 || cntStage == 7 || cntStage == 8 || cntStage == 9 || cntStage == 10)
            {

                stage2.GetComponent<Image>().color = SelectOff;
            }
            else
            {
                stage2.GetComponent<Image>().color = SelectOn;
            }

        }
    }

    void Select()
    {
        if (Input.GetButtonDown("A_BUTTON")&& rightMoveFlag == false && leftMoveFlag == false)
        {
            if (cntStage == 1|| cntStage == 2 || cntStage == 3 || cntStage == 4 || cntStage == 5 || cntStage == 6 || cntStage == 7 || cntStage == 8|| cntStage == 9 || cntStage == 10)
            {
                if (fadeInInstance == null)
                {
                    endFlag = true;
                    decision.clip = decision1;
                    decision.Play();
                    fadeInInstance = GameObject.Instantiate(fadeInPrefab) as GameObject;
                    StartCoroutine("End");
                }
            }
            else
            {
                cancel.clip = cancel1;
                cancel.Play();
            }
        }

    }
    public IEnumerator End()
    {
        yield return new WaitForSeconds(2);
        if (cntStage == 1)
        {
            SceneManager.LoadScene("stage1");
        }
        if (cntStage == 2)
        {
            SceneManager.LoadScene("stage2");
        }
        if(cntStage == 3)
        {
            SceneManager.LoadScene("stage3");
        }
        if (cntStage == 4)
        {
            SceneManager.LoadScene("stage4");
        }
        if (cntStage == 5)
        {
            SceneManager.LoadScene("stage5");
        }
        if (cntStage == 6)
        {
            SceneManager.LoadScene("stage6");
        }
        if (cntStage == 7)
        {
            SceneManager.LoadScene("stage7");
        }
        if (cntStage == 8)
        {
            SceneManager.LoadScene("stage8");
        }
        if (cntStage == 9)
        {
            SceneManager.LoadScene("stage9");
        }
        if (cntStage == 10)
        {
            SceneManager.LoadScene("stage10");
        }
        //SceneManager.LoadScene("GameMain");
    }

    public IEnumerator Flag()
    {
        yield return new WaitForSeconds(1);
        endFlag = false;
    }
}
