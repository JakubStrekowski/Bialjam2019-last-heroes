using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    public float movementSpeed;
    public float maximumSpeed;
    public float accelerateration;
    private float stoppingRatio=0.7f;
    public bool isOnGround;
    Rigidbody2D rb;


    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        if (rb.velocity.x > maximumSpeed)
        {
            rb.velocity = new Vector2(maximumSpeed * stoppingRatio, rb.velocity.y);
        }
        if (rb.velocity.x < -maximumSpeed)
        {
            rb.velocity = new Vector2(-maximumSpeed * stoppingRatio, rb.velocity.y);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            Debug.Log("moving");
            rb.velocity = new Vector2(1 * accelerateration, rb.velocity.y);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            rb.velocity = new Vector2(-1 * accelerateration, rb.velocity.y);
        }
        if (Input.GetAxis("Horizontal" ) == 0)
        {
            if (Mathf.Abs(rb.velocity.x) > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x*stoppingRatio, rb.velocity.y);
                if (rb.velocity.x < 0.05f)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            }
        }

    }

    void OnDeath()
    {

    }
}
