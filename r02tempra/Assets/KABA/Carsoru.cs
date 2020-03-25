using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carsoru : MonoBehaviour
{
    SpriteRenderer sprite;
    // カーソルが対象オブジェクトに入った時
    void Start()
    {
       
        sprite = gameObject.GetComponent<SpriteRenderer>();

    }
    void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter");
        sprite.color = new Color(1, 0, 0, 1);
    }

    // カーソルが対象オブジェクトから出た時
    void OnMouseExit()
    {
        Debug.Log("OnMouseEnterexit");
        sprite.color = new Color(255, 255, 255, 1);
    }
}
