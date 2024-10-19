using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    
    public IEnumerator Shake(float duration, float magnitude)
    {
        Debug.Log("SHAKE");
        // you can set the originalPos to transform.localPosition of the camera in that instance.
        Vector3 originalPos = transform.localPosition;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;

            transform.localPosition = new Vector3(transform.localPosition.x + xOffset, transform.localPosition.y + yOffset, originalPos.z);

            elapsedTime += Time.deltaTime;

            // wait one frame
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}

