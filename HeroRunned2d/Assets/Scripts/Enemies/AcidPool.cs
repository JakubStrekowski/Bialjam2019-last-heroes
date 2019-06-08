using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPool : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(Flash));
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            for (float i = 0.6f; i <= 1f; i += 0.01f)
            {
                spriteRenderer.color = new Color(color.r, color.g, color.b, i);
                yield return new WaitForSeconds(0.02f);
            }

            for (float i = 1f; i >= 0.6f; i -= 0.01f)
            {
                spriteRenderer.color = new Color(color.r, color.g, color.b, i);
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
}
