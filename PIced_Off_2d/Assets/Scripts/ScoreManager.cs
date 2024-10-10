using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScore;         // haven't done yet (may have been yoinked from quail flayl)
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        highScore.SetText("High Score " + PlayerPrefs.GetInt("HighScore"));
    }

    private void Update()
    {
        if (GameManager.Instance.MenuIsActive() == true)
        {
            scoreText.gameObject.SetActive(false);
        }
    }

    public void AddPoint()
    {
        score+= BallSpawner.Instance.GetNumBalls();     // add number of balls currently in play (multiplier ish)
        //score++;
        if (BallSpawner.Instance.Lost != true)        // if head destroyed
        {
            scoreText.SetText(score.ToString());       // remove own collider so don't mess up game by adding new ball
        }

        
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore.SetText("High Score: " + PlayerPrefs.GetInt("HighScore"));
        }
    }
}
