using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    public float movementSpeed;
    public float maximumSpeed;
    public float accelerateration;
    private float stoppingRatio=0.98f;
    protected bool isOnGround;
    protected Rigidbody2D rb;
    public Animator animator;
    private bool isFacingRight=true;
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    public void FixedUpdate()
    {
        
        animator.SetFloat("Speed",Mathf.Abs(rb.velocity.x));

        if (rb.velocity.x > maximumSpeed*0.8)
        {
            if (rb.velocity.x > maximumSpeed)
            {
                rb.velocity = new Vector2(maximumSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(maximumSpeed * stoppingRatio, rb.velocity.y);
            }
        }
        if (rb.velocity.x < -maximumSpeed* 0.8)
        {
            if (rb.velocity.x < -maximumSpeed)
            {
                rb.velocity = new Vector2(-maximumSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-maximumSpeed * stoppingRatio, rb.velocity.y);
            }
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

        if (Input.GetAxis("Horizontal")>0)
        {
            if (!isFacingRight)
            {
                isFacingRight=!isFacingRight;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            if (isFacingRight)
            {
                isFacingRight = !isFacingRight;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
        rb.velocity = new Vector2(  Input.GetAxis("Horizontal") * movementSpeed, rb.velocity.y);

    }

    void OnDeath()
    {

    }
}
