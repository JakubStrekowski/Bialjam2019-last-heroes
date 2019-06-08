using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : Hero
{
    public Transform groundCheck;
    bool jumpReady;
    bool didSecondJump;
    bool finishedHolding;
    float jumpCooldown = 1f;

    int groundLayer;
    private void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        groundLayer = LayerMask.NameToLayer("GroundDetect");
        isOnGround = false;
        jumpReady = false;
        didSecondJump = true;
        finishedHolding = true;
        StartCoroutine("JumpCooldown");
    }

    // Update is called once per frame
    void Update()
    {
        //base.Update();
    }
    private void FixedUpdate()
    {
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
            if (jumpReady&&Input.GetAxis("Ability") > 0.3)
            {
                StartCoroutine("JumpCooldown");
                rb.AddForce(Vector2.up * 80, ForceMode2D.Impulse);
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
                    rb.AddForce(Vector2.up * 80, ForceMode2D.Impulse);
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
        jumpReady = false;
            yield return new WaitForSeconds(jumpCooldown);
        jumpReady = true;
    }

}
