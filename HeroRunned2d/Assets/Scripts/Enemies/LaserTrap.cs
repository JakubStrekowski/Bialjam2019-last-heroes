using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : BaseEnemy
{
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2d;
    private bool coroutinesStared = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(Switch));
        StartCoroutine(nameof(Flash));
        coroutinesStared = true;
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

    public override void Stop()
    {
        if (coroutinesStared)
        {
            StopCoroutine(nameof(Switch));
            StopCoroutine(nameof(Flash));
            coroutinesStared = false;
        }
    }

    public override void Resume()
    {
        if (!coroutinesStared)
        {
            StartCoroutine(nameof(Switch));
            StartCoroutine(nameof(Flash));
            coroutinesStared = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            var hero = other.gameObject.GetComponent<Hero>();
            hero.Die();
        }
    }

    private IEnumerator Switch()
    { 
        while (true)
        {
            collider2d.enabled = true;
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(3f);
            collider2d.enabled = false;
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(2f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator Flash()
    {
        var color = spriteRenderer.color;
        while (true)
        {
            for (float i = 0.3f; i <= 1f; i += 0.02f)
            {
                spriteRenderer.color = new Color(color.r, color.g, color.b, i);
                yield return new WaitForSeconds(0.01f);
            }

            for (float i = 1f; i >= 0.3f; i -= 0.02f)
            {
                spriteRenderer.color = new Color(color.r, color.g, color.b, i);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}
