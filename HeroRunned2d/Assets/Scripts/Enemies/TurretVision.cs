using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class TurretVision : MonoBehaviour
{
    public float shotDelay = 1f;
    private float radius;
    private IEnumerator coroutine;
    private bool running = true;

    // Start is called before the first frame update
    void Awake()
    {
        var circleCollider2D = GetComponent<CircleCollider2D>();
        radius = circleCollider2D.radius * 2;
    }

    // Update is called once per frame
    void Update()
    {
    }
    /*
    public override void Stop()
    {
        running = false;
    }

    public override void Resume()
    {
        running = true;
    }
    */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (running && other.CompareTag("Hero"))
        {
            coroutine = HandleHeroCollision(other.gameObject.GetComponent<Hero>());
            StartCoroutine(coroutine);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (coroutine != null && other.CompareTag("Hero"))
        {
            StopCoroutine(coroutine);
        }
    }

    private IEnumerator HandleHeroCollision(Hero hero)
    {
        while (running)
        {
            var position = transform.position;
            var playerMask = LayerMask.GetMask("Player", "Wall");
            RaycastHit2D raycastHit = Physics2D.Raycast(position, hero.head.position - position, radius, playerMask);
            if (raycastHit.collider != null && raycastHit.collider.CompareTag("Hero"))
            {
                hero.Die();
                break;
            }

            raycastHit = Physics2D.Raycast(position, hero.body.position - position, radius, playerMask);
            if (raycastHit.collider != null && raycastHit.collider.CompareTag("Hero"))
            {
                hero.Die();
                break;
            }

            raycastHit = Physics2D.Raycast(position, hero.legs.position - position, radius, playerMask);
            if (raycastHit.collider != null && raycastHit.collider.CompareTag("Hero"))
            {
                hero.Die();
                break;
            }
            yield return new WaitForSeconds(shotDelay);
        }
    }
}
