using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : Hero
{
    public Transform groundCheck;
    bool didSecondJump;
    bool finishedHolding;
    float jumpCooldown = 1f;
    private HeroContainer heroContainer;

    int groundLayer;
    private void Awake()
    {
        base.Awake();
        heroContainer = GetComponentInParent<HeroContainer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        groundLayer = LayerMask.NameToLayer("GroundDetect");
        isOnGround = false;
        didSecondJump = true;
        finishedHolding = true;
    }

    // Update is called once per frame
    void Update()
    {
        //base.Update();
    }
    private void FixedUpdate()
    {
        float vertical = rb.velocity.y;
        if (vertical<0)
        {
            animator.SetBool("Ascending", false);
        }

        isOnGround = Physics2D.OverlapCircle(groundCheck.position, 0.4f, groundLayer);
        if (isOnGround)
        {
            didSecondJump = false;
        }
        base.FixedUpdate();
        if (Mathf.Abs(Input.GetAxis("Ability")) < 0.1f&&!finishedHolding)
        {
            finishedHolding = true;
        }
        if (isOnGround)
        {
            if (heroContainer.ninjaSkillState.isSkillActive && Input.GetAxis("Ability") > 0.3)
            {
                heroContainer.StartSkill(HeroContainer.HeroType.Ninja);
                rb.AddForce(Vector2.up * 40, ForceMode2D.Impulse);
                animator.SetTrigger("TakeOF");
                animator.SetBool("Ascending", true);
                audio.PlayOneShot(audioClip);
                //TODO Start jump anim
                isOnGround = false;
                finishedHolding = false;
                didSecondJump = false;
            }
        }
        else
        {
            if (!didSecondJump&&finishedHolding)
            {
                if (Input.GetAxis("Ability") > 0.3)
                {
                    rb.AddForce(Vector2.up * 40, ForceMode2D.Impulse);
                    animator.SetBool("Ascending", true);
                    animator.SetTrigger("TakeOF");
                    audio.PlayOneShot(audioClip);

                    //TODO Start  jump anim
                    didSecondJump = true;
                    isOnGround = false;
                    finishedHolding = false;
                }
            }
        }
    }

    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(jumpCooldown);
    }
}
