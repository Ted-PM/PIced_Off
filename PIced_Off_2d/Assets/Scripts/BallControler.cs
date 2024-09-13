using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class BallControler : MonoBehaviour
{
    public float jumpVelocity;
    bool canJump = true;
    Rigidbody2D rigidBody2D;
    bool isSelected = false;
    //public BulletTemp bulletBody2D;
    //int ballPos;        // relative position in tower of balls

    int numberOfPrefabs;

    bool isHead = false;

    //public bool isSelected;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if (canJump) { BallSpawner.Instance.CanSelect(); }
        //else { BallSpawner.Instance.CannotSelect(); }

        //numberOfPrefabs = BallSpawner.Instance.NumBallsAbove(this);
        //if (isSelected) { Debug.Log("isselected"); }

        //if (canJump) { Debug.Log("canJumpRN"); }

        if (isHead)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        else if (isSelected)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }

        if (canJump && Input.GetMouseButtonDown(0) && isSelected)
        {
            //Debug.Log("selected and jumping");

            //numberOfPrefabs = BallSpawner.Instance.NumBallsAbove(this);

            Jump();
        }
    }

    public void Select()        // just wether or not can jump
    {
        if (!isSelected) { isSelected = true; }
    }
    public void DeSelect()
    {
        if (isSelected) { isSelected = false; }
    }


    private void OnCollisionEnter2D(Collision2D collision)  // make sure ball touching ground before can jump
    {
        //Debug.Log("collision");
        if (collision.gameObject.GetComponent<BulletTemp>())        // finds out if collied object is a bullet and not ground
        {
            //Debug.Log("collide with triangle");
            Lose();
        }
        else
        {
            canJump = true;
            //Debug.Log("canJump");
        }
    }

    void Jump()
    {
        numberOfPrefabs = BallSpawner.Instance.NumBallsAbove(this);     // get num balls above self (including slelf)

        rigidBody2D.velocity = Vector2.up * jumpVelocity * numberOfPrefabs; // increase velocity based on ^ to push other balls up
        canJump = false;
    }

    public void Lose()
    {
        BallSpawner.Instance.ballList.Remove(gameObject);       // remove self from ball spawner list (this 1 line replaced a fkin paragraph in "MoveHeightDown()"
        //BallSpawner.MoveHeightDown();
        //BallSpawner.Instance.MoveHeightDown();      // spawn new ball lower
        //BallSpawner.Instance.CanSelect();
        

        Destroy(gameObject);                            // destroy self
        BallSpawner.Instance.MoveHeightDown();      // spawn new ball lower
        Debug.Log("lose");

        if (isHead)
        {
            Debug.Log("is head lose all called START");
            BallSpawner.Instance.LoseAll();
            Debug.Log("is head lose all called END");
        }
    }

    public void SetHead()
    {
        isHead = true;
        //Debug.Log("head set");
    }

    public void RemoveHead()
    {
        isHead = false;
        //Debug.Log("head set");
    }

    public bool IsHead() { return isHead; }

    public void ThrowBall(int speed)
    {
        rigidBody2D.AddForce(Vector2.right * speed);

        //BallSpawner.Instance.ballList.Remove(gameObject);
        //Destroy(gameObject);
    }
}
