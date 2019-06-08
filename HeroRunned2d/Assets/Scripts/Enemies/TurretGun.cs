using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TurretGun : BaseEnemy
{
    public HeroContainer lookAt;
    public TurretVision turretVision;
    private Collider2D attackCircle;
    private bool running = true;
    
    // Start is called before the first frame update
    void Start()
    {
        attackCircle = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            Vector3 vectorToTarget = lookAt.activeHero.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 360f);
        }
    }

    public override void Stop()
    {
        turretVision.Stop();
        running = false;
    }

    public override void Resume()
    {
        turretVision.Resume();
        running = true;
    }
}
