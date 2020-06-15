using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauceManu : MonoBehaviour
{
    int cntNum;
   

 
    [SerializeField]
    //フェードイン処理
    private GameObject fadeInPrefab;
    private GameObject fadeInInstance;

    public bool cntFlag;

    //se
    public AudioClip select1;
    public AudioClip decision1;
    private AudioSource select;
    private AudioSource decision;
    // Start is called before the first frame update
    void Start()
    {

   
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
        switch (cntNum)
        {
            case 0:

                transform.localPosition = new Vector3(-290, 185, 0);
                break;
            case 1:
                transform.localPosition = new Vector3(-494, 19, 0);
                break;
            case 2:
                transform.localPosition = new Vector3(-439, -167, 0);
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
                StartCoroutine("RestCoroutine");
            }
            if (cntNum == 1)
            {
                StartCoroutine("StageSelectCoroutine");
            }
            if (cntNum == 2)
            {
                StartCoroutine("MainCoroutine");
            }
            decision.clip = decision1;
            decision.Play();
        }
    }


    public IEnumerator RestCoroutine()
    {

        if (fadeInInstance == null)
        {
            yield return new WaitForSeconds(0.5f);
            //フェードイン処理
            fadeInInstance = GameObject.Instantiate(fadeInPrefab) as GameObject;
            yield return new WaitForSeconds(1f);
            //Debug.Log ("リスタート");
            Destroy(GameObject.FindGameObjectWithTag("SceneManager"));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);


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
