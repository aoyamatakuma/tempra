using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
   
     //経過時間カウント
     private float time_KO;
    // Start is called before the first frame update
    void Start()
    {
        //経過時間初期化
        time_KO=0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        TimeScene();
    }
    
    //時間経過でシーン切り替え
    public void TimeScene()
    {
        //経過時間をカウント
        time_KO +=Time.deltaTime;

        //3秒経過で画面移動
        if(time_KO>=3.0f)
        {
            SceneManager.LoadScene("select");
        }
    }
}
