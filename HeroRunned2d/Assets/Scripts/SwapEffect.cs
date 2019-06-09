using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapEffect : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GetSmaller");
    }

    IEnumerator GetSmaller()
    {
        float size = 1.0f;
        for (int i = 0; i < 20; i++)
        {
            size -= 0.05f;
            transform.localScale = new Vector3(size, size);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}
