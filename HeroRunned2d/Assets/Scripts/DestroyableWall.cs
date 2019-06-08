using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartDestroying()
    {
        StartCoroutine("Destroy");
    }

    IEnumerator Destroy()
    {
        GetComponent<Animator>().SetTrigger("Destroy");
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1.3f);
        Destroy(gameObject);
    }
}
