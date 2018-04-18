using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour {

    AudioSource audioSource;

    public GameObject particleSystemPrefab;

    public AudioClip sfxHammer;

    void Start () {
        audioSource = GetComponent<AudioSource>();
       
    }


        void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(sfxHammer);
        //device.TriggerHapticPulse(2000);
        foreach (ContactPoint contact in collision.contacts)
        {
            Instantiate(particleSystemPrefab, contact.point, Quaternion.identity);
        }
    }


}





