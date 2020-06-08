using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRule : MonoBehaviour
{
    public  bool isPlay;

  
    // Start is called before the first frame update
    void Start()
    {
        isPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void falsePlay()
    {
        isPlay = false;
    }

    public void truePlay()
    {
        isPlay = true;
    }

    public bool getIsPlay()
    {
        return isPlay;
    }
}
