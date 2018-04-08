using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour {

    AudioSource audioSource;

    public AudioClip sfxMusic;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    public void PlayMusic()
    {
        audioSource.PlayOneShot(sfxMusic);
    }

}
