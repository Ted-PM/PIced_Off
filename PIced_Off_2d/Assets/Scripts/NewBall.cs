using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NewBall : MonoBehaviour
{
    public static NewBall Instance;

    float secondsPassed;
    public float timePerSpawn;
    public float currentTime;

    Vector2 distance;                       // because cam change
    public Transform camLocation;           // where cam is

    public GameObject fakeBallPrefab;

    int numBalls;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timePerSpawn = Random.Range(10, 15);
    }

    void RollBall()
    {
        GameObject newFakeBall = Instantiate(fakeBallPrefab);       // make fake ball

        distance = new Vector2 (camLocation.position.x, camLocation.position.y);        // find location right outside main cam view

        newFakeBall.transform.position = newFakeBall.transform.position * distance;     // move ball there before starts rolling self, also cuz initial y pos is 0, multiply 
    }
    void Update()
    {
        if (secondsPassed >= timePerSpawn)
        {
            currentTime += secondsPassed;
            if (BallSpawner.Instance.GetNumBalls() < 8)
            {
                RollBall();
            }
           
            secondsPassed = 0;

            timePerSpawn = Random.Range(10, 15);        // how long between new fake ball
        }

        secondsPassed += Time.deltaTime;
    }
}
