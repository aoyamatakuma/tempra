using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour {
    [SerializeField]
    private GameObject fadeOutPrefab;
    private GameObject fadeOutInstance;


    [SerializeField]
    private GameObject fadeInPrefab;
    private GameObject fadeInInstance;

    [SerializeField]
    private GameObject startButtonPrefab;
    private GameObject startButtonInstance;

    [SerializeField]
    private GameObject selectButtonPrefab;
    private GameObject selectButtonInstance;



    //public AudioClip select1;
    //public AudioClip select2;
    //private AudioSource audioSource;
    //public static int Bgm;



    // Use this for initialization
    void Start () {
        //Bgm = 0;
        Destroy(fadeOutInstance);
        Destroy(fadeInInstance);
        Destroy(startButtonInstance);
        Destroy(selectButtonInstance);

        //audioSource = gameObject.GetComponent<AudioSource>();

        if (fadeOutInstance == null)
        {
            fadeOutInstance = GameObject.Instantiate(fadeOutPrefab) as GameObject;
        }
        if (startButtonInstance == null)
        {
            startButtonInstance = GameObject.Instantiate(startButtonPrefab) as GameObject;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("GamePad_A"))
        {
            if (fadeInInstance == null)
            {
                //audioSource.clip = select1;
                //audioSource.Play();
                //fadeInInstance = GameObject.Instantiate(fadeInPrefab) as GameObject;
                Destroy(startButtonInstance);
                if (selectButtonInstance == null)
                {
                    selectButtonInstance = GameObject.Instantiate(selectButtonPrefab) as GameObject;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //audioSource.clip = select2;
            //audioSource.Play();
        }

    }
    //public IEnumerator End()
    //{
    //    //yield return new WaitForSeconds(2);
    //    //SceneManager.LoadScene("Entry");
    //}
}
