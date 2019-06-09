using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInText : MonoBehaviour
{
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FadeIn");
    }
    
    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(1.3f);
        Color color;
        float alpha = 0.0f;
        for (int i = 0; i < 20; i++)
        {
            alpha += 0.05f;
            color = sr.color;
            sr.color = new Color(color.r, color.g, color.b, alpha);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
