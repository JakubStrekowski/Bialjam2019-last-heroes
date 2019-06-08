using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Hero
{
    bool isAbilityReady=true;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Ability") > 0.3f && isAbilityReady)
        {
            StartCoroutine("AttackSkill");
        }
    }

    IEnumerator AttackSkill()
    {
        isAbilityReady = false;
        GetComponent<Animator>().SetBool("IsAttacking", true);
        //todo start animation
        yield return new WaitForSeconds(0.36f);
        GetComponent<Animator>().SetBool("IsAttacking", false);
        isAbilityReady = true;
    }
}
