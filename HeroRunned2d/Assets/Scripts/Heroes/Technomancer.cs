using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Technomancer : Hero
{
    public GameObject timeSphere;
    private HeroContainer heroContainer;

    private void Awake()
    {
        base.Awake();
        heroContainer = GetComponentInParent<HeroContainer>();
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();
        if (Input.GetAxis("Ability") > 0.3f && heroContainer.technomancerSkillState.isSkillActive)
        {
            StartCoroutine("StopSkill");
        }
    }

    private void OnEnable()
    {
        animator.SetBool("IsStopTime", false);
        timeSphere.GetComponent<Animator>().SetBool("StopTime", false);
        timeSphere.SetActive(false);
    }

    IEnumerator StopSkill()
    {
        animator.SetBool("IsStopTime", true);
        timeSphere.GetComponent<Animator>().SetBool("StopTime", true);
        
        yield return new WaitForSeconds(1);
        
        heroContainer.StartSkill(HeroContainer.HeroType.Technomancer);
        
        timeSphere.SetActive(true);
        animator.SetBool("IsStopTime", false);
        timeSphere.GetComponent<Animator>().SetBool("StopTime", false);
        yield return new WaitForSeconds(4);
        timeSphere.SetActive(false);
        yield return new WaitForSeconds(4);
    }
}
