using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2d;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(Flash));
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            var hero = other.gameObject.GetComponent<Hero>();
            hero.Die();
        }
    }

    private IEnumerator Flash()
    { 
        var color = spriteRenderer.color;
        while (true)
        {
            collider2d.enabled = true;
            spriteRenderer.color = new Color(color.r, color.g, color.b, 180f);
            yield return new WaitForSeconds(3f);
            collider2d.enabled = false;
            spriteRenderer.color = new Color(color.r, color.g, color.b, 0f);
            yield return new WaitForSeconds(2f);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 180f);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 0f);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 180f);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(color.r, color.g, color.b, 0f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
