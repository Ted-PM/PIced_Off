using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BadSpawner : MonoBehaviour
{
   public static BadSpawner Instance;

    float secondsPassed;
    public float timePerSpawn;
    public float currentTime;

    public GameObject badPrefab;

    //public float yHeight;   // how height want thing spawn

    Vector2 height;     // hol
    float spawnHeight;
    int numBalls;

    //bool spawnHeight = true;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        SpawnBad();
        //height = new Vector2(1, yHeight);       // x = 1 because *= later
    }

    // Update is called once per frame
    void Update()
    {
        if (secondsPassed >= timePerSpawn)
        {
            currentTime+=secondsPassed;

            SpawnBad();
            secondsPassed = 0;
        }

        secondsPassed += Time.deltaTime;
        //currentTime += Time.deltaTime;
    }

    void SpawnBad()
    {
        GameObject newBad = Instantiate(badPrefab);     // creates thing

        //newBad.GetComponent<Rigidbody2D>().velocity = Vector2.left * currentTime;

        numBalls = BallSpawner.Instance.GetNumBalls();      // get number of balls in play
        Debug.Log("Num balls = " + numBalls);

        spawnHeight = Random.Range(0, (numBalls + 1));

        //if (numBalls > 1)
        //{
        //    spawnHeight = (float)(Random.Range(0, (numBalls+2)));         // get random height (+2 so can have bad above whole snowman)
        //}
        //else
        //{
        //    spawnHeight = (float)(Random.Range(0, 2));          // 0 or 1
        //}
        //spawnHeight = (float)(Random.Range(0, (numBalls + 2)));         // get random height (+2 so can have bad above whole snowman)


        //if (spawnHeight != 0)     // if heigh = 0 then its chill
        //{
        //    spawnHeight -= (float)0.5;      // if not -0.5 cuz ball diameter is 1 but ball rad is .5
        //}

        //spawnHeight = spawnHeight - (float)0.5;      // if not -0.5 cuz ball diameter is 1 but ball rad is .5
        //Debug.Log("spawnHeight = "+ spawnHeight);


        height = new Vector2(1, spawnHeight);

        //newBad.transform.position = height;

        //Debug.Log("curr heigh = " + newBad.transform.position.y);
        //Debug.Log("curr X = " + newBad.transform.position.x);

        newBad.transform.position = newBad.transform.position * height;         // move bullet up / down

        //Debug.Log("new heigh = " + newBad.transform.position.y);
        //Debug.Log("new X = " + newBad.transform.position.x);

        //if (!spawnHeight)
        //{
        //    newBad.transform.position = transform.position*height;      // move bullet up / down

        //    spawnHeight = true;
        //}
        //else
        //{
        //    spawnHeight = false;
        //}


        //Destroy(newBad, 5);
    }
}
