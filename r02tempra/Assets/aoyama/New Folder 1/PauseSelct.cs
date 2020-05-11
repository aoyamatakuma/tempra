using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseSelct : MonoBehaviour
{
    
    public Image stage1;
    public Image stage2;

    int cntPause;
    GameObject pauseObject;//追加
    Pause script;//追加

    // Use this for initialization
    void Start()
    {
        cntPause = 0;
        pauseObject = GameObject.Find("PauseObject");//追加
        script = pauseObject.GetComponent<Pause>();//追加
    }
}

    // Update is called once per frame
//    void Update()
//    {
//        //int Pauseactive = script.Pauseactive;

//        if (Pauseactive == 1)
//        {
//            if (Input.GetAxis("GamePad1_Vertical") < -0.9f)
//            {
//                cntPause++;
//                if (cntPause >= 1)
//                {
//                    cntPause = 1;
//                }
//            }
//            if (Input.GetAxis("GamePad1_Vertical") > 0.9f)
//            {
//                cntPause--;
//                if (cntPause <= 0)
//                {
//                    cntPause = 0;
//                }
//            }
//        }
//        if (Pauseactive == 2)
//        {
//            if (Input.GetAxis("GamePad2_Vertical") < -0.9f)
//            {
//                cntPause++;
//                if (cntPause >= 1)
//                {
//                    cntPause = 1;
//                }
//            }
//            if (Input.GetAxis("GamePad2_Vertical") > 0.9f)
//            {
//                cntPause--;
//                if (cntPause <= 0)
//                {
//                    cntPause = 0;
//                }
//            }
//        }
//    }

//    void DrawPauseSelect()
//    {
//        if (cntPause == 0)
//        {

//        }
//        else
//        {
//        }
//    }
//    void Select()
//    {

//    }
//}
