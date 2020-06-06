using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    public GameObject obj;
    public Warp transObj;
    private Vector3[] transVec;
    //移動状態を表すフラグ
    public bool moveStatus;
    public GameObject[] warp;
    private PlayerMove player;
  
    
    // 歩行速度
    public float walkSpeed = 3f;
  
   
    void Start()
    {
        warp = GameObject.FindGameObjectsWithTag("WarpPoint");
        
        //  transVec = transObj.transform.position;
        //初期では移動可能なためTrue
        moveStatus = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        obj = GameObject.Find(other.name);
        //スクリプト取得
        player = other.GetComponent<PlayerMove>();

        //自分が移動可能なとき移動する。
     
        if (other.gameObject.CompareTag("Player"))
        {
            // 経過時間をカウント
            if (moveStatus)
            {
                player.StartCoroutine("Warolocal");
                StartCoroutine("WarpCoroutine");
                warp[0].GetComponent<Warp>().moveStatus = false;
                warp[1].GetComponent<Warp>().moveStatus = false;
                //  移動先は直後移動できないようにする
                if (gameObject != warp[0])
                {
                    StartCoroutine("Warp1");
                }
                else if (gameObject != warp[1])
                {
                    StartCoroutine("Warp2");
                }
            }
        }
    }

    //ワープアニメーション？
    public IEnumerator WarpCoroutine()
    {
        //無効化
        player.SetCurrentState(PlayerState.Warp);
        yield return new WaitForSeconds(2f);
        player.SetCurrentState(PlayerState.Normal);
        //コルーチンを終了
        yield break;
    }
    //ワープ時間
    public IEnumerator Warp1()
    {
        //無効化
        yield return new WaitForSeconds(1f);
        player.transform.position = warp[0].transform.position;
        //yield return new WaitForSeconds(2f);
        //player.SetCurrentState(PlayerState.Normal);
        //コルーチンを終了
        yield break;
    }

    //ワープ時間
    public IEnumerator Warp2()
    {
        //無効化
        yield return new WaitForSeconds(1f);
        player.transform.position = warp[1].transform.position;
        //yield return new WaitForSeconds(2f);
        //player.SetCurrentState(PlayerState.Normal);
        //コルーチンを終了
        yield break;
    }

    //物体と離れた直後呼ばれる
    void OnTriggerExit2D(Collider2D other)
    {
        //移動可能にする
        StartCoroutine("warpstatus");
    }
    public IEnumerator warpstatus()
    {
        //無効化
        yield return new WaitForSeconds(0.5f);
          moveStatus = true;
        //コルーチンを終了
        yield break;
    }
}