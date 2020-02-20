using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScript : MonoBehaviour
{
    void Update()
    {
        float scroll = Mathf.Repeat(Time.time * 0.1f, 1);
        Vector2 offset = new Vector2(-scroll, -scroll);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
