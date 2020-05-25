using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swicth : MonoBehaviour
{
    public GameObject obj;
    //移動状態を表すフラグ


    void Start()
    {
        obj = GameObject.FindGameObjectWithTag("");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(obj.gameObject);
        }

    }
}