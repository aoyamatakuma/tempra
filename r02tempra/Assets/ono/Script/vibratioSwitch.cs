using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vibratioSwitch : MonoBehaviour
{
    public vibrationScript vib;

    public float vibrationTime;
    public float vibrationScale;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            vib.vibration(vibrationTime, vibrationScale);
        }
    }
}
