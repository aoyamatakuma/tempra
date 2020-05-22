using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonSwich : MonoBehaviour {
    // Start is called before the first frame update
    void Start () {
        // 自分を選択状態にする
        Selectable sel = GetComponent<Selectable> ();
        sel.Select ();
    }

    // Update is called once per frame
    void Update () {

    }
    public void OnClick () {
        //名前を取得して分岐
        switch (transform.name) {
            case "Button":
                //Debug.Log ("リスタート");
                SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
                break;
            case "Button1":
                //Debug.Log ("ステージセレクト");
                SceneManager.LoadScene ("select");
                break;
            case "Button2":
                //Debug.Log ("タイトル");
                SceneManager.LoadScene ("MasterTitle");
                break;
            default:
                break;

        }

    }
}