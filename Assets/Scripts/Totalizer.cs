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
    public AudioClip sfxCoin;
   
    

    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // auto populate the gamemanager
    }

    private void OnEnable()
    {
        Debug.Log("build index is " + gamemanager.buildIndex);
        scoreBoardScore = 00000;
        if (gamemanager.buildIndex >= 1)
        {
            Debug.Log("fast score counting");
            InvokeRepeating("ShowTheScore", 0.005f, 0.005f);
        }
        else { InvokeRepeating("ShowTheScore", 0.02f, 0.02f); }

        
    }

    void ShowTheScore()
    {

        scoreBoard.text = ("$" + (scoreBoardScore.ToString("00000")));
        //Debug.Log("updating the score");
        if (scoreBoardScore < gamemanager.score)
        {
            scoreBoardScore++;
            audioSource.PlayOneShot(sfxCoin);
        }

        if (scoreBoardScore >= gamemanager.score)
        {
            if(gamemanager.buildIndex == 1)
            {
                scoreBoard.text = ("$" + (scoreBoardScore.ToString("00000")));
                gamemanager.CheckTheScore();
                CancelInvoke();
               
            }

            if (gamemanager.buildIndex == 0)
            {
                gamemanager.CheckTheScore();
                StartCoroutine(ScoreDisplayDelay());
                CancelInvoke();
            }
            
            
        }

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