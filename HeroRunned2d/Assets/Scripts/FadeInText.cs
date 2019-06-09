using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInText : MonoBehaviour
{
    public Image sr;
    public float timeToWait = 1f;
    public float howFastAppear=0.1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FadeIn");
    }
    
    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(timeToWait);
        Color color;
        float alpha = 0.0f;
        for (int i = 0; i < 20; i++)
        {
            alpha += 0.05f;
            color = sr.color;
            sr.color = new Color(color.r, color.g, color.b, alpha);
            yield return new WaitForSeconds(howFastAppear);
        }
    }
}
