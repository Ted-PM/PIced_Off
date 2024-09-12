using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBall : MonoBehaviour
{
    public static NewBall Instance;

    float secondsPassed;
    public float timePerSpawn;
    public float currentTime;

    public GameObject fakeBallPrefab;

    int numBalls;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void RollBall()
    {
        GameObject newFakeBall = Instantiate(fakeBallPrefab);
    }
    // Update is called once per frame
    void Update()
    {
        if (secondsPassed >= timePerSpawn)
        {
            currentTime += secondsPassed;

            RollBall();
            secondsPassed = 0;
        }

        secondsPassed += Time.deltaTime;
    }
}
