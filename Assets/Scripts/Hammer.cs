using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour {

    AudioSource audioSource;
    BoxCollider hammerCollider;

    public GameObject particleSystemPrefab;

    public AudioClip sfxHammer;

    void Start () {
        audioSource = GetComponent<AudioSource>();
        hammerCollider = GetComponent<BoxCollider>();
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





