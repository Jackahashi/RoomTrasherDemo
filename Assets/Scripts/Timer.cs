
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
    public AudioClip sfxTimerTick;

    void Start()
    {
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
                Debug.Log("Outtatime called");
                milliTimer.text = miliseconds.ToString("00");
                SecsTimer.text = seconds.ToString("10");
                return;
                
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

        if (seconds == 3 && miliseconds == 100 )
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