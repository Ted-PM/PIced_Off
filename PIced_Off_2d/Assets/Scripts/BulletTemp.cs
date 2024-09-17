using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTemp : MonoBehaviour
{
    float bulletVelocity;
    //float bulletVelocitieMultiplier;

    Rigidbody2D rigidBody2D;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        bulletVelocity = BadSpawner.Instance.currentTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletVelocity < 10)
        {
            bulletVelocity = 10;
        }

        rigidBody2D.velocity = Vector2.left * bulletVelocity;// * (BadSpawner.Instance.currentTime/10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy (rigidBody2D);
        //Debug.Log("triangle collision");
        Destroy(gameObject);
    }
}