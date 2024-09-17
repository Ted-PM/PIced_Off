using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BulletTemp>())        // check that obj hit was bullet
        {
            ScoreManager.Instance.AddPoint();       // call add point
        }
    }
}
