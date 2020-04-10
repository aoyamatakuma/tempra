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
    private Vector3 offset= new Vector3(0,2,0);

    [SerializeField]
    Vector3 cameraVec;
    public bool isCameraPos1;
    public bool isCameraPos2;
    private static int intA;
   
    void Start()
    {
        isCameraPos1 = true;
        isCameraPos2 = false; 
        basePosition = playerTrans.position;
        intA = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (intA)
        {
            case 0:
                isCameraPos1 = false;
                isCameraPos2 = true;
                break;
            case 1:
                isCameraPos1 = true;
                isCameraPos2 = false;
                break;
        }

        if (Input.GetKeyDown(KeyCode.Y) || Input.GetButtonDown("Y_BUTTON"))
        {
            intA++;
            if (intA >= 2) { intA = 0; }
        }
        if (isCameraPos1)
        {
            CameraLerp(new Vector3(transform.position.x, posY, posZ));
        }
            
        else if (isCameraPos2)
        {
           
            CameraLerp(new Vector3(playerTrans.position.x, playerTrans.position.y + offset.y, -11));
        }
    }

    //カメラが動く処理
    void CameraLerp(Vector3 move)
    {
        // transform.position = Vector3.Lerp(transform.position, move, speed * Time.deltaTime);
        transform.position = move;
    }


}
