using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBall : MonoBehaviour
{
    
    public float fakeBallVelocity;
    Rigidbody2D rigidBody2D;

    public Animator rollAnimator;
    float animSpeedControl = 1f;
    void Start()
    {
        
        rigidBody2D = GetComponent<Rigidbody2D>();
        if (animSpeedControl < 1 || GameManager.Instance.MenuIsActive() == true) // if at start of round, or if player died
        {
            animSpeedControl = (BadSpawner.Instance.logTotalTime / 8);
        }
        //anim.SetFloat("speed", X);
        //animSpeedControl = ((BadSpawner.Instance.totalTime)/8);
        
        rollAnimator.SetFloat("SpeedMultiplier", animSpeedControl);
    }

    void Update()
    {
        
        rigidBody2D.velocity = Vector2.left * fakeBallVelocity;

        if (GameManager.Instance.MenuIsActive() == true)        // if head destroyed
        {
            RemoveCollider();       // remove own collider so don't mess up game by adding new ball
        }

    }

    void RemoveCollider()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY; // freeze y positon (no collider = fall through ground)
        CircleCollider2D collider = gameObject.GetComponent<CircleCollider2D>(); // get own collider

        if (collider != null)       // cuz being called in update, check if collider already removed
        {
            Destroy(collider);      // if not removed, destroy it
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)  // make sure ball touching ground before can jump
    {
        //Debug.Log("collision");
        if (collision.gameObject.GetComponent<BallControler>())        // if hit ball then add new one
        {
            //Debug.Log("collide with triangle");
            BallSpawner.Instance.SpawnBall();
            Destroy(gameObject);
        }
        else if (collision.gameObject.GetComponent<AddScore>() || collision.gameObject.GetComponent<BulletTemp>())  // else die
        {
            Destroy(gameObject);
        }
    }


}
