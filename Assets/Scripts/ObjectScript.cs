using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour {


    public FloorScript floor;   
    public AudioClip sfxImpact;
    public AudioClip sfxMoney;
    AudioSource audioSource;
    public GameObject particleSystemPrefab;




    private void Start()
    {
         audioSource = GetComponent<AudioSource>();
    }


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Hammer")
        {
            

            if (collision.relativeVelocity.magnitude > 2)
            {
                audioSource.PlayOneShot(sfxImpact);
                audioSource.PlayOneShot(sfxMoney);
                foreach (ContactPoint contact in collision.contacts)
                    {
                        Instantiate(particleSystemPrefab, contact.point, Quaternion.identity);
                    }

            }
        }
       

        if (collision.gameObject.tag == "Floor" && gameObject.tag == "FirstStatue")
        {
            //Debug.Log("Object hit floor");
            floor.PlayMusic();
            
        }

    }




}
