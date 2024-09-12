using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    int score = 0;
    public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI highScore;
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
        score+= BallSpawner.Instance.GetNumBalls();
        //score++;
        scoreText.SetText(score.ToString());
        //if (score > PlayerPrefs.GetInt("HighScore"))
        //{
        //    PlayerPrefs.SetInt("HighScore", score);
        //    highScore.SetText("High Score: " + PlayerPrefs.GetInt("HighScore"));
        //}
    }
}
