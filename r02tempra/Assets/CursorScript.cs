using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
   
    void Start()
    {
    }

    void Update()
    {
        CursorMove();
    }

    void CursorMove()
    {
        float x = Input.GetAxis("4TH_BUTTON_H");
        float y = Input.GetAxis("4TH_BUTTON_V");
        this.transform.position += new Vector3(x, y,0);
    }
}
