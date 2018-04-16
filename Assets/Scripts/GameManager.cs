using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public LightsController[] lighting;
    public GameObject[] Stage2Items;
    public UIAlphaController stage2UI;
    public Timer timerScript;
    public static int score;
    public static int requiredScore = 200;
    public int restarts = 0;
    private float delayTimerDelay;

    AudioSource audioSource;
    public AudioClip sfxStageComplete;
    public AudioClip sfxTimerStart;
    public AudioClip sfxFail;

    public ObjectScript objectscript;
    public Totalizer totalizer;
    


    void Start () {
        Stage2Items = GameObject.FindGameObjectsWithTag("SecondStageItems");
        audioSource = GetComponent<AudioSource>();
        timerScript = timerScript.GetComponent<Timer>();
        totalizer = totalizer.GetComponent<Totalizer>();
        totalizer.enabled = false;
        timerScript.enabled = false;
        score = 0;
        delayTimerDelay = 3.0f;
    }

    public void scorePoints(int amount)
    {
        score += amount;
    }

    //--------------------------------------------------------------------------------------------------------

    public void EndStageOne()
    {
        StartCoroutine(DelayLevel2Items());
        score = 0;
        stage2UI.FadeUp();
        foreach(LightsController light in lighting)
        {
            light.FadeUp();
        }
        foreach (GameObject item in Stage2Items)
        {
            item.SetActive(true);
            Debug.Log("stage2 items activated");
        }
        //re enable the hammers
        ResetTimer();
        // stage one objects should fade away maybe?
    }
    IEnumerator DelayLevel2Items()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(sfxStageComplete); 
    }

    //---------------------------------------------------------------------------------------------------------



    public void OutOfTime()
    {
        audioSource.PlayOneShot(sfxFail);
        Debug.Log("Out of time");
        timerScript.enabled = false;
        //disable the hammers
        CountTheScore();
        Debug.Log("The score is actually " + score);
    }

    public void CountTheScore() {

        StartCoroutine(ScoreCountActions());
        //Start coroutine WITH delay 2 or 3 seconds for dollar particles to complete 
        // at the end of the delay , begin the counting of the score 
       
        // start counting the score, ideally this would use update to count the score from '0' to 'score'
        // activate the score counting script , which will in turn access the high score + display the UI + play counting sound

    }
    IEnumerator ScoreCountActions()
    {
        yield return new WaitForSeconds(2);
        foreach (GameObject item in Stage2Items)
        {
            objectscript = item.GetComponent<ObjectScript>();
            if (objectscript != null)
            {
                objectscript.showValue();
            }
        }
        yield return new WaitForSeconds(1);
        totalizer.enabled = true;
        // activate the score counter script and UI (the score counter will call the score check function)

    }

    //----------------------------------------------------------------------------------------------------------------------------


    // make sure nothing else happends during this process
    public void CheckTheScore()
    {
        Debug.Log("Checking the score");
        if (score > requiredScore)
        {
            StartCoroutine(ScoreDisplayDelay());
            //Make score green and 
            // play success sound
   
            //SteamVR_LoadLevel// trigger end of onboarding -functions 
        } else
        {
            
            StartCoroutine(ScoreDisplayDelay());
            delayTimerDelay = 4.0f;
            ResetTimer();
        }
        
    }
    IEnumerator ScoreDisplayDelay()
    {
        yield return new WaitForSeconds(2);
    }


    public void ResetTimer()
    {
        Debug.Log("Timer resetting, objs should reposition");
        totalizer.enabled = false;
        score = 0;
        StartCoroutine(DelayTimerStart());
        
        foreach (GameObject item in Stage2Items)
        {

            objectscript = item.GetComponent<ObjectScript>();
            if (objectscript != null)
            {
                objectscript.StartingPositions();

            }
        }
    }
    IEnumerator DelayTimerStart()
    {
        
        yield return new WaitForSeconds(delayTimerDelay);
        audioSource.PlayOneShot(sfxTimerStart);

        yield return new WaitForSeconds(3.3f);
        Debug.Log("Timer starting");
        timerScript.enabled = true;
    }




}
