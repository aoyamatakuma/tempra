using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleText : MonoBehaviour
{
    StageRule stage;
    Text bubbleText;
    [SerializeField]
    private SpriteRenderer enn;
    bool isChange;
    // Start is called before the first frame update
    void Start()
    {
        bubbleText = gameObject.GetComponent<Text>();
        stage = transform.root.gameObject.GetComponent<StageRule>();
        isChange = false;
    }

    // Update is called once per frame
    void Update()
    {
        bubbleText.text = stage.current_bubble + "/" + stage.limit_bubble;
        ColorChange();
        boolText();
    }

    void ColorChange()
    {
        if (isChange)
        {
            enn.color = new Color(255, 235, 0, 255);
        }
        else
        {
            enn.color = new Color(255, 255, 255, 255);
        }
    }

    void boolText()
    {
        if (stage.current_bubble == stage.limit_bubble)
        {
            isChange = true;
        }
        else
        {
            isChange = false;
        }
    }
}
