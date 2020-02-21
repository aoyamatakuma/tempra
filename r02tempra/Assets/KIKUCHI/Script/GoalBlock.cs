using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBlock : MonoBehaviour
{
    void Start()
    {
        Debug.Log("aaaaaaaaaaa");
    }

    void Update() { }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag =="Player")
        {
            Debug.Log("GOAL!!!!!");
        }
    }
}
