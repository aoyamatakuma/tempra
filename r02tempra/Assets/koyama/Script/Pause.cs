using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Rigidbodyの速度を保存しておくクラス
public class RigidbodyVelocity {
    public Vector3 velocity;
    public Vector3 angularVeloccity;
    public RigidbodyVelocity (Rigidbody rigidbody) {
        velocity = rigidbody.velocity;
        angularVeloccity = rigidbody.angularVelocity;
    }
}

public class Pause : MonoBehaviour {

  [SerializeField]
    public GameObject manuCanvas;
 
    //現在セーブ中か？
    public bool pausing;

    //無視するゲームオブジェクト
    public GameObject[] ignoreGameObjects;

    //ポーズ状態が変更された時間を調べるため前回のポーズ状況を記録
    private bool prevPausing;
    //ポーズ中の速度配列
    RigidbodyVelocity[] rigidbodyVelocities;
    //ポーズ中のRigidbodyの配列
    Rigidbody[] pausingRigidbodies;
    //ポーズ中のMonoBehaviourの配列
    MonoBehaviour[] pausingMonoBehaviours;
   
   void Start() {
       
   }
    // Update is called once per frame
    void Update () {
        PauseandResumeCall ();
    }

    void PauseandResumeCall () {
        // ポーズ状態が変更されていたら、Pause/Resumeを呼び出す。
        if (Input.GetKeyDown ("joystick button 7") && pausing == false) {
            //Debug.Log ("ウンコモラスメン");
            pausing = true;
            Pausing ();
            manuCanvas.SetActive (true);
            
        } else if (Input.GetKeyDown ("joystick button 7") && pausing == true) {
            //Debug.Log ("ウンコモラサナイメン");
            pausing = false;
            Resume ();
            manuCanvas.SetActive (false);
        }

    }

    void Pausing () {
        //Rigidbodyの停止処理
        //IgnoreGameObjectsに含まれていないRigidbodyを抽出
        Predicate<Rigidbody> rigidbodyPredicate = obj => !obj.IsSleeping () && Array.FindIndex (ignoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
        //FindAllで数字のみ取り出す
        pausingRigidbodies = Array.FindAll (transform.GetComponentsInChildren<Rigidbody> (), rigidbodyPredicate);
        rigidbodyVelocities = new RigidbodyVelocity[pausingRigidbodies.Length];

        for (int i = 0; i < pausingRigidbodies.Length; i++) {
            // 速度、角速度も保存しておく
            rigidbodyVelocities[i] = new RigidbodyVelocity (pausingRigidbodies[i]);
            pausingRigidbodies[i].Sleep ();
        }
        // MonoBehaviourの停止
        // IgnoreGameObjectsに含まれていないMonoBehaviourを抽出
        Predicate<MonoBehaviour> monoBehaviourPredicate = obj => obj.enabled && obj != this && Array.FindIndex (ignoreGameObjects, gameObject => gameObject == obj.gameObject) < 0;
        //FindAllで数字のみ取り出す
        pausingMonoBehaviours = Array.FindAll (transform.GetComponentsInChildren<MonoBehaviour> (), monoBehaviourPredicate);
        //一つずつ変数を取り出して無効にしてる
        foreach (var monoBehaviour in pausingMonoBehaviours) {
            monoBehaviour.enabled = false;
        }

    }
    //再開
    void Resume () {
        // Rigidbodyの再開
        for (int i = 0; i < pausingRigidbodies.Length; i++) {
            pausingRigidbodies[i].WakeUp ();
            pausingRigidbodies[i].velocity = rigidbodyVelocities[i].velocity;
            pausingRigidbodies[i].angularVelocity = rigidbodyVelocities[i].angularVeloccity;
        }

        // MonoBehaviourの再開
        //一つずつ変数を取り出して有効にしてる
        foreach (var monoBehaviour in pausingMonoBehaviours) {
            monoBehaviour.enabled = true;
        }
    }
}