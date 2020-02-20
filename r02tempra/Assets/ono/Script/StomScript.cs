using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StomScript : MonoBehaviour
{

    private static bool airMove;

    public Rigidbody2D Playerrb;

    public float airPowerX;
    public float airPowerY;



    // Use this for initialization
    void Start()
    {
        airMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        Air();
    }

    void Air()
    {
        if (airMove)
        {
            Playerrb.AddForce(Vector2.right * airPowerX);
            Playerrb.AddForce(Vector2.up * airPowerY);

            //Debug.Log(airMove);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            airMove = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            airMove = false;
    }
}
