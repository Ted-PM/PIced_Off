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
    private IEnumerator coroutine;

    //long numFrames = 0;
    public bool isScary;
    public float playTime;
    bool vidDone = false;
    //public TextMeshProUGUI scaryTXT;

    //public int isScary;
    //public Toggle isScaryPlaying;
    private void Awake()
    {
        if (!isScary)
        {
            videoToPlay.url = System.IO.Path.Combine(Application.dataPath, "Materials/SnowmanEnter3.mp4");
        }
        else
        {
            videoToPlay.url = System.IO.Path.Combine(Application.dataPath, "Materials/PicedOff_DeerScarLonger.mp4");
        }
        //instance = this;
        //scaryTXT.SetText("scaryTXT");

    }
    private void Start()
    {   
        if (videoToPlay != null)
        {
            videoToPlay.Play();
        }
        //Debug.Log(Application.dataPath);
        //scaryTXT.SetText("scaryTXT");
        //GameManager.Instance.stopWind();
        //videoToPlay.url = System.IO.Path.Combine(Application.streamingAssetsPath, "SnowmanEnter3.mp4");
        //videoToPlay.url.play();
        //videoToPlay.VideoAudioOutputMode.Direct;
        vidDone = false;
        videoToPlay.Play();
        coroutine = WaitForVidEnd(playTime);
        StartCoroutine(coroutine);
        //numFrames = Convert.ToInt64(videoToPlay.GetComponent<VideoPlayer>().frameCount);

        //if (isScary)
        //{
        //    numFrames -= 15;
        //}
        //else
        //{
        //    numFrames -= 5;
        //}

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
        //if (((videoToPlay != null) && (videoToPlay.frame > numFrames)) || Input.anyKeyDown)

        if (((videoToPlay != null) && vidDone) || Input.anyKeyDown)
        {
            if (isScary)
            {
                GameManager.Instance.viewMenu();
            }
            GameManager.Instance.playWind();
            Destroy(videoToPlay);
        }
        
    }

    IEnumerator WaitForVidEnd(float time)
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(time);
        vidDone = true; 
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
