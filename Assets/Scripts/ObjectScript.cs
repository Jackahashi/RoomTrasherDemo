using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour {


    public FloorScript floor;   
    public AudioClip sfxImpact;
    public AudioClip sfxMoney;
    AudioSource audioSource;
    public GameObject particleSystemPrefab;
    public GameObject moneyParticle;

    private bool firstImpact = true;
    private bool musicPlaying = false;


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

                
                foreach (ContactPoint contact in collision.contacts)
                    {
                        Instantiate(particleSystemPrefab, contact.point, Quaternion.identity);
                    }

                if (firstImpact)
                {
                    Debug.Log("first impact");
                    Instantiate(moneyParticle, gameObject.transform.position,Quaternion.identity);
                    firstImpact = false;
                    //increase score on gamemanager
                    audioSource.PlayOneShot(sfxMoney);
                }
            }
        }
       

        if (collision.gameObject.tag == "Floor" && gameObject.tag == "FirstStatue" && !(musicPlaying))
        {
            //Debug.Log("Object hit floor");
            floor.PlayMusic();
            musicPlaying = true;

            
        }

    }




}
