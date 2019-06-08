using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    public float movementSpeed;
    public float maximumSpeed;
    public float accelerateration;
    public Transform head;
    public Transform body;
    public Transform legs;
    private float stoppingRatio=0.98f;
    protected bool isOnGround;
    protected Rigidbody2D rb;

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
        rb.velocity = new Vector2(  Input.GetAxis("Horizontal") * movementSpeed, rb.velocity.y);
    }

    public void Die()
    {
        Debug.Log("Dead");
    }
}
