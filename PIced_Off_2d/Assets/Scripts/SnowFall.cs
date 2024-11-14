using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFall : MonoBehaviour
{
    public GameObject thingToPointTo;       // character usually
    Transform pointToPosition;

    private float height;

    // Start is called before the first frame update
    void Start()
    {
        pointToPosition = thingToPointTo.GetComponent<Transform>().transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (((BadSpawner.Instance.logTotalTime) < 10) || (GameManager.Instance.MenuIsActive() == true))
        {
            height = 15;
            //scrollSpeed = (BadSpawner.Instance.totalTime);
            
        }
        else if (((BadSpawner.Instance.logTotalTime) > 30))
        {
            height = 5;
        }
        else// if (GameManager.Instance.MenuIsActive() == true)
        {
            height = BadSpawner.Instance.logTotalTime;
            height = 150 / height;
        }

        //speed = BadSpawner.Instance.logTotalTime;
        //BadSpawner.Instance.logTotalTime
        transform.LookAt(pointToPosition);

        transform.position = new Vector3(15f, height, 0f);

        //transform.position = new Vector3(15f, (13.0f/speed), 0f);
    }
}
