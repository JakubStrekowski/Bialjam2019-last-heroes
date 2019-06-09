using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEnemy
{
    public AudioClip audioClip;
    public AudioClip audioClip2;

    public AudioSource audioSource;
    public GameObject bloodParticle;
    bool isFrozen=false;
    public Transform[] waypoints;
    public float movementSpeed = 3f;
    public Rigidbody2D rb;
    public Collider2D attackCollider;
    public Animator animator;
    int currentWaypoint;
    float moveDirection;
    [HideInInspector]
    public bool ismoving = true;
    [HideInInspector]
    public bool heroInRange = false;
    [HideInInspector]
    public bool attackWithDamage = true;
    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (ismoving&&!isFrozen)
        {
            float distance = Mathf.Abs(waypoints[currentWaypoint].position.x - transform.position.x);
            if (distance < 0.2f)
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
                StartCoroutine("WaitBeforeSwitchDirection");
            }
            float direction = (waypoints[currentWaypoint].position.x - transform.position.x);
            if (direction < 0)
            {
                moveDirection = -1;
            }
            else
            {
                moveDirection = 1;
            }
            rb.velocity = new Vector2(moveDirection * movementSpeed, rb.velocity.y);
        }
    }


    IEnumerator WaitBeforeSwitchDirection()
    {
        ismoving = false;
        //TODO start idle animation
        yield return new WaitForSeconds(0.01f);
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        //TODO start moving animation
        ismoving = true;
    }

    IEnumerator Attack()
    {
        if(!isFrozen)
        while (heroInRange)
        {
            attackCollider.enabled = true;
            ismoving = false;
            if (attackWithDamage)
            {
                animator.SetTrigger("Attack");

                Debug.Log("attacking with hit");
            }
            else
            {
                    attackCollider.gameObject.SetActive(false);
                attackCollider.enabled = false;
                animator.SetTrigger("Attack");

                Debug.Log("attacking with miss");
            }
            
            //TODO start attack animation
            
            yield return new WaitForSeconds(2f);
                attackCollider.gameObject.SetActive(true);
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyKiller"))
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(bloodParticle, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(audioClip);
        audioSource.PlayOneShot(audioClip2);
        GetComponent<Collider2D>().enabled = false;
        rb.simulated = false;
        StartCoroutine("DeathAnimation");
    }

    IEnumerator DeathAnimation()
    {
        Debug.Log("Death anim start");
        ismoving = false;
        //TODO start death animation
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }

    public override void Stop()
    {
        isFrozen = true;
    }

    public override void Resume()
    {
        isFrozen = false;
    }
}
