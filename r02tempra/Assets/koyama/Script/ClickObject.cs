using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour

{
    //生成するもの
    public GameObject foam;
    //クリックした位置座標
    private Vector3 ClickPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClickMouseButton();
    }

    void ClickMouseButton()
    {
        //マウス入力で左クリックしたとき
        if (Input.GetMouseButtonDown(0))
        {
            ClickPosition = Input.mousePosition;

            ClickPosition.z = 10f;
            Instantiate(foam, Camera.main.ScreenToWorldPoint(ClickPosition), foam.transform.rotation);
        }
    }
}
