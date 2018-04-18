using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public LightsController[] lighting;
    public GameObject[] Stage2Items;
    public ObjectScript objectscript;
    public UIAlphaController stage2UI;
    
    public int score;
    public static int requiredScore = 120;

    private float delayTimerDelay;

        public Timer timerScript;
    public Totalizer totalizer;
    public GameObject scoreBoard;

    AudioSource audioSource;
    public AudioClip sfxStageComplete;
    public AudioClip sfxTimerStart;
    public AudioClip sfxFail;
    public AudioClip sfxReset;


    


    void Start () {
        Stage2Items = GameObject.FindGameObjectsWithTag("SecondStageItems");
        audioSource = GetComponent<AudioSource>();
        timerScript = timerScript.GetComponent<Timer>();
        totalizer = totalizer.GetComponent<Totalizer>();
        scoreBoard = GameObject.Find("ScoreBoardPanel");
        totalizer.enabled = false;
        timerScript.enabled = false;
        score = 0;
        delayTimerDelay = 3.0f;
        scoreBoard.SetActive(false);
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
        //re enable the hammers?
        ResetTimer();
        // stage one objects should dissapear or fade away maybe?
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
        //disable the hammers?
        CountTheScore();
        Debug.Log("The score is actually " + score);
    }

    public void CountTheScore() {

        StartCoroutine(ScoreCountActions());
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
        yield return new WaitForSeconds(3);
        scoreBoard.SetActive(true);
        totalizer.enabled = true;
    }

    //----------------------------------------------------------------------------------------------------------------------------


    public void CheckTheScore()
    {
        Debug.Log("Checking the score");
        if (score > requiredScore)
        {
            totalizer.ChangeTextColour(Color.green);
            audioSource.PlayOneShot(sfxStageComplete);
            StartCoroutine(ScoreCheckPassDelay());
            
            //Make score green and totalizer.ChangeTextAppearence();
            SteamVR_LoadLevel.Begin("Level1");
        } else
        {
            totalizer.ChangeTextColour(Color.red);
            audioSource.PlayOneShot(sfxFail);
            StartCoroutine(ScoreCheckFailDelay());
            
        }
        
    }
    IEnumerator ScoreCheckPassDelay()
    {
        
        
        yield return new WaitForSeconds(5);
        SteamVR_LoadLevel.Begin("Level1");
    }
    IEnumerator ScoreCheckFailDelay()
    {
        yield return new WaitForSeconds(5);
        delayTimerDelay = 4.0f;
        ResetTimer();
       
    }

    //--------------------------------------------------------------------------------------------------------------------------
    public void ResetTimer()
    {
        Debug.Log("Timer resetting, objs should reposition");
        totalizer.enabled = false;
        scoreBoard.SetActive(false);
        score = 0;
        StartCoroutine(DelayTimerStart());
        
        foreach (GameObject item in Stage2Items)
        {

            objectscript = item.GetComponent<ObjectScript>();
            if (objectscript != null)
            {
                objectscript.StartingPositions();
                audioSource.PlayOneShot(sfxReset);

            }
        }
    }
    IEnumerator DelayTimerStart()
    {
        
        yield return new WaitForSeconds(delayTimerDelay);
        audioSource.PlayOneShot(sfxTimerStart);

        yield return new WaitForSeconds(4f);
        Debug.Log("Timer starting");
        // enable hammers
        timerScript.enabled = true;
    }




}
