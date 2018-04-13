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
    private float delayTimerDelay;

    AudioSource audioSource;
    public AudioClip sfxStageComplete;
    public AudioClip sfxTimerStart;
    public AudioClip sfxFail;


    void Start () {
        Stage2Items = GameObject.FindGameObjectsWithTag("SecondStageItems");
        audioSource = GetComponent<AudioSource>();
        timerScript = timerScript.GetComponent<Timer>();
        timerScript.enabled = false;
        score = 0;
        delayTimerDelay = 3.0f;
    }

    public void scorePoints(int amount)
    {
        score += amount;
    }

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
            
            Debug.Log("stage2 items activated");
        }
        ResetTimer();
        // stage one objects should fade away?
    }

    IEnumerator DelayLevel2Items()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(sfxStageComplete); 
    }




public void OutOfTime()
    {
        audioSource.PlayOneShot(sfxFail);
        Debug.Log("Out of time");
        timerScript.enabled = false;

        if (score > requiredScore)
        {
           // trigger end of onboarding -functions e.g. loadlevel
        } else
        {
            delayTimerDelay = 4.0f;
            ResetTimer();
            restarts++;
            Debug.Log("restart count: " + restarts);
            // stage2 items return to start positions
        }
    }

    public void ResetTimer()
    {
        Debug.Log("Timer reset function");
        score = 0;
        StartCoroutine(DelayTimerStart());
    }

    IEnumerator DelayTimerStart()
    {
        Debug.Log("Timer reset coroutine");
        yield return new WaitForSeconds(delayTimerDelay);
        audioSource.PlayOneShot(sfxTimerStart);

        yield return new WaitForSeconds(3.3f);
        timerScript.enabled = true;

    }


}
