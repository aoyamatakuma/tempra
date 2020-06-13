using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveIcon : MonoBehaviour
{
    [SerializeField]
    private CameraControl CameraCO;
    [SerializeField]
    private GameObject m_Player;
    [SerializeField]
    private StageRule Stage;

    [SerializeField]
    private GameObject explosionEffect;
    [SerializeField]
    //アイコン取得
    private Image Icon;

    //画像取得
    public Image Teleportimage;
    public Sprite TeleportOnImage;
    public Sprite TeleportOffImage;


    //テキスト取得
    public Text text;
    //アイコンが１秒間に何ピクセル移動するか
    public float iconSpeed = Screen.width;

    //アイコンのサイズを取得
    private static RectTransform rect;

    //アイコンが画面内に収まる
    private static Vector2 Offset;

    //動くフラグ
    private static bool MoveFlag;
    //瞬間移動フラグ
    private static bool TeleportFlag;

    //泡破裂フラグ
    private static bool AwaExplosion;

   
    private GameRule gameRule;
    [SerializeField]
    private PlayerMove player;
    [SerializeField]
    private PlayerHeadMove playerhead;
    // Start is called before the first frame update
    void Start()
    {

        rect = GetComponent<RectTransform>();

        //アイコンのサイズ設定
        Offset = new Vector2(rect.sizeDelta.x/2f , rect.sizeDelta.y/2f);

        //動かないように設定
        //MoveFlag = false;
        //Debug.Log(MoveFlag);
        TeleportFlag = false;

        //テキスト非表示
        //text.enabled = false;

        //画像非表示
        Teleportimage.enabled = false;

        //awaFalse
        AwaExplosion = false;

        gameRule = GameObject.Find("StageManager").GetComponent<GameRule>();
        //アイコンを取得
        Icon = GameObject.Find("PausableObjects/Canvas/Panel/Icon").GetComponent<Image>();
        Icon.enabled = false;

        //プレイヤーを取得
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        playerhead = GameObject.FindGameObjectWithTag("PlayerHead").GetComponent<PlayerHeadMove>();
    }

    // Update is called once per frame
    void Update()
    {
        IconMove();
        Explosion();

    }
    //枠内での当たり判定
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "StageBox")
        {
            //テレポートフラグを立てる
            TeleportFlag = true;
            AwaExplosion = true;
            //text.text = "テレポート可能";

            //画像読み込み変更
            Teleportimage.gameObject.GetComponent<Image>().sprite = TeleportOnImage;

            //ステージスクリプトを取得する
            Stage = collider.gameObject.GetComponent<StageRule>();

            //Debug.Log (Stage.current_bubble);

        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "StageBox")
        {
            //フラグオフにする
            TeleportFlag = false;
            AwaExplosion = false;

            //画像読み込み変更
            Teleportimage.gameObject.GetComponent<Image>().sprite = TeleportOffImage;

            //text.text = "テレポート不可能";
            //Debug.Log ("No");


        }
    }

    //アイコンの操作
    public void IconMove()
    {

        //アイコンの表示非表示のフラグ設定
        if (Input.GetButtonDown("Y_BUTTON") && CameraCO.isCameraPos2 && player.currentPlayerState != PlayerState.Warp)
        {
            //MoveFlag =true;
            //Debug.Log(MoveFlag);

            //アイコンの存在をONにする
            Icon.enabled = true;
            //text.enabled = true;
            Teleportimage.enabled = true;

        }
        else if (Input.GetButtonDown("Y_BUTTON") && CameraCO.isCameraPos2 == false)
        {
            //MoveFlag = false;
            //Debug.Log(MoveFlag);

            //アイコンの存在をOFFにする
            Icon.enabled = false;
            //text.enabled = false;
            Teleportimage.enabled = false;
        }

        if (CameraCO.isCameraPos2 == false)
        {
            //移動キーが押されてなければ何もしない 2 つの浮動小数点値を比較し、近似している場合は true を返します
            //if (Mathf.Approximately (Input.GetAxis ("Horizontal"), 0f) && Mathf.Approximately (Input.GetAxis ("Vertical"), 0f) && (Input.GetButtonDown ("B_BUTTON"))) {

            //return;
            // }

            //移動先を計算
            var pos = rect.anchoredPosition + new Vector2(Input.GetAxis("Horizontal") * iconSpeed, Input.GetAxis("Vertical") * iconSpeed) * Time.deltaTime;

            //アイコンが画面外に出ないようにする
            pos.x = Mathf.Clamp(pos.x, -Screen.width * 0.5f + Offset.x, Screen.width * 0.5f - Offset.x);
            pos.y = Mathf.Clamp(pos.y, -Screen.height * 0.5f + Offset.y, Screen.height * 0.5f - Offset.y);

            //アイコン位置を設定
            rect.anchoredPosition = pos;
            if (Input.GetButtonDown("B_BUTTON") && TeleportFlag)
            {
                Teleport();
            }
        }
    }
    //プレイヤーテレポート
    public void Teleport()
    {

        //Iconの位置を取得する
        var IconPos = GameObject.Find("Icon").transform.position;
        IconPos.z = 0;
        //アイコンの位置にプレイヤーの位置を移動させる
        m_Player.transform.position = IconPos;
        //Debug.Log (m_Player.transform.position);

    }
    //爆発
    public void Explosion()
    {

        if (Input.GetButtonDown("A_BUTTON") && AwaExplosion && CameraCO.isCameraPos2 == false && gameRule.getIsPlay() && player.currentPlayerState == PlayerState.Division)
        {
            if (!player.awaCreate || !player.goalAwaDelete)
            {
                //配列処理
                foreach (var a in Stage.Bubblehub)
                {
                    //nullチェック
                    if (a != null)
                    {
                        Instantiate(explosionEffect, transform.position, Quaternion.identity);
                        Destroy(a);
                    }
                }
                //要素削除
                for (int i = 0; i < Stage.Bubblehub.Count; i++)
                {
                    Stage.Bubblehub.RemoveAt(i);
                }
                //ステージのバブルのカウント表示
                Stage.Minus();

            }
        }

        if (Input.GetButtonDown("A_BUTTON") && AwaExplosion && CameraCO.isCameraPos2 == false && gameRule.getIsPlay() && player.currentPlayerState == PlayerState.Head)
        {
            if (!playerhead.headAwaCreate || !playerhead.headGoalAwaDelete)
            {
                //配列処理
                foreach (var a in Stage.Bubblehub)
                {
                    //nullチェック
                    if (a != null)
                    {
                        Instantiate(explosionEffect, transform.position, Quaternion.identity);
                        Destroy(a);
                    }
                }
                //要素削除
                for (int i = 0; i < Stage.Bubblehub.Count; i++)
                {
                    Stage.Bubblehub.RemoveAt(i);
                }
                //ステージのバブルのカウント表示
                Stage.Minus();

            }
        }
    }
}