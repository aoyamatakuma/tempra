using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public float readyCount;

    [System.NonSerialized]
    public float GameStart=0;

    [SerializeField]//ポーズ画面のUI
    private GameObject CountDownPrefab;
    private GameObject CountDownUIInstance;//ポーズUIのインスタンス
    // Use this for initialization
    public IEnumerator Start()
    {
        if (CountDownUIInstance == null)
        {
            CountDownUIInstance = GameObject.Instantiate(CountDownPrefab) as GameObject;
        }

        yield return new WaitForSeconds(3);
        GameStart = 1;
        yield return new WaitForSeconds(1);
        Destroy(CountDownUIInstance);
    }

    // Update is called once per frame
    void Update()
    {
    }
    
}
