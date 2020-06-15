using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectIcon : MonoBehaviour
{
    int cntNum;
    [SerializeField]
    private GameObject Button1;
    [SerializeField]
    private GameObject Button2;
    [SerializeField]
    private GameObject Button3;


    [SerializeField]
    //Scene スクリプト取得
    private SceneM gameManager;
    [SerializeField]
    //フェードイン処理
    private GameObject fadeInPrefab;
    private GameObject fadeInInstance;

    bool cntFlag;

    //se
    public AudioClip select1;
    public AudioClip decision1;
    private AudioSource select;
    private AudioSource decision;

    // Start is called before the first frame update
    void Start()
    {

        //読み込む
        gameManager = GameObject.Find("GameManager").GetComponent<SceneM>();
        gameManager = gameManager.GetComponent<SceneM>();
        cntNum = 0;
        cntFlag = true;
        //フェードイン消去
        Destroy(fadeInInstance);

        select = gameObject.GetComponent<AudioSource>();
        decision = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeMode();
        Select();
    }

    void ChangeMode()
    {
        switch(cntNum)
        {
            case 0:
               
                transform.localPosition = new Vector3(-472, -192, 0);
                break;
            case 1:
                transform.localPosition = new Vector3(-472, -354, 0);
                break;
            case 2:
                transform.localPosition = new Vector3(-472, -497, 0);
                break;
            default:
                break;
        }
    }

    void Select()
    {
        if (Input.GetAxis("Vertical") < -0.1f && cntNum != 2 && cntFlag)
        {
           
            StartCoroutine(CountNum(1));
            select.clip = select1;
            select.Play();
        }
        if (Input.GetAxis("Vertical") > 0.1f && cntNum != 0 && cntFlag)
        {
            StartCoroutine(CountNum(-1));
            select.clip = select1;
            select.Play();
        }

        if (Input.GetButtonDown("A_BUTTON"))
        {
            if (cntNum == 0)
            {
                StartCoroutine("NextStageCoroutine");
            }
            if (cntNum == 1)
            {
                StartCoroutine("StageSelectCoroutine");
            }
            if(cntNum == 2)
            {
                StartCoroutine("MainCoroutine");
            }
            decision.clip = decision1;
            decision.Play();
        }
    }

    public void NextSecene()
    {
        if (gameManager.currentStageNum == 11)
        {
            SceneManager.LoadScene("MasterTitle");
        }
        else
        {
            SceneManager.LoadScene("stage" + gameManager.currentStageNum);

        }
    }

    public IEnumerator NextStageCoroutine()
    {

        if (fadeInInstance == null)
        {
            yield return new WaitForSeconds(0.5f);
            //フェードイン処理
            fadeInInstance = GameObject.Instantiate(fadeInPrefab) as GameObject;
            yield return new WaitForSeconds(1f);
            NextSecene();
            Destroy(GameObject.FindGameObjectWithTag("SceneManager"));


        }

    }

    IEnumerator CountNum(int num)
    {
        cntFlag = false;
        cntNum += num;
        yield return new WaitForSeconds(0.5f);
        cntFlag = true;
    }

    public IEnumerator StageSelectCoroutine()
    {

        if (fadeInInstance == null)
        {
            yield return new WaitForSeconds(0.5f);
            //フェードイン処理
            fadeInInstance = GameObject.Instantiate(fadeInPrefab) as GameObject;
            yield return new WaitForSeconds(1f);
            Destroy(GameObject.FindGameObjectWithTag("SceneManager"));
            //Debug.Log ("ステージセレクト");
            SceneManager.LoadScene("select");

        }

    }
    public IEnumerator MainCoroutine()
    {

        if (fadeInInstance == null)
        {
            yield return new WaitForSeconds(0.5f);
            //フェードイン処理
            fadeInInstance = GameObject.Instantiate(fadeInPrefab) as GameObject;
            yield return new WaitForSeconds(1f);
            Destroy(GameObject.FindGameObjectWithTag("SceneManager"));
            //Debug.Log ("タイトル");
            SceneManager.LoadScene("MasterTitle");

        }

    }
}





