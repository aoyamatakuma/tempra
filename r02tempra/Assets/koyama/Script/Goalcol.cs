using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class Goalcol : MonoBehaviour
{

    [SerializeField]
    //フェードイン処理
    private GameObject fadeInPrefab;
    private GameObject fadeInInstance;
    [SerializeField]
    //プレイヤースクリプト取得
    private PlayerMove Player;
    [SerializeField]
    private SpriteRenderer sprite;
   
   

    // Start is called before the first frame update
    void Start()
    {
       
        //フェードイン消去
        Destroy(fadeInInstance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //当たり判定
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //スクリプト取得
            Player = col.GetComponent<PlayerMove>();
                   
            //コルーチン開始
            StartCoroutine("GoalCoroutine");       
        }
        if(col.gameObject.tag == "PlayerHead")
        {
            Debug.Log("aaaaaaaaaaaaaaaaa");
            sprite.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "PlayerHead")
        {                 
            sprite.enabled = false;       
        }
    }
    public IEnumerator GoalCoroutine()
    {
   
        //無効化
        Player.SetCurrentState(PlayerState.Goal);

        Debug.Log("コルーチン");
      
       
        if (fadeInInstance == null)
        {
          
           
            //1秒停止
            yield return new WaitForSeconds(2f);
            //フェードイン処理
            fadeInInstance = GameObject.Instantiate(fadeInPrefab) as GameObject;
            yield return new WaitForSeconds(1f);
            //ゴールシーンに飛ぶ
            SceneManager.LoadScene("MasterGoal");

        }

    }
}
