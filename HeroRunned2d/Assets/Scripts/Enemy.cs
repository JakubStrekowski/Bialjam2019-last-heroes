using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] waypoints;
    public float movementSpeed = 3f;
    public Rigidbody2D rb;
    int currentWaypoint;
    float moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float distance =Mathf.Abs( waypoints[currentWaypoint].position.x - transform.position.x);
        
        if (distance < 0.2f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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
