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
    public AudioClip trapSound;
    public GameObject effectPrefab;
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
        // エフェクトを出す。（posでエフェクトの出現位置を調整する。）
        Vector3 pos = other.transform.position;
        GameObject effect = (GameObject)Instantiate(effectPrefab, new Vector3(pos.x, pos.y + 1, pos.z - 1), Quaternion.identity);

        // エフェクトを２秒後に消す。
        Destroy(effect, 1.0f);

    
        if (moveStatus)
        {
            //  移動先は直後移動できないようにする
            if (gameObject != warp[0])
            {  
                warp[0].GetComponent<Warp>().moveStatus = false;
                obj.transform.position = warp[0].transform.position;
              

            }
            else if (gameObject != warp[1])
            {
                warp[1].GetComponent<Warp>().moveStatus = false;
                obj.transform.position = warp[1].transform.position;
            }
        }
    }
    //public IEnumerator wait()
    //{

       

    //}
    //物体と離れた直後呼ばれる
    void OnTriggerExit2D(Collider2D other)
    {
        //移動可能にする。
        moveStatus = true;

    }
}