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

    public Sprite headSprite;
    public Sprite bodySprite;

    public ParticleSystem dieParticles;

    int numberOfPrefabs;

    bool isHead = false;

    public AudioSource jumpSound;
    //public AudioSource ballDieSound;

    //public bool isSelected;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        //jumpSound = GetComponent<AudioSource>();
        //gameObject.GetComponent<SpriteRenderer>().sprite = bodySprite;
    }

    void Update()
    {

        //if (isSelected) { Debug.Log("isselected"); }

        //if (canJump) { Debug.Log("canJumpRN"); }

        //if (isHead)
        //{
        //    //gameObject.GetComponent<Renderer>().material.color = Color.green;
        //    gameObject.GetComponent<SpriteRenderer>().sprite = headSprite;
        //}
        //else if (isSelected)
        //{
        //    gameObject.GetComponent<Renderer>().material.color = Color.red;
        //}
        //else
        //{
        //    gameObject.GetComponent<Renderer>().material.color = Color.white;
        //}

        if (canJump && (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")) && isSelected)
        {
            //Debug.Log("selected and jumping");

            Jump();
        }
    }

    public void Select()        // just wether or not can jump
    {
        if (!isSelected) 
        { 
            isSelected = true;
            gameObject.GetComponent<Renderer>().material.color = Color.cyan;
            //var col = gameObject.GetComponent<Renderer>().material.color;
            //col.a = 0.5f;
            //gameObject.GetComponent<Renderer>().material.color = col;
            //float trans = 0.1f;
            //col.a = trans;
            //gameObject.GetComponent<Renderer>().material.color.a = 0.5f;
            //gameObject.GetComponent<Renderer>().material.color = Color.RGBToHSV(Color.red, 0, 0, 0);
            //gameObject.GetComponent<Renderer>().material.color = Color.HSVToRGB(0, 1, 5);

        }
    }
    public void DeSelect()
    {
        if (isSelected) 
        {
            isSelected = false;
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
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
        jumpSound.pitch = 2.0f / (float)numberOfPrefabs;
        jumpSound.Play();
        rigidBody2D.velocity = Vector2.up * jumpVelocity * numberOfPrefabs; // increase velocity based on ^ to push other balls up
        canJump = false;
    }

    public void Lose()
    {
        ParticleSystem diePart = Instantiate(dieParticles);
        diePart.transform.position = gameObject.transform.position;

        BallSpawner.Instance.ballList.Remove(gameObject);       // remove self from ball spawner list (this 1 line replaced a fkin paragraph in "MoveHeightDown()"
        //BallSpawner.Instance.playSound(ballDieSound);

        Destroy(gameObject);                            // destroy self
        if (!isHead)
        {
            
            BallSpawner.Instance.MoveHeightDown();      // spawn new ball lower
        }
        Debug.Log("lose");

        if (isHead)
        {
            //dieSound.Play();
            BallSpawner.Instance.LoseAll();
            
        }
        //Destroy(diePart);
    }

    public bool CheckCanJump() { return canJump; }

    public void SetHead()
    {
        isHead = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = headSprite;

        //Debug.Log("head set");
    }

    public void RemoveHead()
    {
        isHead = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = bodySprite;
        //Debug.Log("head set");
    }

    public bool IsHead() { return isHead; }

    public void ThrowBall(int speed)                // called if head destroyed
    {
        //RemoveCollider();                                 // looks dubious tbf
        rigidBody2D.AddForce(Vector2.right * speed);            // move ball
        StartCoroutine(DelayThenDestroy());     // call coroutine to kill self
    }   

    private void RemoveCollider()       // not used rn tbh, but kinda interensting (used in fake ball)
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        CircleCollider2D collider = gameObject.GetComponent<CircleCollider2D>();

        if (collider != null)
        {
            Destroy(collider);
        }
    }

    private IEnumerator DelayThenDestroy()
    {
        yield return new WaitForSeconds(2);             // wait 2 sec

        BallSpawner.Instance.ballList.Remove(gameObject);       // remove self from ball spawner list (this 1 line replaced a fkin paragraph in "MoveHeightDown()"

        Destroy(gameObject);            // destroy self
    }
}
