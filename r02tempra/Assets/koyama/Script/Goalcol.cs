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
    //Scene スクリプト取得
    private SceneM gameManager;
    [SerializeField]
    private SpriteRenderer sprite;

    Animator anim;

   
    public AudioClip goalse1;
    private AudioSource goalse;
   

    // Start is called before the first frame update
    void Start()
    {
       
        //フェードイン消去
        Destroy(fadeInInstance);
        anim = gameObject.GetComponent<Animator>();
        //読み込む
        gameManager = GameObject.Find("GameManager").GetComponent<SceneM>();
        gameManager = gameManager.GetComponent<SceneM>();
        goalse = gameObject.GetComponent<AudioSource>();
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
            anim.SetBool("Open", true);
            goalse.clip = goalse1;
            goalse.Play();
            //スクリプト取得
            Player = col.GetComponent<PlayerMove>();
                   
            if (gameManager.currentStageNum == 10)
            {
                StartCoroutine("GoalEndCoroutine");
            }
            else
            {
                //コルーチン開始
                StartCoroutine("GoalCoroutine");

            }
        }
        if(col.gameObject.tag == "PlayerHead")
        {
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
    public IEnumerator GoalEndCoroutine()
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
            SceneManager.LoadScene("MasterEnd");

        }

    }
}
