using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleText : MonoBehaviour
{
    StageRule stage;
    Text bubbleText;
    // Start is called before the first frame update
    void Start()
    {
        bubbleText = gameObject.GetComponent<Text>();
        stage = transform.root.gameObject.GetComponent<StageRule>();
        
    }

    // Update is called once per frame
    void Update()
    {
        bubbleText.text = stage.current_bubble + "/" + stage.limit_bubble;
    }
}
