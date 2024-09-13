using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance;
    public GameObject ballPrefab;
    //public Camera myCam;

    public List <GameObject> ballList;          // keep list of ball prefabs instantiated
    //List<GameObject> tempList;

    float instatiateHeight;

    //Vector2 height;

    //int tracker;

    int ballSelector;               // which ball chosen by player

    //bool canSelect = true;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        instatiateHeight = 0;                   // how high ball will be spawned, increase on each spawn / decrease if destroyed
        //ballList = new GameObject[5];
        ballList = new List <GameObject>();
        //tempList = new List<GameObject>();
        //tracker = 0;

        //SpawnBall();
        //SpawnBall();
        //SpawnBall();
        ////SpawnBall();
        ////SpawnBall();
        ////ballSelector = 0;
        ////SelectBall();
        //SelectBottom();
        spawnStart();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))// && canSelect)
        {
            SelectBall();
        }

        //Camera.main.fieldOfView = 90*((GetNumBalls()-1)/ GetNumBalls());
        //myCam.fieldOfView = GetNumBalls() * 60f;
    }

    void spawnStart()
    {
        SpawnBall();
        SpawnBall();
        SpawnBall();
        SelectBottom();
        ballList[2].GetComponent<BallControler>().SetHead();
    }

    //public void CanSelect()
    //{
    //    if (!canSelect) { canSelect = true; }
    //}
    //public void CannotSelect()
    //{
    //    if (canSelect) { canSelect = false; }
    //}

    void SelectBottom()
    {
        
        ballSelector = 0;

        DeselectAllBall();

        

        ballList[0].GetComponent<BallControler>().Select();

        //if (ballList[0] != null)
        //{
        //    //ballList[0].GetComponent<BallControler>().DeSelect();
        //    //ballList[0].GetComponent<BallControler>().Select();

        //    //for (int i = 0; i < ballList.Length; i++)
        //    //{
        //    //    if(ballList[i] != null)
        //    //    {
        //    //ballList[ballSelector].GetComponent<BallControler>().Select();
        //    //    }

        //    //}
        //}

        //Debug.Log("selected bottom");
    }

    void DeselectAllBall()
    {
        for(int i = 0; i < ballList.Count -1; i++)
        {

            ballList[i].GetComponent<BallControler>().DeSelect();

            //if (ballList[i] != null)
            //{
            //    ballList[i].GetComponent<BallControler>().DeSelect();
            //}
        }

        //if (ballList.Count == 1)
        //{
        //    ballList[0].GetComponent<BallControler>().DeSelect();
        //    ballList[1].GetComponent<BallControler>().DeSelect();
        //}

        //ballSelector = 0;

        //Debug.Log("deselect all");
    }

    void SelectBall()
    {
        //canSelect = false;
        //Debug.Log("selectBall " + ballSelector);

        //if (ballList.Count == 1)
        //{
        //    Debug.Log("only 1 ball");
        //    ballList[0].GetComponent<BallControler>().Select();
        //}
        if (ballSelector == ballList.Count-1)       // if last in list, deselect and go bottom
        {
            ballList[ballSelector].GetComponent<BallControler>().DeSelect();
            //DeselectAllBall();
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
        
        //canSelect = true;
    }

    public void SpawnBall()
    {
        //height = new Vector2(1, instatiateHeight);
        GameObject newBall = Instantiate(ballPrefab);
        //newBall.GetComponent<BallControler>().SetHead();
        if (ballList.Count > 0)
        {
            ballList[ballList.Count - 1].GetComponent<BallControler>().RemoveHead();
        }
        //newBall.tag = tracker.ToString();
        ballList.Add(newBall);                          // adds to start, then "higher" balls are after (Lowest->highest in list)
        ballList[ballList.Count - 1].GetComponent<BallControler>().SetHead();
        //ballList.Add(Instantiate(ballPrefab));

        //tracker++;

        newBall.transform.position = new Vector2(transform.position.x, instatiateHeight);   // instantiate height derived by num prev balls made / died
        
        MoveHeightUp();
    }

    public int NumBallsAbove(BallControler selectedBall)        // find number of balls above one selected (to change how much force will need to move self and others)
    {
        int amntAbove = 0;
        //bool found = false;
        //for (int i = 0; i < ballList.Count-1; i++)
        //{
        //    if(ballList[i] != null)
        //        amntAbove++;
        //}

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
        //canSelect = false;
        instatiateHeight -= 1;
        //bool found = false;

        Debug.Log("curent balls = " + ballList.Count);

        if (ballSelector != 0)
        {
            ballList[ballSelector - 1].GetComponent<BallControler>().DeSelect();        // just trust this line, weird stuff happen without (case 3 balls, selected top ball and middle breaks, wierd reselection stuff) 
        }
        //if (ballList.Count == 2)
        //{
        //    if (ballList[0] == null)
        //    {
        //        tempList.Add(ballList[1]);
        //        ballList.RemoveAt(1);
        //        ballList.RemoveAt(0);
        //        ballList.Add(tempList[0]);
        //        Debug.Log("ballList[0] = null");
        //        //tempList.RemoveAt(0);
        //    }
        //    else
        //    {
        //        Debug.Log("ballList[1] = null");
        //        ballList.RemoveAt(1);
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i < ballList.Count - 1; i++)
        //    {
        //        if (ballList[i] != null)
        //        {
        //            tempList.Add(ballList[i]);

        //        }
        //        ballList.RemoveAt(i);

        //        //if(ballList[i] != null && !found)
        //        //{
        //        //    tempList.Add(ballList[i]);
        //        //}
        //        //else if (ballList[i] != null && found)
        //        //{
        //        //    tempList.Add(ballList[i]); 
        //        //}
        //        //else if (ballList[i] == null && !found)
        //        //{
        //        //    found = true;
        //        //}


        //    }

        //    //ballList.Clear();

        //    //tempList[tempList.Length - 1] = null;

        //    for (int i = 0; i < tempList.Count - 1; i++)
        //    {
        //        ballList.Add(tempList[i]);
        //        tempList.RemoveAt(i);
        //    }
        //}

        //tempList.Clear();

        //tracker--;



        //Debug.Log("MoveHD new num = " + ballList.Count);

        //canSelect = true;
        //ballSelector = 0;
        //DeselectAllBall();
        SelectBottom();

    }

    public void LoseAll()
    {
        if (ballList.Count > 0)
        {
            for (int i = (ballList.Count - 1); i >= 0; i--)        // go through list
            {
                ballList[i].GetComponent<BallControler>().DeSelect();
                ballList[i].GetComponent<Rigidbody2D>().constraints = 0;        // change locked x axis to not locked
                ballList[i].GetComponent<BallControler>().ThrowBall((i + 1) * 200);     // call throw ball (higher ball = heigher i, so throw further)
            }
        }

    }


}
