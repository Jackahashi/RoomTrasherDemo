using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderAlphaController : MonoBehaviour {

    public Component[] myRenderers;

    // Use this for initialization
    void Start () {

        myRenderers = GetComponentsInChildren<Renderer>();
		
	}

    // for test use only 
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            FadeTheRenderers();
        }
    }


    public void FadeTheRenderers()
    {
        foreach (Renderer rend in myRenderers)
        {
            StartCoroutine(FadeTo(rend,0.0f, 1.0f));
        }
    }



        IEnumerator FadeTo(Renderer rend,float aValue, float aTime)
    {
      
            float alpha = rend.material.color.a;
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
            {
                Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
                rend.material.color = newColor;
                yield return null;
            }

        
    }
}
