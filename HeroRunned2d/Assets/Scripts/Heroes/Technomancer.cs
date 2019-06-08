using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Technomancer : Hero
{
    float skillCooldownTime=8f;
    bool skillReady = true;
    public GameObject timeSphere;
   

    private void Awake()
    {
        base.Awake();
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();
        if (Input.GetAxis("Ability") > 0.3f && skillReady)
        {
            StartCoroutine("StopSkill");
        }
    }

    IEnumerator StopSkill()
    {
        skillReady = false;
        animator.SetBool("IsStopTime", true);
        timeSphere.GetComponent<Animator>().SetBool("StopTime", true);

        //todo start animation
        yield return new WaitForSeconds(1);
        timeSphere.SetActive(true);
        animator.SetBool("IsStopTime", false);
        timeSphere.GetComponent<Animator>().SetBool("StopTime", false);


        yield return new WaitForSeconds(4);
        timeSphere.SetActive(false);
        yield return new WaitForSeconds(4);
        skillReady = true;

    }
}
