using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Hero
{
    public bool isWallInRange = false;
    private GameObject wallToDestroy;
    private HeroContainer heroContainer;
    
    private void Awake()
    {
        base.Awake();
        heroContainer = GetComponentInParent<HeroContainer>();
    }
    // Start is called before the first frame update
    
    private void FixedUpdate()
    {
        base.FixedUpdate();
        if (isWallInRange)
        {
            if (Input.GetAxis("Ability") > 0.3f && heroContainer.tankSkillState.isSkillActive)
            {
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
        heroContainer.StartSkill(HeroContainer.HeroType.Tank);
        if (wall != null)
        {
            wall.GetComponent<DestroyableWall>().StartDestroying();
        }
        yield return new WaitForSeconds(1.3f);
        animator.SetBool("Hit", false);
    }


}
