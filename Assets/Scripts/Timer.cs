
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
    public float seconds = 20;
    float miliseconds = 0;

    AudioSource audioSource;

    public AudioClip sfxTimerChime;
    public AudioClip sfxTimerTick;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (miliseconds <= 0)
        {
            if (seconds <= 0 && miliseconds <=0)
            {

                gamemanager.OutOfTime();
                miliseconds = 00;
                return;
                //try to find a way to override the "milliseconds = 100" outside of the if statement
                //minutes--;
                //seconds = 59;
            }
            else if (seconds >= 0)
            {
                seconds--;
                
            }

            
            miliseconds = 100;
            // maybe play a Tick noise?
        }

        if (seconds == 2 && miliseconds == 100 )
        {
            Debug.Log("3 seconds left");
            PlayChime();
            // maybe start a coroutine
        }




        miliseconds -= Time.deltaTime * 100;

        //Debug.Log(string.Format("{0}:{1}:{2}", minutes, seconds, (int)miliseconds));
        minsTimer.text = minutes.ToString("00");
        SecsTimer.text = seconds.ToString("00");
        milliTimer.text = miliseconds.ToString("00");
    }

    void PlayChime()
    {
        audioSource.PlayOneShot(sfxTimerChime);
    }
}