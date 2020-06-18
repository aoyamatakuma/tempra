using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class awamove : MonoBehaviour
{
    float angle = 0;
    public float range = 1f;//幅
    [SerializeField]
   private float speed = 0.04f;//はやさ
    [SerializeField]
    private float speedY = 0.01f;

    [SerializeField]
    private GameObject explosionEffect;

    //停止フラグ
    private bool stop;
   

    // Start is called before the first frame update
    void Start()
    {
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            Vector2 sin = transform.position;
            sin.x += Mathf.Sin(angle) * range;
            sin.y += speedY;
            angle += speed;
            transform.position = sin;
        }
    }


    public void Explosions()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
       // vib.vibration(vibrationTime, vibrationScale);
    }

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.CompareTag("Collision"))
    //    {
    //        stop = true;
    //    }
    //}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.Find("stage_soto"))
        {
            stop = true;
        }
    }
}
