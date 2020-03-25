using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carsoru : MonoBehaviour
{
    SpriteRenderer sprite;
    [SerializeField]
    public CameraControl CameraCO;
  
    // カーソルが対象オブジェクトに入った時
    void Start()
    {

        sprite = gameObject.GetComponent<SpriteRenderer>();
    
    }
    void Update()
    {
        if (Input.GetButtonDown("Y_BUTTON") && CameraCO.isCameraPos2)
        {
            Debug.Log("触れてる");
            sprite.color = new Color(1, 0, 0, 1);
        }
        if (Input.GetButtonDown("Y_BUTTON") && CameraCO.isCameraPos2 == false)
        {
            Debug.Log("触れてないよ");
            sprite.color = new Color(255, 255, 255, 1);
        }
     
    }
}
    // カーソルが対象オブジェクトから出た時
    
