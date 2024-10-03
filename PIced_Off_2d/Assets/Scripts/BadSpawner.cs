using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BadSpawner : MonoBehaviour
{
   public static BadSpawner Instance;

    float secondsPassed;
    public float timePerSpawn;      // max time between spawns

    float randomTimePerSpawn;       
    public float totalTime;

    public float logTotalTime;

    public GameObject badPrefab;

    Vector2 height;     // hol
    float spawnHeight;
    int numBalls;

    public Transform camLocation;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        randomTimePerSpawn = timePerSpawn;
    }
    void Update()
    {
        if (secondsPassed >= randomTimePerSpawn)
        {
            //currentTime+=secondsPassed;

            SpawnBad();
            secondsPassed = 0;
            randomTimePerSpawn = Random.Range(2, timePerSpawn);

        }

        secondsPassed += Time.deltaTime;
        totalTime += Time.deltaTime;
        //logTotalTime = Mathf.Log(totalTime*10,2);
        logTotalTime = totalTime / 5;
    }

    void SpawnBad()
    {
        GameObject newBad = Instantiate(badPrefab);     // creates thing


        numBalls = BallSpawner.Instance.GetNumBalls();      // get number of balls in play
        Debug.Log("Num balls = " + numBalls);

        spawnHeight = Random.Range(0, (numBalls + 1));        //-------     how high ball will spawn
        //spawnHeight = 2;

        height = new Vector2(camLocation.position.x, spawnHeight);          // has cam location (right outside cam fov) for x position

        newBad.transform.position = newBad.transform.position * height;         // move bullet up / down

    }
}
