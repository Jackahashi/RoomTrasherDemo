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
        scoreBoardScore = 00000;
        Debug.Log("totalizer enabled");
        InvokeRepeating("ShowTheScore", 0.05f, 0.05f);
    }


  
    void ShowTheScore()
    {

        scoreBoard.text = ("$" + (scoreBoardScore.ToString("00000")));
        Debug.Log("updating the score");
        if (scoreBoardScore < gamemanager.score)
        {
            scoreBoardScore++;
            audioSource.PlayOneShot(sfxCoin);
        }

        if (scoreBoardScore >= gamemanager.score)
        {
            gamemanager.CheckTheScore();
            StartCoroutine(ScoreDisplayDelay());
            
            
        }

    }

    IEnumerator ScoreDisplayDelay()
    {
        scoreBoard.text = ("$" + (scoreBoardScore.ToString("00000")));
        yield return new WaitForSeconds(5);
        CancelInvoke();
        
    }

    public void ChangeTextColour(Color newColor)
    {
        scoreBoard.color = newColor;
    }

}