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
    // Start is called before the first frame update
    void Start()
    {
        sselect = 1;
        if(sselect != 1)
        {
            transform.localPosition = new Vector3(-304, -280, 0);
        }


    }

    // Update is called once per frame
    void Update()
    {
      
        if ( Input.GetAxis("Vertical") < -0.1f && sselect==1)
        {
            transform.localPosition = new Vector3(-304,-390, 0);
            sselect = sselect + 1;
        }
        if (Input.GetAxis("Vertical") > 0.1f && sselect == 2)
        {
            transform.localPosition = new Vector3(-304, -280, 0);
            sselect = sselect - 1;
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
