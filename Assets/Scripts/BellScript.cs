using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellScript : MonoBehaviour {

    AudioSource audioSource;

    public AudioClip sfxBell;

    Rigidbody rb;

    public GameObject[] StartUiItems;

    

    public Level1Controller gameManager;

    private void Start()
    {
        StartUiItems = GameObject.FindGameObjectsWithTag("StartUI");
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Hammer")
        {
            rb.useGravity = true;
            audioSource.PlayOneShot(sfxBell);
            gameManager.StartLevel1();
            StartCoroutine(DestroyBell());
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
