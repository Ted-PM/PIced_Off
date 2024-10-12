using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
public class VideoPlays : MonoBehaviour
{
    //public static VideoPlays instance;
    public VideoPlayer deerScare;

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
        deerScare.Play();
    }

    void Update()
    {
        //if (returnScaryStat() && deerScare == null)
        //{
        //    deerScare.Play();
        //}

        if (((deerScare != null) && (deerScare.frame >= 128)) || Input.anyKeyDown)
        {
            Destroy(deerScare);
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
