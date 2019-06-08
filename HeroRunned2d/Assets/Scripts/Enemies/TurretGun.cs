using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TurretGun : MonoBehaviour
{
    public HeroContainer lookAt;
    private Collider2D attackCircle;
    
    // Start is called before the first frame update
    void Start()
    {
        attackCircle = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {    
        Vector3 vectorToTarget = lookAt.activeHero.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 360f);
    }
}
