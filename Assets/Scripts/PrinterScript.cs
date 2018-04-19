using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterScript : MonoBehaviour {


    private float cooldownTime = 0.5f;

   public GameObject Paper;


    private float cooldown;
    private float cooldownLength = 1;

    private void Start()
    {
        
    }

    void FixedUpdate()
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
