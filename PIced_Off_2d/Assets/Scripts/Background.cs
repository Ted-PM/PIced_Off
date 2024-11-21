using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float scrollSpeed;
    Vector3 initialPosition;
    public float minXPosition;
    public float startPos;
    private void Awake()
    {
        initialPosition = transform.position;
        //Debug.Log(initialPosition.x);
        initialPosition.x = startPos;
        //Debug.Log(initialPosition.x);
    }

    void Start()
    {
        //initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        if (transform.position.x <= minXPosition)
        {
            transform.position = initialPosition;
        }
    }
}
