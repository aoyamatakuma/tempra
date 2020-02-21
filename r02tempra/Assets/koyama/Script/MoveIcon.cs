using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveIcon : MonoBehaviour
{
    [SerializeField]
    public CameraControl CameraCO;
    //アイコンが１秒間に何ピクセル移動するか
    private float iconSpeed = Screen.width;

    //アイコンのサイズを取得
    private static RectTransform rect;

    //アイコンが画面内に収まる
    private static Vector2 Offset;
    
    //動くフラグ
    private static bool MoveFlag;


    Image Icon;
    // Start is called before the first frame update
    void Start()
    {
     
        rect = GetComponent<RectTransform>();
        //アイコンのサイズの半分で設定
        Offset = new Vector2(rect.sizeDelta.x / 2f, rect.sizeDelta.y / 2f);
        
        //動かないように設定
        //MoveFlag = false;
        //Debug.Log(MoveFlag);

        //アイコンを取得
        Icon = GameObject.Find("Canvas/Panel/Icon").GetComponent<Image>();
        Icon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        IconMove();
     
    }

    //アイコンの操作
    public void IconMove()
    {
        
        //アイコンの表示非表示のフラグ設定
        if (Input.GetButtonDown("Y_BUTTON") &&CameraCO.isCameraPos2)
        {
            //MoveFlag =true;
            Debug.Log(MoveFlag);
            Icon.enabled = true;

        }
        else if(Input.GetButtonDown("Y_BUTTON") &&CameraCO.isCameraPos2 == false)
        {
            //MoveFlag = false;
            Debug.Log(MoveFlag);
            Icon.enabled = false;
        }

        if(CameraCO.isCameraPos2 == false)
        {
            //移動キーが押されてなければ何もしない
            if (Mathf.Approximately(Input.GetAxis("Horizontal"), 0f) && Mathf.Approximately(Input.GetAxis("Vertical"), 0f))
            {
               return;
            }
           

            //移動先を計算
            var pos = rect.anchoredPosition + new Vector2(Input.GetAxis("Horizontal") * iconSpeed, Input.GetAxis("Vertical") * iconSpeed) * Time.deltaTime;

            //　アイコンが画面外に出ないようにする
            pos.x = Mathf.Clamp(pos.x, -Screen.width * 0.5f + Offset.x, Screen.width * 0.5f - Offset.x);
            pos.y = Mathf.Clamp(pos.y, -Screen.height * 0.5f + Offset.y, Screen.height * 0.5f - Offset.y);

            //　アイコン位置を設定
            rect.anchoredPosition = pos;

           
          
        }
    }
}
