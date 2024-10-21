using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.U2D;

using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class lightsFlicker : MonoBehaviour
{
    public float intensity;
    public float delay;
    public float timeBetween;
    float elapsedTime;
    Light2D currentLight;
    // Start is called before the first frame update
    void Start()
    {
        currentLight = GetComponent<Light2D>();
        elapsedTime = 0f;
        intensity = currentLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime > timeBetween) 
        {
            StartCoroutine(flicker());
            elapsedTime = 0f;
        }
        elapsedTime += Time.deltaTime;
    }
    IEnumerator flicker()
    {
        float elTime = 0f;
        while (elTime < delay)
        {
            GetComponent<Light2D>().intensity = Random.Range(0, intensity);
            elTime += Time.deltaTime;
            yield return null;
        }
        GetComponent<Light2D>().intensity = intensity;
        timeBetween = Random.Range(0, 4f);
        
    }
}
