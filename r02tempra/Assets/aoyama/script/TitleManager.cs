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
    private GameObject menuPrefab;
    private GameObject menuInstance;

    [SerializeField]
    private GameObject deletePrefab;
    private GameObject deleteInstance;

    [SerializeField]
    private GameObject aButtonPrefab;
    private GameObject aButtonInstance;


    public static int Bgm;

    public AudioClip decision1;
    private AudioSource decision;

    // Use this for initialization
    void Start()
    {
        Bgm = 0;
        Destroy(fadeOutInstance);
        Destroy(fadeInInstance);
        Destroy(menuInstance);
        Destroy(deleteInstance);
        Destroy(aButtonInstance);


        if (fadeOutInstance == null)
        {
            fadeOutInstance = GameObject.Instantiate(fadeOutPrefab) as GameObject;
        }

        if (aButtonInstance == null)
        {
            aButtonInstance = GameObject.Instantiate(aButtonPrefab) as GameObject;
        }
        decision = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("A_BUTTON"))
        {
            ////Debug.Log("tes");
            Destroy(aButtonInstance);
          
            if (deleteInstance == null)
            {
                deleteInstance = GameObject.Instantiate(deletePrefab) as GameObject;
                StartCoroutine("Select");
            }
            decision.clip = decision1;
            decision.Play();
            //if (fadeInInstance == null)
            //{
            //    fadeInInstance = GameObject.Instantiate(fadeInPrefab) as GameObject;
            //    StartCoroutine("End");
            //}
           
        }

    }

    public IEnumerator Select()
    {
        Debug.Log("tes");
        yield return new WaitForSeconds(0.2f);
        if (menuInstance == null)
        {
            menuInstance = GameObject.Instantiate(menuPrefab) as GameObject;
            
        }
    }
    public IEnumerator End()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Select");
    }
}
