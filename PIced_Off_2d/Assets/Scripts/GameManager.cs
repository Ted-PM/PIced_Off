using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverMenu;
    private bool menuIsActive;          // allow other things to check if game is over
    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
    }

    private void Start()
    {
        gameOverMenu.SetActive(false);      // start w/ game over menue
        menuIsActive = false;
    }

    public void GameOver()
    {
        Debug.Log("game over called");

        gameOverMenu.SetActive(true);       // make game over menu appear (has butto to restart)
        menuIsActive = true;
        //Time.timeScale = 0;
    }

    public bool MenuIsActive() { return menuIsActive; }
    public void Restart()
    {
        //Debug.Log("loading scene");

        SceneManager.LoadScene(0);      // only one scene rn, so loads that
    }

    public void Quit()
    {
        #if UNITY_STANDALONE                // if running in game view
                Application.Quit();
        #endif
        #if UNITY_EDITOR        // if running on acc web or smth 
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
