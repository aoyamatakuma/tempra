using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollision : MonoBehaviour
{

    StageRule stage;
    // Start is called before the first frame update
    void Start()
    {
        GameObject objColliderTriggerParent = gameObject.transform.parent.gameObject;
        stage = objColliderTriggerParent.GetComponent<StageRule>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        stage.HitStage(col);
    }
    void OnTriggerStay2D(Collider2D col)
    {
        stage.HitStage(col);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        stage.ExitStage(col);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
