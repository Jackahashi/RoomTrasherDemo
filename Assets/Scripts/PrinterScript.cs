using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterScript : MonoBehaviour {


    private float cooldownTime = 0.5f;
    public GameObject Paper;
    public AudioSource audioSource;
    private float cooldown;
    private float cooldownLength = 1.3f;
    

    private void OnEnable()
    {
        audioSource.Play();
        
    }

    private void OnDisable()
    {
        audioSource.Stop();
    }

    void Update()
    {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                cooldown = cooldownLength;
                InstantiatePaper();
            }
    }

    void InstantiatePaper()
    {
       Instantiate(Paper, gameObject.transform);
    }
}
