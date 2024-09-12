using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    //Rigidbody2D rigidBody2D;
    

    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BulletTemp>())
        {
            ScoreManager.Instance.AddPoint();
        }
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
