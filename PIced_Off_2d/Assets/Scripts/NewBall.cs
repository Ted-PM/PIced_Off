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
    public Transform camLocation;

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
        GameObject newFakeBall = Instantiate(fakeBallPrefab);

        distance = new Vector2 (camLocation.position.x, camLocation.position.y);

        newFakeBall.transform.position = newFakeBall.transform.position * distance;
    }
    void Update()
    {
        if (secondsPassed >= timePerSpawn)
        {
            currentTime += secondsPassed;

            RollBall();
            secondsPassed = 0;

            timePerSpawn = Random.Range(10, 15);
        }

        secondsPassed += Time.deltaTime;
    }
}
