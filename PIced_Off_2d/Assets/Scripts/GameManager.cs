using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverMenu;
    public GameObject settingsMenu;
    public GameObject scaryDeerVid;
    public bool menuIsActive;          // allow other things to check if game is over
    //public bool scary = false;
    //public int isScaryInt;
    //public Toggle isScary;



    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
    }

    private void Start()
    {
        gameOverMenu.SetActive(false);      // start w/ game over menue
        settingsMenu.SetActive(false);
        scaryDeerVid.SetActive(false);
        menuIsActive = false;

        //PlayerPrefs.GetString("scaryTXT");
        //if(returnScaryStat())
        //    isScary.isOn = true;
        //else 
        //    isScary.isOn = false;
        //isScary = (PlayerPrefs.GetString("scaryTXT"))
        //isScary.set
        //isScaryInt = PlayerPrefs.GetInt("isScaryInt");

            //scary = false;
    }
    //public bool returnScaryStat()
    //{
    //    return ((PlayerPrefs.GetString("scaryTXT") == "y") ? true : false);
    //}
    public void GameOver()
    {
        scaryDeerVid.SetActive(true);
        //Debug.Log("game over called");
        //settingsMenu.SetActive(false);
        //gameOverMenu.SetActive(false);       // make game over menu appear (has butto to restart)
        //menuIsActive = false;

        //Time.timeScale = 0;
    }

    public void scaryVidOver()
    {
        Debug.Log("game over called");
        settingsMenu.SetActive(false);
        gameOverMenu.SetActive(true);       // make game over menu appear (has butto to restart)
        menuIsActive = true;
    }

    public void viewSettings()
    {
        Debug.Log("DisplaySettings");

        gameOverMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    //public void changeScaryStatus()
    //{
    //    if (isScary)
    //    {
    //        VideoPlays.instance.scaryOn();
    //    }
    //    else
    //    {
    //        VideoPlays.instance.scaryOff();
    //    }
    //}

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
