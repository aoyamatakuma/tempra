using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearButton : MonoBehaviour
{
    

    [SerializeField]
    //Scene スクリプト取得
    private SceneM gameManager;
    // Start is called before the first frame update
    void Start()
    {
        // 自分を選択状態にする
        Selectable sel = GetComponent<Selectable>();
        sel.Select();
        //読み込む
        gameManager = GameObject.Find("GameManager").GetComponent<SceneM>();
        gameManager = gameManager.GetComponent<SceneM>();
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
                NextSecene();
                break;
            case "Button1":
                //Debug.Log ("ステージセレクト");
                SceneManager.LoadScene("select");
                break;
            case "Button2":
                //Debug.Log ("タイトル");
                SceneManager.LoadScene("MasterTitle");
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
}
