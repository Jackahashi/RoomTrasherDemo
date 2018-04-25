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
    public AudioSource ExtraAudioSource;
    public AudioClip sfxStageOneComplete;
    public AudioClip sfxStageTwoComplete;
    public AudioClip sfxTimerStart;
    public AudioClip sfxTimerEnd;
    public AudioClip sfxFail;
    public AudioClip sfxReset;
    public AudioClip sfxLevel1Music;

    public int buildIndex;

    public Text ResetText;

    public Text highScoreText;
    public static int highscore;
    public PrinterScript printerScript;
    public GameObject Bell;

    public GameObject[] screens;
    



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
        foreach (GameObject obj in Stage2Items)
        {
            obj.layer = 9;
        }

        if (buildIndex == 1)
        {
            
            printerScript = GameObject.Find("PrinterContainer").GetComponentInChildren<PrinterScript>();
            highscore = PlayerPrefs.GetInt("highscore", highscore);
            highScoreText.text = ("$" + (highscore.ToString()));
            foreach (GameObject screen in screens)
            {
                screen.SetActive(false);
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
        foreach (GameObject item in Stage2Items)
        {
            item.SetActive(true);
        }
        foreach (LightsController light in lighting)
        {
          light.FadeUp();
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
        if (buildIndex == 1)
        {
            printerScript.enabled = false;
            foreach (GameObject screen in screens)
            {
                screen.SetActive(false);
            }

        }

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
            StartCoroutine(Level1EndDelay());
 
        } else
        if (score >= requiredScore)
        {
            totalizer.ChangeTextColour(Color.green);
            StartCoroutine(ScoreCheckPassDelay());

        } else
        {
            totalizer.ChangeTextColour(Color.red);
            ExtraAudioSource.PlayOneShot(sfxFail);
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
    IEnumerator Level1EndDelay()
    {
        yield return new WaitForSeconds(2);
        Bell.SetActive(true);
        Destroypapers();
        ResetText.text = ("TRY AGAIN");
    }

    //-----------------------------------------------------reset timer---------------------------------------------------------------------
    public void ResetTimer()
    {
        if (buildIndex == 1)
        {
            printerScript.enabled = true;
            delayTimerDelay = 1;
            foreach (GameObject screen in screens)
            {
                screen.SetActive(true);
            }
            //ExtraAudioSource.PlayOneShot(sfxLevel1Music);
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
        ExtraAudioSource.PlayOneShot(sfxLevel1Music);
        foreach (GameObject obj in Stage2Items)
        {
            obj.layer = 0;
        }
        timerScript.enabled = true;
        ExtraAudioSource.PlayOneShot(sfxLevel1Music);
    }

    private void Destroypapers()
    {
        GameObject[] papers = GameObject.FindGameObjectsWithTag("Paper");
        foreach (GameObject paper in papers)
            GameObject.Destroy(paper);
    }
}
