using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBall : MonoBehaviour
{
    public static NewBall Instance;

    float secondsPassed;
    public float timePerSpawn;
    public float currentTime;
    float spawnDistance;
    Vector2 distance;

    public GameObject fakeBallPrefab;

    int numBalls;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timePerSpawn = Random.Range(10, 15);
    }

    void RollBall()
    {
        GameObject newFakeBall = Instantiate(fakeBallPrefab);
        numBalls = BallSpawner.Instance.GetNumBalls();
        spawnDistance = 10 * numBalls;
        distance = new Vector2(spawnDistance, 1);

        newFakeBall.transform.position = newFakeBall.transform.position * distance;
    }
    // Update is called once per frame
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
