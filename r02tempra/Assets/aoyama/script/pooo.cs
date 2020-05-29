using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class pooo : MonoBehaviour
{
    [SerializeField]//ポーズ画面のUI
    private GameObject pauseUIPrefab;
    private GameObject pauseUIInstance;//ポーズUIのインスタンス
    [SerializeField]//ポーズ画面のUI
    private GameObject FadeIn;
    private GameObject FadeInInstance;//ポーズUIのインスタンス
    bool isPlayer1active;
    bool isPlayer2active;
    public int Pauseactive = 0;
    public int YoYoactive = 1;
    int cntPause;
    bool isWait;
    int waitTime;
    GameObject GameStartObject;//追加
    CountDown countDownScript;//追加
    public GameObject Player1;//青山追加
    public GameObject Player2;//青山追加
    // Use this for initialization

    void Start()

    {
        //cntPause = 0;
        isPlayer1active = false;
        isPlayer2active = false;
        isWait = false;
        waitTime = 2;
        GameStartObject = GameObject.Find("GameStopObject");//追加
        countDownScript = GameStartObject.GetComponent<CountDown>();//追加
        Player1 = GameObject.FindWithTag("Player1");
        Player2 = GameObject.FindWithTag("Player2");
    }
    // Update is called once per frame
    void Update()
    {
        if (countDownScript.GameStart != 1)
        {
            return;
        }
        if (Player2 == null||Player1==null)
        {
            Player1 = GameObject.FindWithTag("Player1");
            Player2 = GameObject.FindWithTag("Player2");
            return;
        }





            if (isWait == true)
        {
            return;
        }
        if (Input.GetButtonDown("GamePad1_Pause") && isPlayer2active == false)
        {
            if (pauseUIInstance == null)
            {
                pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
                isPlayer1active = true;
                Pauseactive = 1;
            }
            else
            {
                Destroy(pauseUIInstance);
                isPlayer1active = false;
                Pauseactive = 0;
            }
        }        
        if (Input.GetButtonDown("GamePad2_Pause") && isPlayer1active == false)
        {
            if (pauseUIInstance == null)
            {
                pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
                isPlayer2active = true;
                Pauseactive = 2;
            }
            else
            {
                Destroy(pauseUIInstance);
                isPlayer2active = false;
                Pauseactive = 0;
            }
        }
        
        Select();
    }
    void Select()
    {
        if (Pauseactive == 1 && Input.GetAxis("GamePad1_Vertical") < -0.9f)
        {
            cntPause++;
            if (cntPause >= 1)
            {
                cntPause = 1;
            }
        }
        if (Pauseactive == 1&&Input.GetAxis("GamePad1_Vertical") > 0.9f)
        {
            cntPause--;
            if (cntPause <= 0)
            {
                cntPause = 0;
            }
        }
        if (Pauseactive == 2 && Input.GetAxis("GamePad2_Vertical") < -0.9f)
        {
            cntPause++;
            if (cntPause >= 1)
            {
                cntPause = 1;
            }
        }
        if (Pauseactive == 2 && Input.GetAxis("GamePad2_Vertical") > 0.9f)
        {
            cntPause--;
            if (cntPause <= 0)
            {
                cntPause = 0;
            }
        }
        if (Pauseactive == 1&&cntPause == 0 && Input.GetButtonDown("GamePad1_A"))
        {
            Destroy(pauseUIInstance);
            isPlayer1active = false;
            YoYoactive = 0;
            Pauseactive = 0;
        }
        if (Pauseactive == 2&&cntPause == 0&& Input.GetButtonDown("GamePad2_A"))
         {
            Destroy(pauseUIInstance);
            isPlayer2active = false;
            YoYoactive = 0;
            Pauseactive = 0;
        }


        if (Input.GetButtonUp("GamePad1_A") && isPlayer2active == false) 
        {
            YoYoactive = 1;
        }
        if (Input.GetButtonUp("GamePad2_A") && isPlayer1active == false)
        {
            YoYoactive = 1;
        }

        if (Pauseactive == 1 && cntPause == 1 && Input.GetButtonDown("GamePad1_A"))
        {

            StartCoroutine("TitleScene");
            Pauseactive = 3;
        }
        if (Pauseactive == 2 && cntPause == 1 && Input.GetButtonDown("GamePad2_A"))
        {

            StartCoroutine("TitleScene");
            Pauseactive = 3;
        }

    }

    private IEnumerator TitleScene()
    {
        isWait = true;
        FadeInInstance = GameObject.Instantiate(FadeIn) as GameObject;
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Title");

    }



}
