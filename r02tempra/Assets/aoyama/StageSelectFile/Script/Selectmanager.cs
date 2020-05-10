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


    int cntMove;
    int cntnumber;
    int cntStage;

    // Start is called before the first frame update
    void Start()
    {
        stage2.GetComponent<Image>().color = SelectOff;
        Destroy(fadeOutInstance);
        Destroy(fadeInInstance);
        endFlag = true;
        

        if (fadeOutInstance == null)
        {
            fadeOutInstance = GameObject.Instantiate(fadeOutPrefab) as GameObject;
        }


        cntStage = 0;
        cntnumber=0;
        cntMove = 0;
        rightMoveFlag = false;
        leftMoveFlag = false;

        select = gameObject.GetComponent<AudioSource>();
        cancel = gameObject.GetComponent<AudioSource>();
        decision = gameObject.GetComponent<AudioSource>();
        if (cntStage == 0)
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
        if (endFlag==true)
        {
            return;
        }


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
            select.clip = select1;
            select.Play();
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
            select.clip = select1;
            select.Play();
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
        if (cntStage == 2 && Input.GetAxis("Horizontal") > 0.1f && rightMoveFlag == false && leftMoveFlag == false)
        {
            cancel.clip = cancel1;
            cancel.Play();
        }
        if (cntStage == 0 && Input.GetAxis("Horizontal") < -0.1f && rightMoveFlag == false && leftMoveFlag == false)
        {
            cancel.clip = cancel1;
            cancel.Play();
        }




         if (cntStage == 2)
        {

            stage2.GetComponent<Image>().color = SelectOn;
        }
        else
        {
            stage2.GetComponent<Image>().color = SelectOff;
        }
        
    }

    void Select()
    {
        if (Input.GetButtonDown("A_BUTTON")&& rightMoveFlag == false && leftMoveFlag == false)
        {
            if (cntStage != 2)
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
            if (cntStage == 2)
            {
                cancel.clip = cancel1;
                cancel.Play();
            }
        }

    }
    public IEnumerator End()
    {
        yield return new WaitForSeconds(2);
        if (cntStage == 0)
        {
            SceneManager.LoadScene("StageExample");
        }
        if (cntStage == 1)
        {
            SceneManager.LoadScene("stage02");
        }
        //SceneManager.LoadScene("GameMain");
    }

    public IEnumerator Flag()
    {
        yield return new WaitForSeconds(1);
        endFlag = false;
    }
}
