using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAlphaController : MonoBehaviour {

    public CanvasGroup canvasGroup;
    public float fadeSpeed = 2f;

    public float highIntensity = 1f;
    public float lowIntensity = 0.0f;
    public float changeMargin = 0.2f;
    private bool UISwitchOn = false;

    private float targetIntensity;

    void Awake()
    {
        canvasGroup.alpha = 0f;
        targetIntensity = highIntensity;
    }

    public void FadeUp()
    {
        UISwitchOn = true;

    }


    void Update()
    {
        if (UISwitchOn)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetIntensity, fadeSpeed * Time.deltaTime);
             CheckTargetIntensity();
        }
        else
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0f, fadeSpeed * Time.deltaTime);
        }
    }

    void CheckTargetIntensity()
    {
        if (Mathf.Abs(targetIntensity - canvasGroup.alpha) < changeMargin)
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
