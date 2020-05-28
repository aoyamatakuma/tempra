using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swicth : MonoBehaviour
{
    [SerializeField]
    private GameObject walls;

    bool isPush;

    void Start()
    {

        walls = GameObject.FindWithTag("WallBox");

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            // 消す！
            isPush = true;
            Destroy(walls);
        }

    }
}