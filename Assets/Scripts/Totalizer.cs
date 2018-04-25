using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Totalizer : MonoBehaviour
{

    public GameManager gamemanager;

    public Text scoreBoard;

    public int scoreBoardScore = 000000;

    AudioSource audioSource;
    public AudioClip sfxCoin; // make shorter version of coin sound for counting

    private bool currentlyCounting = false;
    private float cooldown;
    private float cooldownLength = 0.02f;

    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // auto populate the gamemanager
    }

    private void Update()
    {
        if (currentlyCounting)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                cooldown = cooldownLength;
                audioSource.PlayOneShot(sfxCoin);
            }
        }
    }

    private void OnEnable()
    {
       
        scoreBoardScore = 00000;
        currentlyCounting = true;
        if (gamemanager.buildIndex >= 1)
        {
            Debug.Log("fast score counting");
            InvokeRepeating("ShowTheScore", 0.0005f, 0.0005f);
        }
        else { InvokeRepeating("ShowTheScore", 0.02f, 0.02f); }

        
    }

    void ShowTheScore()
    {
        /*for(int scoreBoardScore = 0; scoreBoardScore < gamemanager.score; scoreBoardScore++)
        {
            scoreBoard.text = ("$" + (scoreBoardScore.ToString("00000")));
            audioSource.PlayOneShot(sfxCoin);
        }
        */
        
        scoreBoard.text = ("$" + (scoreBoardScore.ToString("00000")));
        if (scoreBoardScore < gamemanager.score)
        {
            scoreBoardScore++;
            
        }
        
        if (scoreBoardScore >= gamemanager.score)
        {
            if(gamemanager.buildIndex == 1)
            {
                scoreBoard.text = ("$" + (scoreBoardScore.ToString("00000")));
                gamemanager.CheckTheScore();
                CancelInvoke();
                currentlyCounting = false;

            }
            if (gamemanager.buildIndex == 0)
            {
                gamemanager.CheckTheScore();
                StartCoroutine(ScoreDisplayDelay());
                CancelInvoke();
                currentlyCounting = false;
            }
        }
        //audioSource.PlayOneShot(sfxCoin);
    }

    IEnumerator ScoreDisplayDelay()

    {
        scoreBoard.text = ("$" + (scoreBoardScore.ToString("00000")));
        yield return new WaitForSeconds(5);
        
        
    }

    public void ChangeTextColour(Color newColor)
    {
        scoreBoard.color = newColor;
    }

}