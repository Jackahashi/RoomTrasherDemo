using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ScreenScript : MonoBehaviour {

    public GameObject videoPanel;

    private VideoPlayer videoplayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 0.5f)
        {
            videoPanel.SetActive(false);
            
        }
    }





}
