using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public float scrollSpeed;
    Vector3 initialPosition;
    public float minXPosition;

    void Start()
    {
        initialPosition = transform.position;
        scrollSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        if (transform.position.x < minXPosition)
        {
            transform.position = initialPosition;
            scrollSpeed += 0.05f;
        }
    }
}