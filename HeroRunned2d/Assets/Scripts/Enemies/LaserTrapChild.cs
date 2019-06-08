using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrapChild : BaseEnemy
{
    private LaserTrap laserTrap;

    // Start is called before the first frame update
    void Start()
    {
        laserTrap = GetComponentInParent<LaserTrap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void Stop()
    {
        laserTrap.Stop();
    }

    public override void Resume()
    {
        laserTrap.Resume();
    }
}
