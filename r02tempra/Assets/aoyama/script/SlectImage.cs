using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlectImage : MonoBehaviour
{
    [SerializeField]
    private GameObject fadeInPrefab;
    private GameObject fadeInInstance;
    int sselect = 1;
    //se
    public AudioClip select1;
    public AudioClip decision1;
    private AudioSource select;
    private AudioSource decision;
    // Start is called before the first frame update
    void Start()
    {
        sselect = 1;
        if(sselect != 1)
        {
            transform.localPosition = new Vector3(-304, -280, 0);
        }
        select = gameObject.GetComponent<AudioSource>();
        decision = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
      
        if ( Input.GetAxis("Vertical") < -0.1f && sselect==1)
        {
            transform.localPosition = new Vector3(-304,-390, 0);
            sselect = sselect + 1;
            select.clip = select1;
            select.Play();
        }
        if (Input.GetAxis("Vertical") > 0.1f && sselect == 2)
        {
            transform.localPosition = new Vector3(-304, -280, 0);
            sselect = sselect - 1;
            select.clip = select1;
            select.Play();
        }
        if (Input.GetButtonDown("A_BUTTON"))
        {
            if (sselect == 1)
            {
                StartCoroutine("GameStart");
            }
            if (sselect == 2)
            {
                
            UnityEngine.Application.Quit();
            }
            decision.clip = decision1;
            decision.Play();
        }
    }
    public IEnumerator GameStart()
    {
        if (fadeInInstance == null)
        {
            fadeInInstance = GameObject.Instantiate(fadeInPrefab) as GameObject;
        }
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("select");
    }
}
