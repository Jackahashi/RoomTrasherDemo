using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //public DamageValue damageScore;

    public float timeRemaining;

    public LightsController[] lighting;

    public static int currentLevel = 0;

    public static int score;

    public GameObject[] Stage1Items;
    public GameObject[] Stage2Items;

    AudioSource audioSource;

    public AudioClip sfxStageComplete;


    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();

        score = 0;
    }
	

    public void EndStageOne()
    {
        Debug.Log("OnBoarding stage one complete");
        StartCoroutine(DelayLevelSound());
        score = 0;
        currentLevel = 1;
        foreach(LightsController light in lighting)
        {
            light.FadeUp();
        }


    }

    public void scorePoints(int amount)
    {
        score += amount;
    }



    IEnumerator DelayLevelSound()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(sfxStageComplete);
        Debug.Log("Score is " + score);


    }
        
        
}
