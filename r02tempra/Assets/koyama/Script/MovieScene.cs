using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MovieScene : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    [SerializeField]
    //フェードイン処理
    private GameObject fadeInPrefab;
    private GameObject fadeInInstance;
    [SerializeField]
    //フェードイン処理
    private GameObject fadeoutPrefab;
    private GameObject fadeoutInstance;

    bool Button;
    // Start is called before the first frame update
    void Start()
    {
        //フェードイン消去
        Destroy(fadeInInstance);
        StartCoroutine("MovieStartCole");
        Button = false;
    }

    // Update is called once per frame
    void Update()
    {
        //アイコンの表示非表示のフラグ設定
        if (Input.GetButtonDown("A_BUTTON")&&!Button)
        {
            StartCoroutine("MovieButtonSceneCole");
        }

    }
    public IEnumerator MovieStartCole()
    {

       

        Debug.Log("動画");



        //フェードイン処理
        fadeInInstance = GameObject.Instantiate(fadeInPrefab) as GameObject;
        yield return new WaitForSeconds(0.8f);
        videoPlayer = GameObject.Find("VideoPlane").GetComponent<VideoPlayer>();
        videoPlayer.Play();
        yield return new WaitForSeconds(11f);
        SceneManager.LoadScene("MasterTitle");

    }
    public IEnumerator MovieButtonSceneCole()
    {
        Button = true;
        fadeoutInstance = GameObject.Instantiate(fadeoutPrefab) as GameObject;
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("MasterTitle");
        

    }
    
}
