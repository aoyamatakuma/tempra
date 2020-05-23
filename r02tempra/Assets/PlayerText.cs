using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerText : MonoBehaviour 
{

    PlayerMove player;
    Text limitText;
    int num;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        limitText = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Calculation();
        limitText.text = num.ToString();
    }

    void Calculation()
    {
        num = player.babulimit - player.foamCount;
    }
}
