using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoalBlock : MonoBehaviour
{
    void Start()
    {
       
    }

    void Update() { }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag =="Player")
        {
     
            SceneManager.LoadScene("MasterGoal");
        }
    }
}
