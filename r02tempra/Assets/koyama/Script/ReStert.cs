using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReStert : MonoBehaviour {

    void Start () {
        // 自分を選択状態にする
        Selectable sel = GetComponent<Selectable> ();
        sel.Select ();
    }
    // Update is called once per frame
    void Update () {

    }

    //public void StringArgFunction (string scene) {
    //SceneManager.LoadScene (scene);
    // }

    public void OnClick () {
        SceneManager.LoadScene ("StageDemo");
    }
}