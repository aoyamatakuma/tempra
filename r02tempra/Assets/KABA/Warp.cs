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
  public  GameObject[] warp;
   
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
        //自分が移動可能なとき移動する。
        if (moveStatus)
        {
            //移動先は直後移動できないようにする
            if (gameObject!=warp[0])
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
    //物体と離れた直後呼ばれる
    void OnTriggerExit2D(Collider2D other)
    {
        //移動可能にする。
        moveStatus = true;
    }
}
