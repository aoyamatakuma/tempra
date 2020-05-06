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
    private GameObject explosionEffect;

    GameObject camera;
    vibrationScript vib;

    public float vibrationTime;
    public float vibrationScale;
   

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        vib = camera.GetComponent<vibrationScript>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 sin = transform.position;
        sin.x += Mathf.Sin(angle) * range;
        sin.y += 0.01f;
        angle += speed;
        transform.position = sin;

        if (Input.GetKey(KeyCode.Q))
        {
            Explosions();
        }
    }


    public void Explosions()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
       // vib.vibration(vibrationTime, vibrationScale);
    }
}
