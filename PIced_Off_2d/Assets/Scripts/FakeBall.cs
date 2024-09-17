using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeBall : MonoBehaviour
{
    
    public float fakeBallVelocity;
    Rigidbody2D rigidBody2D;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidBody2D.velocity = Vector2.left * fakeBallVelocity;
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
