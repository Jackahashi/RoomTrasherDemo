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
            transform.parent = null;
            rb.isKinematic = false;
            box.enabled = true;
            gameObject.GetComponent<PaperScript>().enabled = false;
            StartCoroutine(KillPaper());
        }

    }
    IEnumerator KillPaper()
    {
        yield return new WaitForSeconds(4);
        gameObject.SetActive(false);
    }
}
