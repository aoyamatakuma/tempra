using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour {

    public Image stage1;
    public Image stage2;
    

    private Color SelectOn = new Color(255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
    private Color SelectOff = new Color(90 / 255.0f, 90 / 255.0f, 90 / 255.0f);

    
    int cntStage;
    // Use this for initialization
    void Start () {

        cntStage = 1;
	}
	
	// Update is called once per frame
	void Update () {
        DrawStageSelect();
        Select();
		if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            cntStage++;
            if(cntStage >=1)
            {
                cntStage = 1;
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cntStage--;
            if(cntStage<=0)
            {
                cntStage = 0;
            }
        }

	}

    void DrawStageSelect()
    {
        if(cntStage ==0)
        {
            stage1.GetComponent<Image>().color = SelectOn;
            stage2.GetComponent<Image>().color = SelectOff;
            
        }
        else 
        {
            stage1.GetComponent<Image>().color = SelectOff;
            stage2.GetComponent<Image>().color = SelectOn;
         
        }

    }

    void Select()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (cntStage == 0)
            {
                SceneManager.LoadScene("Stage1");
            }
            else 
            {
                SceneManager.LoadScene("Stage2");
            }
   
        }
    }

}
