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

    
    public static int Bgm;



    // Use this for initialization
    void Start()
    {
        Bgm = 0;
        Destroy(fadeOutInstance);
        Destroy(fadeInInstance);

        

        if (fadeOutInstance == null)
        {
            fadeOutInstance = GameObject.Instantiate(fadeOutPrefab) as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("A_BUTTON"))
        {
            if (fadeInInstance == null)
            {
                fadeInInstance = GameObject.Instantiate(fadeInPrefab) as GameObject;
                StartCoroutine("End");
            }
        }

    }

    public IEnumerator End()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Select");
    }
}
