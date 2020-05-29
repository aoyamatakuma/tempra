using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleStart : MonoBehaviour
{

    [SerializeField]
    private GameObject fadeInPrefab;
    private GameObject fadeInInstance;

    bool isEnd;

    Animator anim;
    int cntPause;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(fadeInInstance);
        anim = GetComponent<Animator>();
        cntPause = 0;
        anim.SetTrigger("On");
        isEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnd==true)
        {
            return;
        }
        if (Input.GetAxis("Vertical") > 0.9f)
        {
            cntPause--;
            if (cntPause == 0)
            {
                anim.SetTrigger("On");
            }
            if (cntPause <= 0)
            {
                cntPause = 0;
            }
        }
        if (Input.GetAxis("Vertical") < -0.9f)
        {
            cntPause++;

            if (cntPause == 1)
            {
                anim.SetTrigger("Off");
            }
            if (cntPause >= 1)
            {
                cntPause = 1;
            }
        }

        if (cntPause == 0 && Input.GetButtonDown("GamePad_A"))
        {
            //SceneManager.LoadScene("Entry");]
            if (fadeInInstance == null)
            {
                fadeInInstance = GameObject.Instantiate(fadeInPrefab) as GameObject;
            }
                StartCoroutine("End");
        }
        if (Input.GetButtonDown("GamePad_A"))
        {
            isEnd = true;
        }
    }
    public IEnumerator End()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Entry");
    }
}
