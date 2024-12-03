using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFall : MonoBehaviour
{
    public GameObject thingToPointTo;       // character usually
    Transform pointToPosition;
        
    private float height;

    private ParticleSystem snow;
    private ParticleSystem.VelocityOverLifetimeModule velocityLifeModule;

    // Start is called before the first frame update
    void Start()
    {
        snow = GetComponent<ParticleSystem>();
        pointToPosition = thingToPointTo.GetComponent<Transform>().transform;
        velocityLifeModule = snow.velocityOverLifetime;
        velocityLifeModule.x = 60;
        velocityLifeModule.y = 20;
    }

    // Update is called once per frame
    void Update()
    {
        //      -------
        //if (((BadSpawner.Instance.logTotalTime) < 10) || (GameManager.Instance.MenuIsActive() == true))
        //{
        //    height = 15;
        //    //scrollSpeed = (BadSpawner.Instance.totalTime);
            
        //}
        //else if (((BadSpawner.Instance.logTotalTime) > 30))
        //{
        //    height = 5;
        //}
        //else// if (GameManager.Instance.MenuIsActive() == true)
        //{
        //    height = BadSpawner.Instance.logTotalTime;
        //    height = 150 / height;
        //}
        //      ------

        //speed = BadSpawner.Instance.logTotalTime;
        //BadSpawner.Instance.logTotalTime
        transform.LookAt(pointToPosition);

        //var velocityOverLifetime = snow.velocityOverLifetime;

        //transform.position = new Vector3(15f, height, 0f);    ------


        //var main = snow.main;
        //snow.main.emitterVelocity = new Vector3(height, height, 0f);
        //mainModule.emitterVelocity = new Vector3(500f, height*10f, 0f); 

        //transform.position = new Vector3(15f, (13.0f/speed), 0f);
    }
}
