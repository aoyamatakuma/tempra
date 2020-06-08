﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
 

 
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        //FPS固定
        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(QualitySettings.vSyncCount);
        Debug.Log(Application.targetFrameRate);
        

    }
}
