using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swicth : MonoBehaviour
{
    public GameObject walls;
    //移動状態を表すフラグ


    void Start()
    {

        walls = GameObject.FindWithTag("WallBox");

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            // 消す！
            Destroy(walls);
        }

    }
}