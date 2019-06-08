using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Rigidbody2D rb;

    private void Start()
    {
        rb.gravityScale = 10f;
        rb.mass = 200;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Hero")
        {
            if (collision.gameObject.GetComponent<Tank>() != null)
            {
                rb.gravityScale = 1;
                rb.mass = 2;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Hero")
        {
            if (collision.gameObject.GetComponent<Tank>() != null)
            {
                rb.gravityScale = 10f;
                rb.mass = 200;
            }
        }
    }

}