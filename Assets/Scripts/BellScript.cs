using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellScript : MonoBehaviour {

    AudioSource audioSource;

    public AudioClip sfxBell;

    Rigidbody rb;

    public GameObject[] StartUiItems;


    private bool beenHit;
    public GameManager gameManager;

    private void Start()
    {
        StartUiItems = GameObject.FindGameObjectsWithTag("StartUI");
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        beenHit = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if (!(beenHit))
        {
            if (other.relativeVelocity.magnitude > 0.5f)
            {
                beenHit = true;
                rb.useGravity = true;
                audioSource.PlayOneShot(sfxBell);
                gameManager.ResetTimer();
                StartCoroutine(DestroyBell());
            }
        }
    }


    IEnumerator DestroyBell()
    {
        
        foreach (GameObject item in StartUiItems)
        {
            item.SetActive(false);
        }
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);


    }
}
