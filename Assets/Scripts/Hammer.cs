using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour {

    AudioSource audioSource;

    public GameObject particleSystemPrefab;

    public AudioClip sfxHammer;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(sfxHammer);

        foreach (ContactPoint contact in collision.contacts)
        {
          
                Instantiate(particleSystemPrefab, contact.point, Quaternion.identity);
         

        }


    }

}





