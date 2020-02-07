using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
   public GameObject player;
    Collider2D m_objectWall;
    // Start is called before the first frame update
    void Start()
    {
      
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player")
        {
           other.isTrigger = false;
            Debug.Log("通れるよ");
        }
        
    }
}
