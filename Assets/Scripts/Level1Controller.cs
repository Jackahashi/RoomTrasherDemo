using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Controller : MonoBehaviour {

    public GameObject[] Level1Items;
    public L1ObjectScript objectscript;


    public int score;


    public Timer timerScript;
    public Totalizer totalizer;
    public GameObject scoreBoard;

    AudioSource audioSource;
    

    public AudioClip sfxTimerStart;
    public AudioClip sfxTimerEnd;
    public AudioClip sfxReset;

    void Start()
    {

        Level1Items = GameObject.FindGameObjectsWithTag("projectile");
        audioSource = GetComponent<AudioSource>();
        timerScript = timerScript.GetComponent<Timer>();
        totalizer = totalizer.GetComponent<Totalizer>();
        scoreBoard = GameObject.Find("ScoreBoardPanel");
        totalizer.enabled = false;
        timerScript.enabled = false;
        score = 0;
        scoreBoard.SetActive(false);
    }

    public void scorePoints(int amount)
    {
        score += amount;
    }

    public void StartLevel1()
    {
        Debug.Log("Level1Start");

        foreach (GameObject item in Level1Items)
        {
            item.SetActive(true);
        }
        ResetTimer();
    }
    //---------------------------------------------------------------------------------------------------------------------------
    public void ResetTimer()
    {
        totalizer.enabled = false;
        scoreBoard.SetActive(false);
        score = 0;
        StartCoroutine(DelayTimerStart());

        foreach (GameObject item in Level1Items)
        {
            item.layer = 9;
            objectscript = item.GetComponent<L1ObjectScript>();
            if (objectscript != null)
            {
                objectscript.StartingPositions();
            }
        }
    }
    IEnumerator DelayTimerStart()
    {
        yield return new WaitForSeconds(3);
        audioSource.PlayOneShot(sfxTimerStart);

        yield return new WaitForSeconds(4f);
        foreach (GameObject obj in Level1Items)
        {
            obj.layer = 0;
        }
        timerScript.enabled = true;
    }
}
