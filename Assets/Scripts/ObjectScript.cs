using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    // store start pos and rotation

    public GameManager gameManager;
    public FloorScript floor;
    public AudioClip sfxImpact;
    public AudioClip sfxMoney;
    public GameObject particleSystemPrefab;
    public GameObject moneyParticle;
    public GameObject dollarParticle;
    public int scoreValue;
    private bool ScoreImpact = true;
    private static bool musicPlaying = false;
    Vector3 startPosition;
    Quaternion startRotation;
    AudioSource audioSource;

    //private Rigidbody rb;
    //private bool Hammer1Collided = false;
    //private bool Hammer2Collided = false;
    //private bool beingHeld = false;
    //public float throwForce = 1;
    //private static int hammerCount;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        startPosition = gameObject.transform.position;
        startRotation = gameObject.transform.rotation;
    }


    void OnCollisionEnter(Collision collision)
    {

        if (gameObject.tag == "FirstStatue")
        {
            if (collision.gameObject.tag == "Floor" && !(musicPlaying))
            {
                floor.PlayMusic();
                gameManager.EndStageOne();
                musicPlaying = true;
            }

        }
       if (collision.gameObject.tag == "Hammer")
        {
            /* if (collision.gameObject.name == "Right Hammer")
                 Hammer1Collided = true;
             else if (collision.gameObject.name == "Left Hammer")
                 Hammer2Collided = true;

             if (Hammer1Collided && Hammer2Collided)
             {
                 Debug.Log("Hammer picked up");
                 gameObject.transform.SetParent(collision.transform);
                 gameObject.GetComponent<Rigidbody>().isKinematic = true;
                 beingHeld = true;

             } */
        }
        else
        {
            if (collision.relativeVelocity.magnitude > 1)
            {
                audioSource.PlayOneShot(sfxImpact);
                foreach (ContactPoint contact in collision.contacts)
                {
                    Instantiate(particleSystemPrefab, contact.point, Quaternion.identity);
                }

                if (ScoreImpact && collision.transform.root != transform.root)
                {
                    
                    Instantiate(moneyParticle, gameObject.transform.position, Quaternion.identity);
                    ScoreImpact = false;
                    gameManager.scorePoints(scoreValue);
                    audioSource.PlayOneShot(sfxMoney);
                }
            }
        }
  
    }

   /* public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Right Hammer")
            // maybe put delays of 0.5 seconds in here?
            Hammer1Collided = false;
        else if (collision.gameObject.name == "Left Hammer")

            // maybe put delays of 0.5 seconds in here?
            Hammer2Collided = false;

        if (beingHeld)
        {
            Debug.Log("being thrown");
            gameObject.transform.SetParent(null);
            Rigidbody HammerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;

            rigidbody.velocity = HammerRigidbody.velocity * throwForce;
            rigidbody.angularVelocity = HammerRigidbody.angularVelocity;
            beingHeld = false;
        }

        
    }
    */

    public void StartingPositions()
    {
        gameObject.transform.position = startPosition;
        gameObject.transform.rotation = startRotation;
        ScoreImpact = true;
    }

    public void showValue()
    {
        if(ScoreImpact != true)
        {
            Instantiate(dollarParticle, gameObject.transform.position, Quaternion.identity);
        }
      
    }


}








