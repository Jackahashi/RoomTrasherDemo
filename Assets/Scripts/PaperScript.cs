using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperScript : MonoBehaviour {


    Rigidbody rb;

    BoxCollider box;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        box = GetComponent<BoxCollider>();
    }


    void Update () {
        if(transform.localPosition.x > -1f)
        {
            transform.Translate(-0.4f * Time.deltaTime, 0, 0, Space.Self);
        }
        else
        {
            rb.isKinematic = false;
            box.enabled = true;
            gameObject.GetComponent<PaperScript>().enabled = false;
        }

    }
}
