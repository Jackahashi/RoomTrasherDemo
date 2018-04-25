using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellScript : MonoBehaviour {

    AudioSource audioSource;
    Rigidbody rb;
    private bool beenHit;
    public GameManager gameManager;
    public GameObject[] StartUiItems;
    public AudioClip sfxBell;
    public AudioClip sfxIntroMusic;
    Vector3 startPosition;
    Quaternion startRotation;
    public AudioSource MusicAudio;
    

    private void Start()
    {
        
        StartUiItems = GameObject.FindGameObjectsWithTag("StartUI");
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        startPosition = gameObject.transform.position;
        startRotation = gameObject.transform.rotation;
        
    }

    private void OnEnable()
    {
        beenHit = false;
        foreach (GameObject item in StartUiItems)
        {
            item.SetActive(true);
        }
        MusicAudio.PlayOneShot(sfxIntroMusic);
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (!(beenHit))
        {
                //Debug.Log("HitAndGravityShouldChange");
                beenHit = true;
                rb.useGravity = true;
                audioSource.PlayOneShot(sfxBell);
                gameManager.ResetTimer();
                StartCoroutine(DestroyBell());
            StartCoroutine(AudioFadeOut.FadeOut(MusicAudio, 3));
        }
    }


    IEnumerator DestroyBell()
    {
        
        foreach (GameObject item in StartUiItems)
        {
            item.SetActive(false);
        }
        yield return new WaitForSeconds(3);
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.transform.position = startPosition;
        gameObject.transform.rotation = startRotation;
        gameObject.SetActive(false);



        // make sure velocity is zero so that it cant move unless hit by hammer
        // currently the bell is being given velocity somehow

    }


}
