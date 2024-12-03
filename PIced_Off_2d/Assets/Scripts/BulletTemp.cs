using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTemp : MonoBehaviour
{
    float bulletVelocity;
    //float bulletVelocitieMultiplier;

    Rigidbody2D rigidBody2D;
    public Animator fireAnim;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        if(BallSpawner.Instance.GetNumBalls() == 0)
        {
            RemoveCollider();
        }
        //bulletVelocity = BadSpawner.Instance.currentTime;
        bulletVelocity = BadSpawner.Instance.logTotalTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletVelocity < 10 || GameManager.Instance.MenuIsActive() == true) // if at start of round, or if player died
        {
            bulletVelocity = 10;
        }
        else if (bulletVelocity > 30)
        {
            bulletVelocity = 30;
        }

            rigidBody2D.velocity = Vector2.left * bulletVelocity;   // 

        if (GameManager.Instance.MenuIsActive() == true || BallSpawner.Instance.Lost == true)        // if head destroyed
        {
            RemoveCollider();       // remove own collider so don't mess up game by adding new ball
        }

        //fireAnim.transform = gameObject.transform;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("triangle collision");
        Destroy(gameObject);
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
}
