using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class awamove : MonoBehaviour
{
    float angle = 0;
    public float range = 1f;//幅
    float speed = 0.04f;//はやさ

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 sin = transform.position;
        sin.x += Mathf.Sin(angle) * range;
        sin.y += 0.01f;
        angle += speed;
        transform.position = sin;

    }
}
