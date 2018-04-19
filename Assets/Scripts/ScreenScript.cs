using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ScreenScript : MonoBehaviour {

    public GameObject videoPanel;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1)
        {
            videoPanel.SetActive(false);
        }
    }


}
