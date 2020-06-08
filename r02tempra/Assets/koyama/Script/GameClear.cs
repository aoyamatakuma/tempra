using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    //フェードアウト
    [SerializeField]
    private GameObject fadeOutPrefab;
    private GameObject fadeOutInstance;
    [SerializeField]
    private GameObject stageManu;

    [SerializeField]
    //Scene スクリプト取得
    private SceneM gameManager;



    //経過時間カウント
    private float time_KO;
    private float time_Next;
    // Start is called before the first frame update
    void Start()
    {
        //読み込む
        gameManager = GameObject.Find("GameManager").GetComponent<SceneM>();
        //経過時間初期化
        time_KO =0.0f;
        time_Next = 0.0f;
        Destroy(fadeOutInstance);
    


        //フェードアウト
        if (fadeOutInstance == null)
        {
            fadeOutInstance = GameObject.Instantiate(fadeOutPrefab) as GameObject;
        }
        gameManager = gameManager.GetComponent<SceneM>();
        CountStage();
    }

    // Update is called once per frame
    void Update()
    {
        TimeScene();
        TimeNextSecene();
    }
    
    
    public void TimeScene()
    {
        //経過時間をカウント
        time_KO +=Time.deltaTime;


        //3秒経過で画面移動
        if(time_KO>=1.0f)
        {

            stageManu.SetActive(true);
        }
    }
    public void TimeNextSecene()
    {
        if(gameManager.currentStageNum==11)
        {
            //経過時間をカウント
            time_Next += Time.deltaTime;
            if (time_Next>=4.0f)
            {
            SceneManager.LoadScene("MasterTitle");
            Destroy(GameObject.FindGameObjectWithTag("SceneManager"));

            }
        }
    }
    void CountStage()
    {
        gameManager.currentStageNum += 1;
        Debug.Log(gameManager.currentStageNum);
    }
}
