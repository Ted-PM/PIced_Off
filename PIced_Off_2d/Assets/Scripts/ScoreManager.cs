using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    int score = 0;
    public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI highScore;         // haven't done yet (may have been yoinked from quail flayl)
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //highScore.SetText("High Score " + PlayerPrefs.GetInt("HighScore"));
    }

    public void AddPoint()
    {
        score+= BallSpawner.Instance.GetNumBalls();     // add number of balls currently in play (multiplier ish)
        //score++;
        scoreText.SetText(score.ToString());
        //if (score > PlayerPrefs.GetInt("HighScore"))
        //{
        //    PlayerPrefs.SetInt("HighScore", score);
        //    highScore.SetText("High Score: " + PlayerPrefs.GetInt("HighScore"));
        //}
    }
}
