using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System;
public class VideoPlays : MonoBehaviour
{
    //public static VideoPlays instance;
    public VideoPlayer videoToPlay;

    long numFrames = 0;
    public bool isScary;
    //int frameCount;
    //public TextMeshProUGUI scaryTXT;

    //public int isScary;
    //public Toggle isScaryPlaying;
    private void Awake()
    {
        //instance = this;
        //scaryTXT.SetText("scaryTXT");
    }
    private void Start()
    {
        //scaryTXT.SetText("scaryTXT");

        videoToPlay.Play();
        numFrames = Convert.ToInt64(videoToPlay.GetComponent<VideoPlayer>().frameCount);

        if (isScary)
        {
            numFrames -= 15;
        }
        else
        {
            numFrames -= 5;
        }
        
    }

    void Update()
    {
        //if (returnScaryStat() && deerScare == null)
        //{
        //    deerScare.Play();
        //}
        //if (isScary && videoToPlay.frame == 5)
        //{
        //    numFrames -= 15;
        //}

        if (((videoToPlay != null) && (videoToPlay.frame > numFrames)) || Input.anyKeyDown)
        {
            if (isScary)
            {
                GameManager.Instance.viewMenu();
            }
            Destroy(videoToPlay);
        }
        
    }

    //public bool returnScaryStat()
    //{
    //    return ((PlayerPrefs.GetString("scaryTXT") != "n") ? true : false);
    //}

    //public void scaryOff()
    //{
    //    PlayerPrefs.SetString("scaryTXT", "n");
    //}

    //public void scaryOn()
    //{
    //    PlayerPrefs.SetString("scaryTXT", "y");
    //}
}
