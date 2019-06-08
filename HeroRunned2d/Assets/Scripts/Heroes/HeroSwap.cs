using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSwap : MonoBehaviour
{
    public enum HeroType
    {
        Ninja,
        Technomancer,
        Tank,
        Warrior
    }
    
    public Camera camera;
    public HeroType defaultHero;
    public GameObject ninja;
    public GameObject technomancer;
    public GameObject tank;
    public GameObject warrior;
    private CameraFollowing cameraFollowing;
    private GameObject activeHero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        switch (defaultHero)
        {
            case HeroType.Ninja: activeHero = ninja;
                break;
            case HeroType.Tank: activeHero = tank;
                break;
            case HeroType.Technomancer: activeHero = technomancer;
                break;
            case HeroType.Warrior: activeHero = warrior;
                break;
        }
        
        activeHero.SetActive(true);
        cameraFollowing = camera.GetComponent<CameraFollowing>();
        cameraFollowing.Target = activeHero.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            SwapHero(ninja, activeHero);
        }
        if (Input.GetKey(KeyCode.A))
        {
            SwapHero(technomancer, activeHero);
        }
        if (Input.GetKey(KeyCode.S))
        {
            SwapHero(tank, activeHero);
        }
        if (Input.GetKey(KeyCode.D))
        {
            SwapHero(warrior, activeHero);
        }
    }

    private void SwapHero(GameObject hero, GameObject previousHero)
    {
        if (hero == previousHero)
        {
            return;
        }
        
        activeHero = hero;
        activeHero.transform.position = previousHero.transform.position;
        activeHero.SetActive(true);
        cameraFollowing.Target = activeHero.transform;
        previousHero.SetActive(false);
    }
}
