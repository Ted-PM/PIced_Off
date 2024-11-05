using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaryToggle : MonoBehaviour
{
    public static ScaryToggle Instance;
    public Toggle myToggle;
    static bool toggleValue;

    private void Awake()
    {
        Instance = this;
        //toggleValue = myToggle.isOn;
        myToggle.isOn = toggleValue;
    }

    void Start()
    {
        myToggle.isOn = toggleValue;
    }


    public void toggleUsed()
    {
        toggleValue = myToggle.isOn;
        Debug.Log("Toggle VAL = " +  myToggle.isOn);
    }

}
