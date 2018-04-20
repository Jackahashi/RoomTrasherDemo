﻿
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
     
     public class Timer : MonoBehaviour
{
    public GameManager gamemanager;


    public Text minsTimer;
    public Text SecsTimer;
    public Text milliTimer;
    float minutes = 0;
    public float seconds = 10;
    float miliseconds = 0;

    AudioSource audioSource;

    public AudioClip sfxTimerChime;

    void Start()
    {
        if(gamemanager == null)
        {
            gamemanager = GameObject.Find("LevelController").GetComponent<Level1Controller>();
        }
        audioSource = GetComponent<AudioSource>();
        seconds = 10;
        miliseconds = 0;
    }

    private void OnEnable()
    {
        seconds = 10;
    }

    void Update()
    {
        if (miliseconds <= 0)
        {
            if (seconds <= 0 && miliseconds <=0)
            {
                miliseconds = 0;
                gamemanager.OutOfTime();
                milliTimer.text = miliseconds.ToString("00");
                SecsTimer.text = seconds.ToString("10");
                return;
            }
            else if (seconds >= 0)
            {
                seconds--; 
            }

            miliseconds = 100;
        }

        if (seconds == 3 && miliseconds == 100 )
        {
            Debug.Log("3 seconds left");
            PlayChime();
        }

        miliseconds -= Time.deltaTime * 100;

        minsTimer.text = minutes.ToString("00");
        SecsTimer.text = seconds.ToString("00");
        milliTimer.text = miliseconds.ToString("00");
    }

    void PlayChime()
    {
        audioSource.PlayOneShot(sfxTimerChime);
        StartCoroutine(DelayChimes());
    }

    IEnumerator DelayChimes()
    {
        yield return new WaitForSeconds(1.0f);
        audioSource.PlayOneShot(sfxTimerChime);
        yield return new WaitForSeconds(1.0f);
        audioSource.PlayOneShot(sfxTimerChime);
        yield return new WaitForSeconds(1.0f);
        audioSource.PlayOneShot(sfxTimerChime);

    }
}