using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] waypoints;
    public float movementSpeed = 3f;
    public Rigidbody2D rb;
    public Collider2D attackCollider;

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
        if (ismoving)
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
        yield return new WaitForSeconds(2f);
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        //TODO start moving animation
        ismoving = true;
    }

    IEnumerator Attack()
    {
        while (heroInRange)
        {
            attackCollider.enabled = true;
            ismoving = false;
            if (attackWithDamage)
            {
                Debug.Log("attacking with hit");
            }
            else
            {
                attackCollider.enabled = false;
                Debug.Log("attacking with miss");
            }
            //TODO start attack animation
            
            yield return new WaitForSeconds(2f);
        }
    }

}
