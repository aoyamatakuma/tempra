using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneM : MonoBehaviour

{
   
    //現在のステージ番号
    public int currentStageNum;

   
    // Start is called before the first frame update
    void Start()
    {
        //シーン切り替えても消去しないようにする
        DontDestroyOnLoad(this);
        Debug.Log(currentStageNum);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
 
    
  
}
