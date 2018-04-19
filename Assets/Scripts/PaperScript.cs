using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperScript : MonoBehaviour {


    Rigidbody rb;

    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update () {
        if(transform.localPosition.x > -1f)
        {
            transform.Translate(-0.4f * Time.deltaTime, 0, 0, Space.Self);
        }
        else
        {
            rb.isKinematic = false;
            gameObject.GetComponent<PaperScript>().enabled = false;
        }

    }
}
