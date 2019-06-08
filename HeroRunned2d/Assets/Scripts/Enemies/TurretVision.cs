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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            coroutine = HandleHeroCollision(other.gameObject.GetComponent<Hero>());
            StartCoroutine(coroutine);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hero") && coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    private IEnumerator HandleHeroCollision(Hero hero)
    {
        while (true)
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
