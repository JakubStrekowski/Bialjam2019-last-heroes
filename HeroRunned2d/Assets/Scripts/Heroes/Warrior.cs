using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Hero
{
    private HeroContainer heroContainer;
    
    private void Awake()
    {
        base.Awake();
        heroContainer = GetComponentInParent<HeroContainer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Ability") > 0.3f && heroContainer.warriorSkillState.isSkillActive)
        {
            StartCoroutine("AttackSkill");
        }
    }

    private void OnEnable()
    {
        GetComponent<Animator>().SetBool("IsAttacking", false);
    }

    IEnumerator AttackSkill()
    {
        GetComponent<Animator>().SetBool("IsAttacking", true);
        //todo start animation
        heroContainer.StartSkill(HeroContainer.HeroType.Warrior);
        yield return new WaitForSeconds(0.4f);
        GetComponent<Animator>().SetBool("IsAttacking", false);
    }
}
