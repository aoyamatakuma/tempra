using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonSwich : MonoBehaviour {

  
    [SerializeField]
    //フェードイン処理
    private GameObject fadeInPrefab;
    private GameObject fadeInInstance;
    // Start is called before the first frame update
    void Start () {
        // 自分を選択状態にする
        Selectable sel = GetComponent<Selectable> ();
        sel.Select ();
       
        //フェードイン消去
        Destroy(fadeInInstance);
    }

    // Update is called once per frame
    void Update () {

    }
    public void OnClick () {
        //名前を取得して分岐
        switch (transform.name) {
            case "Button":
                //コルーチン開始
                StartCoroutine("RestCoroutine");
                break;
            case "Button1":
                //コルーチン開始
                StartCoroutine("SelectCoroutine");
                break;
            case "Button2":
                //コルーチン開始
                StartCoroutine("MasterTitleCoroutine");
                break;
            default:
                break;

        }

    }
    public IEnumerator RestCoroutine()
    {

        if (fadeInInstance == null)
        {
            yield return new WaitForSeconds(1f);
            //フェードイン処理
            fadeInInstance = GameObject.Instantiate(fadeInPrefab) as GameObject;
            yield return new WaitForSeconds(1f);
            //Debug.Log ("リスタート");
            Destroy(GameObject.FindGameObjectWithTag("SceneManager"));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
           

        }

    }
    public IEnumerator SelectCoroutine()
    {

        if (fadeInInstance == null)
        {
            yield return new WaitForSeconds(1f);
            //フェードイン処理
            fadeInInstance = GameObject.Instantiate(fadeInPrefab) as GameObject;
            yield return new WaitForSeconds(1f);
            //Debug.Log ("ステージセレクト");
            Destroy(GameObject.FindGameObjectWithTag("SceneManager"));
            SceneManager.LoadScene("select");

        }

    }
    public IEnumerator MasterTitleCoroutine()
    {

        if (fadeInInstance == null)
        {
            yield return new WaitForSeconds(1f);
            //フェードイン処理
            fadeInInstance = GameObject.Instantiate(fadeInPrefab) as GameObject;
            yield return new WaitForSeconds(1f);
            //Debug.Log ("タイトル");
            Destroy(GameObject.FindGameObjectWithTag("SceneManager"));
            SceneManager.LoadScene("MasterTitle");
         
        }

    }
}