using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour {


    public FloorScript floor;

    public AudioClip sfxMusic;
    public AudioClip sfxFloor;
    public AudioClip sfxHammer;

    AudioSource audioSource;

    public GameObject particleSystemPrefab;

    private void Start()
    {
         audioSource = GetComponent<AudioSource>();
    }


    void OnCollisionEnter(Collision collision)
    {
        
            foreach (ContactPoint contact in collision.contacts)
            {
            if (collision.relativeVelocity.magnitude > 2)
            {
                Instantiate(particleSystemPrefab, contact.point, Quaternion.identity);
            }
           
            }


            if (collision.gameObject.tag == "Hammer")
            {
                //Debug.Log("the hammer hit the object");
                audioSource.PlayOneShot(sfxHammer);
            }
            else if (collision.gameObject.tag == "Floor" && collision.relativeVelocity.magnitude > 0.5f)
            {
            Debug.Log("Object hit floor");
                audioSource.PlayOneShot(sfxMusic);
                audioSource.PlayOneShot(sfxFloor);
            }



       


           
        
          
    }




}
