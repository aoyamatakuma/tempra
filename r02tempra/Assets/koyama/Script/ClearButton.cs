﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearButton : MonoBehaviour
{


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
        //フェードイン消去
        Destroy(fadeInInstance);
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
    public IEnumerator NextStageCoroutine()
    {

        if (fadeInInstance == null)
        {
            yield return new WaitForSeconds(1f);
            //フェードイン処理
            fadeInInstance = GameObject.Instantiate(fadeInPrefab) as GameObject;
            yield return new WaitForSeconds(1f);
            NextSecene();

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
            //Debug.Log ("タイトル");
            SceneManager.LoadScene("MasterTitle");

        }

    }
}
