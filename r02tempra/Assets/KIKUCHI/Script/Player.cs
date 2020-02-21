using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed =0.1f;
    SpriteRenderer sprite;
   
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }


    void Move()
    {
        Vector2 position = transform.position;
        if (Input.GetKey(KeyCode.D))
        {
            position.x += moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            position.x -= moveSpeed;
        }
        transform.position = position;
    }
    void ModeChange()
    {
        sprite.color = new Color(0, 0, 0, 1);
       
    }
  

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Abura"))
        {
            ModeChange();
        }
    } 
}
