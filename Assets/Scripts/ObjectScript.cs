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
    AudioSource audioSource;
    public GameObject particleSystemPrefab;
    public GameObject moneyParticle;
    public GameObject dollarParticle;

    public int scoreValue;

    //private static int hammerCount;

    float t;
    Vector3 startPosition;
    public Quaternion startRotation;
 
    float timeToMove = 2000;

    private Rigidbody rb;



    private bool Hammer1Collided = false;
    private bool Hammer2Collided = false;
    private bool beingHeld = false;

    public float throwForce = 1;


    private bool ScoreImpact = true;
    private static bool musicPlaying = false;


   void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        particleSystemPrefab = Resources.Load("PowHit") as GameObject;
        moneyParticle = Resources.Load("Assets/Prefabs/MoneyParticle") as GameObject;
        sfxImpact = Resources.Load("Assets/Sounds/Cropped_Impacts_Extension-I_163-[AudioTrimmer.com]") as AudioClip;
        sfxMoney = Resources.Load("Sounds/Coin Award 2") as AudioClip;
        audioSource = GetComponent<AudioSource>();
        startPosition = gameObject.transform.position;
        startRotation = gameObject.transform.rotation;

        // record the original parent transform
    }


    void OnCollisionEnter(Collision collision)
    {

        // future modification may be necessary for performance -->  if current position on y axis (height) is more than 0.2 from object start height , points can be scored
        // the above script would detect if the object had moved from its start position (heingt) and than enable it to score points collisions
        // public transform startposition;  // e.g. current distance to floor - if distance to floor changes by more than 0.1. set a bool to true to enable point scoring
        //if else

        if (!(musicPlaying))
        {
            if (collision.gameObject.tag == "Floor" && gameObject.tag == "FirstStatue")
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
            if (collision.relativeVelocity.magnitude > 1.2f)
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








