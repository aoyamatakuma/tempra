using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearButton : MonoBehaviour
{
    GameObject buttonA;
    GameObject buttonB;
    GameObject buttonC;
   
    [SerializeField]
    //Scene スクリプト取得
    private SceneM gameManager;
    [SerializeField]
    //フェードイン処理
    private GameObject fadeInPrefab;
    private GameObject fadeInInstance;

   
    // Start is called before the first frame update
    void Start()
    {
        // 自分を選択状態にする
        Selectable sel = GetComponent<Selectable>();
        sel.Select();
        //読み込む
        gameManager = GameObject.Find("GameManager").GetComponent<SceneM>();
        gameManager = gameManager.GetComponent<SceneM>();
        buttonA = GameObject.Find("StageManu/Panel/Button");
        buttonB = GameObject.Find("StageManu/Panel/Button1");
        buttonC = GameObject.Find("StageManu/Panel/Button2");
       
        
        //フェードイン消去
        Destroy(fadeInInstance);
        OnButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        //名前を取得して分岐
        switch (transform.name)
        {
            case "Button": 
                //コルーチン開始
                StartCoroutine("NextStageCoroutine");
                break;
            case "Button1":
                //コルーチン開始
                StartCoroutine("StageSelectCoroutine");
                break;
            case "Button2":
                //コルーチン開始
                StartCoroutine("MainCoroutine");
                break;
           
            default:
                break;

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
    public void OnButton()
    {
        if (gameManager.currentStageNum == 11)
        {
          
            buttonA.SetActive(false);
            buttonB.SetActive(false);
            buttonC.SetActive(false);
           

        }
        else
        {
            buttonA.SetActive(true);
            buttonB.SetActive(true);
            buttonC.SetActive(true);
           

        }
       
    }
    public IEnumerator NextStageCoroutine()
    {

        if (fadeInInstance == null)
        {
            yield return new WaitForSeconds(1f);
            //フェードイン処理
            fadeInInstance = GameObject.Instantiate(fadeInPrefab) as GameObject;
            yield return new WaitForSeconds(1f);
            NextSecene();
            Destroy(GameObject.FindGameObjectWithTag("SceneManager"));
           

        }

    }
    public IEnumerator StageSelectCoroutine()
    {

        if (fadeInInstance == null)
        {
            yield return new WaitForSeconds(1f);
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
            yield return new WaitForSeconds(1f);
            //フェードイン処理
            fadeInInstance = GameObject.Instantiate(fadeInPrefab) as GameObject;
            yield return new WaitForSeconds(1f);
            Destroy(GameObject.FindGameObjectWithTag("SceneManager"));
            //Debug.Log ("タイトル");
            SceneManager.LoadScene("MasterTitle");
        
        }

    }
}
