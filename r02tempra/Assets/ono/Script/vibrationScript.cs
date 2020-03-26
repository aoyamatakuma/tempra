using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vibrationScript : MonoBehaviour
{
    public void vibration(float vibrationTime, float vibrationScale)
    {
        //コールチンを開始
        StartCoroutine(DoShake(vibrationTime, vibrationScale));
    }

    private IEnumerator DoShake(float vibrationTime, float vibrationScale)
    {
        Vector3 pos = transform.localPosition;

        float elapsedTime = 0f;

        while (elapsedTime < vibrationTime)
        {
            float x = pos.x + Random.Range(-1f, 1f) * vibrationScale;
            float y = pos.y + Random.Range(-1f, 1f) * vibrationScale;

            transform.localPosition = new Vector3(x, y, pos.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = pos;
    }
}
