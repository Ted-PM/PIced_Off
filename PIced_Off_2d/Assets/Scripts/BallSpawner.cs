using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance;
    public GameObject ballPrefab;

    public List <GameObject> ballList;          // keep list of ball prefabs instantiated

    float instatiateHeight;

    int ballSelector;               // which ball chosen by player

    public Vector3 initialScale = new Vector3(1, 1, 1); // Default scale for new prefabs
    public float scaleFactor = 0.3f;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        instatiateHeight = 0;                   // how high ball will be spawned, increase on each spawn / decrease if destroyed
        ballList = new List <GameObject>();
        spawnStart();
    }

    void Update()
    {
        

        if ((ballList.Count - 1) > 0 && (ballList[ballList.Count - 1].GetComponent<BallControler>().IsHead()))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                SelectBallUp();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                SelectBallDown();
            }

            ChangeCamFOV();

            ballList[ballList.Count - 1].GetComponent<BallControler>().SetHead();
            ReSizeBalls();
        }
    }

    void ReSizeBalls()
    {
        for (int i = 0; i < (ballList.Count - 1); i++)
        {
            float yPosition = ballList[i].transform.position.y;             // get initial Y position of ball

            float scaleValue = initialScale.x + (ballList.Count - i)*scaleFactor;                              // make new scale of x = initial scale + "relative postion" in list
            ballList[i].transform.localScale = new Vector3(scaleValue, initialScale.y, initialScale.z);     // make local scale chage (same y / z, but new x scale val)
            ballList[i].transform.position = new Vector3(-5, yPosition, 0);                                 // -5 = origonal position of ball, move ball to own origonal y val

            CircleCollider2D collider = ballList[i].GetComponent<CircleCollider2D>();               // get circle collide (if don't do this, circle collider scale asw when prfab scale)
            if (collider != null)
            {
                collider.radius = 0.5f / ballList[i].transform.localScale.x;        // origonal circle collider radius = 0.5, radius still = 0.5 after scale, but is 0.5 of new scaled obj, so divide 0.5 by new scale
            }
        }
    }
    void spawnStart()
    {
        SpawnBall();
        SpawnBall();
        SpawnBall();
        SelectBottom();
        ballList[2].GetComponent<BallControler>().SetHead();
    }

    void ChangeCamFOV()
    {
        if (Camera.main.fieldOfView < (30 + (GetNumBalls() * 10)))      // if more balls added do
        {
            Camera.main.fieldOfView++;                      // increase FOV
            Camera.main.transform.Translate(0.3f, 0, 0);        // move cam so not see behind
        }
        else if (Camera.main.fieldOfView > (30 + (GetNumBalls() * 10)))
        {
            Camera.main.fieldOfView--;
            Camera.main.transform.Translate(-0.3f, 0, 0);
        }
    }

    void SelectBottom()
    {
        ballSelector = 0;

        DeselectAllBall();    

        ballList[0].GetComponent<BallControler>().Select();

        //Debug.Log("selected bottom");
    }

    void SelectTop()
    {
        ballSelector = ballList.Count - 1;

        DeselectAllBall();

        ballList[ballList.Count - 1].GetComponent<BallControler>().Select();
    }

    void DeselectAllBall()
    {
        for(int i = 0; i < ballList.Count -1; i++)
        {
            ballList[i].GetComponent<BallControler>().DeSelect();
        }

        //Debug.Log("deselect all");
    }

    void SelectBallUp()
    {
        //Debug.Log("selectBall " + ballSelector);

        if (ballSelector == ballList.Count - 1)       // if last in list, deselect and go bottom
        {
            ballList[ballSelector].GetComponent<BallControler>().DeSelect();
            SelectBottom();
        }
        else
        {
            //Debug.Log("moveSelectionUp");
            ballList[ballSelector].GetComponent<BallControler>().DeSelect();        // not last in list deselect and select next
            ballSelector++;
            ballList[ballSelector].GetComponent<BallControler>().Select();
        } 

        //Debug.Log("Ball selected " + ballSelector);
    }

    void SelectBallDown()
    {
        //Debug.Log("selectBall " + ballSelector);

        if (ballSelector == 0)       // if last in list, deselect and go bottom
        {
            ballList[ballSelector].GetComponent<BallControler>().DeSelect();
            SelectTop();
        }
        else
        {
            //Debug.Log("moveSelectionUp");
            ballList[ballSelector].GetComponent<BallControler>().DeSelect();        // not last in list deselect and select next
            ballSelector--;
            ballList[ballSelector].GetComponent<BallControler>().Select();
        }

        //Debug.Log("Ball selected " + ballSelector);
    }

    public void SpawnBall()
    {
        GameObject newBall = Instantiate(ballPrefab);

        if (ballList.Count > 0)
        {
            ballList[ballList.Count - 1].GetComponent<BallControler>().RemoveHead();
        }

        ballList.Add(newBall);                          // adds to start, then "higher" balls are after (Lowest->highest in list)
        ballList[ballList.Count - 1].GetComponent<BallControler>().SetHead();

        if (ballList.Count > 1)
        {
            newBall.transform.position = new Vector2(transform.position.x, (ballList[ballList.Count - 2].GetComponent<BallControler>().transform.position.y + 1));   //Incase the top ball is jumping, create above that one
        }
        else
        {
            newBall.transform.position = new Vector2(transform.position.x, instatiateHeight);   // instantiate height derived by num prev balls made / died

        }

        MoveHeightUp();

        //ReSizeBalls();
    }

    public int NumBallsAbove(BallControler selectedBall)        // find number of balls above one selected (to change how much force will need to move self and others)
    {
        int amntAbove = 0;    

        float ballH = selectedBall.transform.position.y;        // (position of lowest = 0 then 1 then 2 etc.

        //Debug.Log("total balls = " + ballList.Count);

        //Debug.Log("height of ball = " + ballH);


        amntAbove = ballList.Count - (int)(ballH +0.5);     // get total num balls - own position (+0.5 cuz acc position like 0.9999 and this dubmass rounds down)

        //Debug.Log("num balls above acc = " + amntAbove);

        return amntAbove;
    }

    public int GetNumBalls()
    {
        return ballList.Count;
    }

    void MoveHeightUp()     // when ball made, move up height for next spawn to be at
    {
        instatiateHeight += 1;
    }
    public void MoveHeightDown()        // ball destroyed move height down then select lowest ball
    {
        instatiateHeight -= 1;

        Debug.Log("curent balls = " + ballList.Count);

        if (ballSelector != 0)
        {
            ballList[ballSelector - 1].GetComponent<BallControler>().DeSelect();        // just trust this line, weird stuff happen without (case 3 balls, selected top ball and middle breaks, wierd reselection stuff) 
        }

        //Debug.Log("MoveHD new num = " + ballList.Count);

        SelectBottom();
    }

    public void LoseAll()
    {
        if (ballList.Count > 0)
        {
            Debug.Log("start lose all");

            for (int i = (ballList.Count - 1); i >= 0; i--)        // go through list
            {
                ballList[i].GetComponent<BallControler>().DeSelect();
                ballList[i].GetComponent<Rigidbody2D>().constraints = 0;        // change locked x axis to not locked
                ballList[i].GetComponent<BallControler>().ThrowBall((i + 1) * 500);     // call throw ball (higher ball = heigher i, so throw further)
            }
        }
        Debug.Log("finish lose all");

        StartCoroutine(DelayGameOver());
    }

    private IEnumerator DelayGameOver()     // wait 3 seconds before call game over (balls wait 2 s before destroy self)
    {
        yield return new WaitForSeconds(3);

        GameManager.Instance.GameOver();
    }
}


