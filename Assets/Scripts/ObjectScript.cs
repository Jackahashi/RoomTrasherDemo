using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{

    public GameManager gameManager;

    public FloorScript floor;
    public AudioClip sfxImpact;
    public AudioClip sfxMoney;
    AudioSource audioSource;
    public GameObject particleSystemPrefab;
    public GameObject moneyParticle;

    public int scoreValue;


    // public transform startposition;  // e.g. current distance to floor - if distance to floor changes by more than 0.1. set a bool to true to enable point scoring

   

    private bool ScoreImpact = true;
    private static bool musicPlaying = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // obtain the start height of the object centre
    }


    void OnCollisionEnter(Collision collision)
    {
   
        // future modification may be necessary for performance -->  if current position on y axis (height) is more than 0.2 from object start height , points can be scored
        // the above script would detect if the object had moved from its start position (heingt) and than enable it to score points collisions

        


            if (collision.gameObject.tag != "Hammer")
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
                        //Debug.Log("first impact");
                        Instantiate(moneyParticle, gameObject.transform.position, Quaternion.identity);
                        ScoreImpact = false;
                        gameManager.scorePoints(scoreValue);
                        audioSource.PlayOneShot(sfxMoney);
                    }
                }
            }
        


                if (collision.gameObject.tag == "Floor" && gameObject.tag == "FirstStatue" && !(musicPlaying))
                {
                    //Debug.Log("Object hit floor");
                    floor.PlayMusic();
                    gameManager.EndStageOne();
                    musicPlaying = true;


                }

            }

        }








