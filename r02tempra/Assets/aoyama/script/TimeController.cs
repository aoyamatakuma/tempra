using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour {

    public static float waitTime;
    private bool isCo;
    //public AudioClip select1;
    //private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        isCo = false;
        waitTime = 3;
        //audioSource = gameObject.GetComponent<AudioSource>();
        
    }
	
	// Update is called once per frame
	void Update () {
        //if (waitTime <= 3 && isCo == false)
        //{
        //    //audioSource.clip = select1;
        //    //audioSource.Play();
        //    isCo = true;
        //}
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
        if(waitTime<=0)
        {
            waitTime = 0;
        }
    }
}
