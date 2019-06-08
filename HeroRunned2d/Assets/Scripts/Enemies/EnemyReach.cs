using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReach : MonoBehaviour
{
    public Enemy parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            parent.heroInRange = true;
            parent.ismoving = false;
            var position = transform.position;
            var playerMask = LayerMask.GetMask("Player", "Shield");
            Hero hero = collision.gameObject.GetComponent<Hero>();
            RaycastHit2D raycastHit = Physics2D.Raycast(position, hero.gameObject.transform.position - position, 8f, playerMask);
            if (raycastHit.collider != null && raycastHit.collider.CompareTag("Hero"))
            {
                parent.attackWithDamage = true;
            }
            if (raycastHit.collider != null && !raycastHit.collider.CompareTag("Hero"))
            {
                parent.attackWithDamage = false;
            }
            parent.StartCoroutine("Attack");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            parent.heroInRange = false;
            parent.ismoving = true;
        }
    }
}