using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour {


    public FloorScript floor;

    public AudioClip sfxMusic;
    public AudioClip sfxFloor;
    public AudioClip sfxHammer;

    AudioSource audioSource;

    private void Start()
    {
         audioSource = GetComponent<AudioSource>();
    }




    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hammer")
        {
            Debug.Log("the hammer hit the object");
        }
        else if (collision.gameObject.tag == "Floor")
        {
            Impact();
            audioSource.PlayOneShot(sfxMusic);
        }
            
    }


    public void Impact()
    {
        Debug.Log("The object has hit something");
        //trigger particle effect for 'hit'
        //trigger particle effect for 'damage points'
        //make a 'hit' noise 
        //score the damage points
        
    }


}
