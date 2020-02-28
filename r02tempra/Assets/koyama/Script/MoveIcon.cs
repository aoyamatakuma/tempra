using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveIcon : MonoBehaviour
{
    [SerializeField]
    public CameraControl CameraCO;
    public GameObject m_Player;
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

        //プレイヤーを取得
        m_Player=GameObject.Find("Player");
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
            //Debug.Log(MoveFlag);

            //アイコンの存在をONにする
            Icon.enabled = true;

        }
        else if(Input.GetButtonDown("Y_BUTTON") &&CameraCO.isCameraPos2 == false)
        {
            //MoveFlag = false;
            //Debug.Log(MoveFlag);

            //アイコンの存在をOFFにする
            Icon.enabled = false;
        }

        if(CameraCO.isCameraPos2 == false)
        {
            //移動キーが押されてなければ何もしない 2 つの浮動小数点値を比較し、近似している場合は true を返します
            if (Mathf.Approximately(Input.GetAxis("Horizontal"), 0f) && Mathf.Approximately(Input.GetAxis("Vertical"), 0f))
            {
               return;
            }
           

            //移動先を計算
            var pos = rect.anchoredPosition + new Vector2(Input.GetAxis("Horizontal") * iconSpeed, Input.GetAxis("Vertical") * iconSpeed) * Time.deltaTime;

            //アイコンが画面外に出ないようにする
            pos.x = Mathf.Clamp(pos.x, -Screen.width * 0.5f + Offset.x, Screen.width * 0.5f - Offset.x);
            pos.y = Mathf.Clamp(pos.y, -Screen.height * 0.5f + Offset.y, Screen.height * 0.5f - Offset.y);

            //アイコン位置を設定
            rect.anchoredPosition = pos;
            Teleport();
        }
    }


    public void Teleport()
    {
        //Bボタンを押したときに発動
        if(Input.GetButtonDown("B_BUTTON"))
        {
            //Iconの位置を取得する
            var IconPos=GameObject.Find("Icon").transform.position;
            //アイコンの位置にプレイヤーの位置を移動させる
            m_Player.transform.position=IconPos;
        }
    } 
}
