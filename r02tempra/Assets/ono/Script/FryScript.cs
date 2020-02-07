using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryScript : MonoBehaviour
{

    float angle = 0;
    public float range = 1f;//幅
    float yspeed = 0.04f;//はやさ

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 sin = transform.position;
        sin.y += Mathf.Sin(angle) * range;
        angle += yspeed;
        transform.position = sin;
      
    }
}
