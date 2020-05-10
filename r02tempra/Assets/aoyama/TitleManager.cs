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

    public AudioClip select1;
    public AudioClip select2;
    private AudioSource audioSource;
    public static int Bgm;



    // Use this for initialization
    void Start()
    {
        Bgm = 0;
        Destroy(fadeOutInstance);
        Destroy(fadeInInstance);

        audioSource = gameObject.GetComponent<AudioSource>();

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
                audioSource.clip = select1;
                audioSource.Play();
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
