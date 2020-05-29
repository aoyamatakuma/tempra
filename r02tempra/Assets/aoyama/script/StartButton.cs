using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private GameObject startPrefab;
    private GameObject startInstance;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("GamePad_A"))
        {
            if (startInstance == null)
            {
                startInstance = GameObject.Instantiate(startPrefab) as GameObject;
                Destroy(this.gameObject);
            }
        }
    }
}
