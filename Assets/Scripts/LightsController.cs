using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour {

    public Light light;
    public float fadeSpeed = 1f;

    public float highIntensity = 6f;
    public float lowIntensity = 0.0f;
    public float changeMargin = 0.2f;
    private bool lightison = false;

    public bool fadingDone = false;

    private float targetIntensity;

    void Awake()
    {
        light.intensity = 0f;
        targetIntensity = highIntensity;
    }

    public void FadeUp()
    {
        lightison = true;
        
    }

    void Update()
    {
        if (lightison)
        {
            light.intensity = Mathf.Lerp(light.intensity, targetIntensity, fadeSpeed * Time.deltaTime);
            CheckTargetIntensity();
        }
        else
        {
            light.intensity = Mathf.Lerp(light.intensity, 0f, fadeSpeed * Time.deltaTime);
           
        }
    }
    void CheckTargetIntensity()
    {
        if (Mathf.Abs(targetIntensity - light.intensity) < changeMargin)
        {
           

            if (targetIntensity == highIntensity)
            {
                
            }
            else
            {
                targetIntensity = highIntensity;
                
            }
        }

    }
}
