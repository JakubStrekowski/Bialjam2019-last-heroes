using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroContainer : MonoBehaviour
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
    public GameObject activeHero { get; private set; }
    public GameObject heroesPanelObj;
    private CameraFollowing cameraFollowing;
    private HeroesPanel heroesPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        heroesPanel = heroesPanelObj.GetComponent<HeroesPanel>();
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
        heroesPanel.ChangeActiveHero(defaultHero);
        
        activeHero.SetActive(true);
        cameraFollowing = camera.GetComponent<CameraFollowing>();
        cameraFollowing.Target = activeHero.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            SwapHero(ninja, activeHero, HeroType.Ninja);
        }
        if (Input.GetKey(KeyCode.A))
        {
            SwapHero(technomancer, activeHero, HeroType.Technomancer);
        }
        if (Input.GetKey(KeyCode.S))
        {
            SwapHero(tank, activeHero, HeroType.Tank);
        }
        if (Input.GetKey(KeyCode.D))
        {
            SwapHero(warrior, activeHero, HeroType.Warrior);
        }
    }

    private void SwapHero(GameObject hero, GameObject previousHero, HeroType heroType)
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
        heroesPanel.ChangeActiveHero(heroType);
    }
}
