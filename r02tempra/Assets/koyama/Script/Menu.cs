using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    Button restart;
    Button end;
    // Start is called before the first frame update
    void Start () {
        //ボタンコンポーネント取得
        restart = GameObject.Find ("ManuCanvas/Panel/Button").GetComponent<Button> ();
        end = GameObject.Find ("ManuCanvas/Panel/Button1").GetComponent<Button> ();

        //最初に選択状態にしたいボタンの設定
        restart.Select();

    }

    // Update is called once per frame
    void Update () {

    }
}