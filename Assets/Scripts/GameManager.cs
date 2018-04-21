using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


    public LightsController[] lighting;
    public GameObject[] Stage2Items;
    public ObjectScript objectscript;
    public UIAlphaController stage2UI;
    
    public int score;
    public static int requiredScore = 160;

    private float delayTimerDelay;

    public Timer timerScript;
    public Totalizer totalizer;
    public GameObject scoreBoard;

    AudioSource audioSource;
    public AudioSource failAudioSource;
    public AudioClip sfxStageOneComplete;
    public AudioClip sfxStageTwoComplete;
    public AudioClip sfxTimerStart;
    public AudioClip sfxTimerEnd;
    public AudioClip sfxFail;
    public AudioClip sfxReset;

    public int buildIndex;

    public Text highScoreText;
    public static int highscore;

    void Start () {
        buildIndex = SceneManager.GetActiveScene().buildIndex;
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
        if(buildIndex == 1)
        {
            

            highscore = PlayerPrefs.GetInt("highscore", highscore);
            highScoreText.text = ("$" + (highscore.ToString()));

            foreach (GameObject obj in Stage2Items)
            {
                obj.layer = 9;
            }
        }
    }

    public void scorePoints(int amount)
    {
        score += amount;
    }

    //----------------------------------------------------------------end stage one------------------------------------------

    public void EndStageOne()
    {
        StartCoroutine(DelayLevel2Items());
        score = 0;
        stage2UI.FadeUp();
        foreach (LightsController light in lighting)
        {
          light.FadeUp();
        }
 
        foreach (GameObject item in Stage2Items)
        {
            item.SetActive(true);
        }
        ResetTimer();
        // stage one objects should dissapear or fade away maybe?
    }
    IEnumerator DelayLevel2Items()
    {
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(sfxStageOneComplete); 
    }

    //----------------------------------------------------out of time and count the score--------------------------------------------------

    public void OutOfTime()
    {
        audioSource.PlayOneShot(sfxTimerEnd);
        foreach (GameObject obj in Stage2Items)
        {
            obj.layer = 9;
        }
        timerScript.enabled = false;
        CountTheScore();
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

    //---------------------------------------------------score checker-------------------------------------------------------------------------
    public void CheckTheScore()
    {

        if (buildIndex >= 1)
        {
            if (score > highscore)
            {
                totalizer.ChangeTextColour(Color.green);
                highscore = score;
                highScoreText.text = ("$" + ("" + score)); 

                PlayerPrefs.SetInt("highscore", highscore);
                PlayerPrefs.Save();
            }
            Debug.Log("Show Reset UI BEll");
            // maybe do something else instead of comparing score values - ensure score remains visible
            // trigger reset UI 
        } else
        if (score >= requiredScore)
        {
            totalizer.ChangeTextColour(Color.green);
            StartCoroutine(ScoreCheckPassDelay());

        } else
        {
            totalizer.ChangeTextColour(Color.red);
            failAudioSource.PlayOneShot(sfxFail);
            StartCoroutine(ScoreCheckFailDelay());
        }
    }
    IEnumerator ScoreCheckPassDelay()
    {
        yield return new WaitForSeconds(1);
        audioSource.PlayOneShot(sfxStageTwoComplete);
        yield return new WaitForSeconds(5);
        SteamVR_LoadLevel.Begin("Level1");
    }
    IEnumerator ScoreCheckFailDelay()
    {
        yield return new WaitForSeconds(5);
        delayTimerDelay = 4.0f;
        ResetTimer();
        audioSource.PlayOneShot(sfxReset);
    }

    //-----------------------------------------------------reset timer---------------------------------------------------------------------
    public void ResetTimer()
    {
        if (buildIndex == 1)
        {
            Debug.Log("StartingLevel1");
        }
        totalizer.enabled = false;
        scoreBoard.SetActive(false);
        score = 0;
        StartCoroutine(DelayTimerStart());
        
        foreach (GameObject item in Stage2Items)
        {
            item.layer = 9;
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

        yield return new WaitForSeconds(4f);
        foreach (GameObject obj in Stage2Items)
        {
            obj.layer = 0;
        }
        timerScript.enabled = true;
    }

}
