using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    public float movementSpeed;
    public float maximumSpeed;
    public float accelerateration;
    public Transform head;
    public Transform body;
    public Transform legs;
    public GameObject gameMaster;
    private float stoppingRatio=0.98f;
    protected bool isOnGround;
    protected Rigidbody2D rb;
    public Animator animator;
    private bool isFacingRight=true;
    private TimeManager timeManager;
    private bool dead = false;
	
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        timeManager = gameMaster.GetComponent<TimeManager>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    public void FixedUpdate()
    {
        if (dead)
        {
            return;
        }
        
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

    public void Die()
    {
        timeManager.LoseGame();
        dead = true;
        var heroContainer = GetComponentInParent<HeroContainer>();
        heroContainer.dead = true;
        StartCoroutine(nameof(FlashHero));
    }

    private IEnumerator FlashHero()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        for (var i = 0; i < 3; i++)
        {
            var color = spriteRenderer.color;
            spriteRenderer.color = new Color(color.r, color.g, color.b, 0f);
            yield return new WaitForSeconds(0.15f);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 100f);
            yield return new WaitForSeconds(0.15f);
        }
        var color2 = spriteRenderer.color;
        spriteRenderer.color = new Color(color2.r, color2.g, color2.b, 0f);
    }
}
