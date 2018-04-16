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
    }

    private void OnEnable()
    {
        scoreBoardScore = 000000;
        Debug.Log("totalizer enabled");
        InvokeRepeating("ShowTheScore", 1, 0.5f);
    }


  
    void ShowTheScore()
    {

        scoreBoard.text = scoreBoardScore.ToString("000");
        Debug.Log("updating the score");
        while (scoreBoardScore < GameManager.score)
        {
            scoreBoardScore++;
        }

        if (scoreBoardScore == GameManager.score)
        {
            CancelInvoke();
            gamemanager.CheckTheScore();

        }

    }

}