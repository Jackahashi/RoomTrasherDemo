using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

   

    

    public LightsController[] lighting;
    public GameObject[] Stage2Items;
    public UIAlphaController stage2UI;
    public Timer timerScript;
    

    public static int score;
    public int requiredScore = 200;

    public int restarts = 0;
    

    AudioSource audioSource;

    public AudioClip sfxStageComplete;
    public AudioClip sfxTimerStart;
    public AudioClip sfxFail;


    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        timerScript = timerScript.GetComponent<Timer>();
        timerScript.enabled = false;
        score = 0;
    }

    // will need to use stopwatch to check score, if timer ends and score is above 200, end of stage 2, show UI to begin first level.
	

    public void EndStageOne()
    {
        Debug.Log("OnBoarding stage one complete");
        StartCoroutine(DelayLevel2Items());
        score = 0;
        stage2UI.FadeUp();
        foreach(LightsController light in lighting)
        {
            light.FadeUp();
        }
        
        
    }

    public void scorePoints(int amount)
    {
        score += amount;
    }


public void OutOfTime()
    {
        Debug.Log("Out of time");
        timerScript.enabled = false;
        if (score > requiredScore)
        {
           // trigger end of onboarding -functions e.g. loadlevel
        } else
        {
            ResetTimer();
        }
    
    }

    public void ResetTimer()
    {
        Debug.Log("Timer reset function");
        score = 0;
        audioSource.PlayOneShot(sfxFail);
        StartCoroutine(DelayTimerStart());
        restarts++;
        //maybe count the number of restarts and give a hint
    }


    IEnumerator DelayLevel2Items()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(sfxStageComplete); // need to find a longer sound here
        yield return new WaitForSeconds(3.0f);
        timerScript.enabled = true;



    }

    IEnumerator DelayTimerStart()
    {

        Debug.Log("Timer reset coroutine");
        yield return new WaitForSeconds(3.0f);
        audioSource.PlayOneShot(sfxTimerStart);
        timerScript.enabled = true;

        



    }


}
