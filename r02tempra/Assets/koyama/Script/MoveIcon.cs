using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveIcon : MonoBehaviour {
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
    //瞬間移動フラグ
    private static bool TeleportFlag;
    //アイコン取得
    Image Icon;
    // Start is called before the first frame update
    void Start () {

        rect = GetComponent<RectTransform> ();
        //アイコンのサイズの半分で設定
        Offset = new Vector2 (rect.sizeDelta.x / 2f, rect.sizeDelta.y / 2f);

        //動かないように設定
        //MoveFlag = false;
        //Debug.Log(MoveFlag);
        TeleportFlag=false;

        //アイコンを取得
        Icon = GameObject.Find ("Canvas/Panel/Icon").GetComponent<Image> ();
        Icon.enabled = false;

<<<<<<< HEAD
        
=======
        //プレイヤーを取得
        //m_Player = GameObject.Find ("Player");
>>>>>>> origin/ishibashi
    }

    // Update is called once per frame
    void Update () {
        IconMove ();

    }
      //枠内での当たり判定
    void OnTriggerEnter2D (Collider2D collider) {
    if (collider.gameObject.tag == "StageArea") {
        TeleportFlag=true;
             Debug.Log("atari");
             Debug.Log(TeleportFlag);
           
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "StageArea") {
        TeleportFlag=false;
             Debug.Log("No");
             Debug.Log(TeleportFlag);
           
        }
    }
    

    //アイコンの操作
    public void IconMove () {

        //アイコンの表示非表示のフラグ設定
        if (Input.GetButtonDown ("Y_BUTTON") && CameraCO.isCameraPos2) {
            //MoveFlag =true;
            //Debug.Log(MoveFlag);

            //アイコンの存在をONにする
            Icon.enabled = true;

        } else if (Input.GetButtonDown ("Y_BUTTON") && CameraCO.isCameraPos2 == false) {
            //MoveFlag = false;
            //Debug.Log(MoveFlag);

            //アイコンの存在をOFFにする
            Icon.enabled = false;
        }

        if (CameraCO.isCameraPos2 == false) {
            //移動キーが押されてなければ何もしない 2 つの浮動小数点値を比較し、近似している場合は true を返します
            if (Mathf.Approximately (Input.GetAxis ("Horizontal"), 0f) && Mathf.Approximately (Input.GetAxis ("Vertical"), 0f)) {
                return;
            }

            //移動先を計算
            var pos = rect.anchoredPosition + new Vector2 (Input.GetAxis ("Horizontal") * iconSpeed, Input.GetAxis ("Vertical") * iconSpeed) * Time.deltaTime;

            //アイコンが画面外に出ないようにする
            pos.x = Mathf.Clamp (pos.x, -Screen.width * 0.5f + Offset.x, Screen.width * 0.5f - Offset.x);
            pos.y = Mathf.Clamp (pos.y, -Screen.height * 0.5f + Offset.y, Screen.height * 0.5f - Offset.y);

            //アイコン位置を設定
            rect.anchoredPosition = pos;
            
            if (Input.GetButtonDown ("B_BUTTON")&&TeleportFlag) {
                Teleport ();
                 Debug.Log("テレポート");
            }

        }
    }

    //プレイヤーテレポート
    public void Teleport () {
        //Bボタンを押したときに発動
        if (Input.GetButtonDown ("B_BUTTON")) {
        //Iconの位置を取得する
        var IconPos = GameObject.Find ("Icon").transform.position;
        IconPos.z=0;
        //アイコンの位置にプレイヤーの位置を移動させる
        m_Player.transform.position = IconPos;
        //Debug.Log (m_Player.transform.position);
        
        

        }
    }

  

}