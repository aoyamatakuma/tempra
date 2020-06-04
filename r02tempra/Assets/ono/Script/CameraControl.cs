using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
   　//カメラが近づく速度
     public float speed = 7.0f;//切り替え速度
    //初期位置
    private static Vector3 basePosition;//メイン時のカメラ位置
    [SerializeField]
    private float posY =-6;
    [SerializeField]
    private float posZ = -90;


    [SerializeField]
    Transform playerTrans;
    [SerializeField]
    PlayerMove player;

    [SerializeField]
    private Vector3 offset= new Vector3(0,2,0);

    [SerializeField]
    Vector3 cameraVec;
    public bool isCameraPos1;
    public bool isCameraPos2;
    private static int intA;

    GameObject stage;

    [SerializeField]
    private GameObject cameraPoint;

    //青山追加
    [SerializeField]
    private GameObject zoomInNaviPrefab;
    private GameObject zoomInNaviInstance;

    [SerializeField]
    private GameObject zoomOutNaviPrefab;
    private GameObject zoomOutNaviInstance;
    private Camera mainCam;


    void Start()
    {
        mainCam = gameObject.GetComponent<Camera>();
        isCameraPos1 = true;
        isCameraPos2 = false;
        zoomInNaviInstance = Instantiate(zoomInNaviPrefab);
        zoomOutNaviInstance = Instantiate(zoomOutNaviPrefab);
        zoomInNaviInstance.gameObject.SetActive(false);
        zoomOutNaviInstance.gameObject.SetActive(true);
        basePosition = playerTrans.position;
        transform.position = cameraPoint.transform.position;
        //   intA = 0;
        //始まったらズームアウト
        intA = 1;
        Invoke("StartTime", 2f);
    }
    //始まったらズームアウト
    void StartTime()
    {
        player.SetCurrentState(PlayerState.Normal);
        intA = 0;
    }

    void Update()
    {
        ChangeMode();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
      
        if (isCameraPos1)
        {
         
            //ここいじる（カメラ操作）
            CameraLerp(new Vector3(cameraPoint.transform.position.x, cameraPoint.transform.position.y, cameraPoint.transform.position.z));        
        }
            
        else if (isCameraPos2)
        {
            
            CameraLerp(new Vector3(playerTrans.position.x, playerTrans.position.y + offset.y, -11 + offset.z));
        }
    }

    //カメラが動く処理
    void CameraLerp(Vector3 move)
    {
        transform.position = Vector3.Lerp(transform.position, move, speed * Time.deltaTime);
        //transform.position = move;
    }

    void ChangeMode()
    {
        switch (intA)
        {
            case 0:
                isCameraPos1 = false;
                isCameraPos2 = true;
                zoomInNaviInstance.gameObject.SetActive(true);
                zoomOutNaviInstance.gameObject.SetActive(false);
                break;
            case 1:
                isCameraPos1 = true;
                isCameraPos2 = false;
                zoomInNaviInstance.gameObject.SetActive(false);
                zoomOutNaviInstance.gameObject.SetActive(true);
                break;
        }

        if (Input.GetKeyDown(KeyCode.Y) || Input.GetButtonDown("Y_BUTTON"))
        {
            intA++;
            if (intA >= 2) { intA = 0; }
        }
    }
}
