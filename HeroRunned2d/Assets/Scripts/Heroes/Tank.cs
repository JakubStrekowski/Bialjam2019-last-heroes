using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Hero
{
    
    public bool isWallInRange = false;
    private GameObject wallToDestroy;
    bool punchReady = true;
    private void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    
    private void FixedUpdate()
    {
        base.FixedUpdate();
        if (isWallInRange)
        {
            if (Input.GetAxis("Ability")>0.3f&&punchReady)
            {
                punchReady = false;
                StartCoroutine("PrepareToDestroy");
            }
        }
    }
    // Update is called once per frame

    public void SetWall(GameObject wall)
    {
        if (wall!=null)
        {
            wallToDestroy = wall;
            isWallInRange = true;
        }
        else
        {
            wallToDestroy = null;
            isWallInRange = false;
        }
    }

    IEnumerator PrepareToDestroy()
    {
        GameObject wall = wallToDestroy;
        animator.SetBool("Hit", true);
        if (wall != null)
        {
            wall.GetComponent<DestroyableWall>().StartDestroying();
        }
        yield return new WaitForSeconds(1.3f);
        animator.SetBool("Hit", false);
        Debug.Log("Hit anima");
        punchReady = true;

    }


}
