using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroContainer : MonoBehaviour
{
    public GameObject swapEffect;
    private Vector3 savedVelocity;

    public enum HeroType
    {
        Ninja,
        Technomancer,
        Tank,
        Warrior
    }
    
    public struct SkillState
    {
        public bool isSkillActive;
        public float maxCooldownSecs;
    }
    
    public Camera camera;
    public HeroType defaultHero;
    public GameObject ninja;
    public GameObject technomancer;
    public GameObject tank;
    public GameObject warrior;
    public GameObject activeHero { get; private set; }
    public GameObject heroesPanelObj;
    public bool dead = false;
    private CameraFollowing cameraFollowing;
    private HeroesPanel heroesPanel;

    public SkillState ninjaSkillState;
    public SkillState tankSkillState;
    public SkillState technomancerSkillState;
    public SkillState warriorSkillState;

    public GameObject ninjaCooldown;
    public GameObject tankCooldown;
    public GameObject technomancerCooldown;
    public GameObject warriorCooldown;

    private Image ninjaCooldownImage;
    private Image tankCooldownImage;
    private Image technomancerCooldownImage;
    private Image warriorCooldownImage;

    // Start is called before the first frame update
    void Start()
    {
        savedVelocity = Vector3.zero;
        ninjaSkillState = new SkillState
        {
            isSkillActive = true,
            maxCooldownSecs = 1f
        };
        tankSkillState = new SkillState
        {
            isSkillActive = true,
            maxCooldownSecs = 1.3f
        };
        technomancerSkillState = new SkillState
        {
            isSkillActive = true,
            maxCooldownSecs = 8f
        };
        warriorSkillState = new SkillState
        {
            isSkillActive = true,
            maxCooldownSecs = 0.4f
        };
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

        ninjaCooldownImage = ninjaCooldown.GetComponent<Image>();
        tankCooldownImage = tankCooldown.GetComponent<Image>();
        technomancerCooldownImage = technomancerCooldown.GetComponent<Image>();
        warriorCooldownImage = warriorCooldown.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            return;
        }
        if (Input.GetKey(KeyCode.W))
        {
            SwapHero(ninja, activeHero, HeroType.Ninja);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            SwapHero(technomancer, activeHero, HeroType.Technomancer);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            SwapHero(tank, activeHero, HeroType.Tank);
        }
        else if (Input.GetKey(KeyCode.D))
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
        savedVelocity = activeHero.GetComponent<Rigidbody2D>().velocity;
        HideAllCooldowns();
        switch (heroType)
        {
            case HeroType.Ninja:
                if (ninjaSkillState.isSkillActive)
                {
                    ninjaCooldownImage.enabled = false;
                }
                else
                {
                    ninjaCooldownImage.enabled = true;
                }
                break;
            case HeroType.Tank:
                if (tankSkillState.isSkillActive)
                {
                    tankCooldownImage.enabled = false;
                }
                else
                {
                    tankCooldownImage.enabled = true;
                }
                break;
            case HeroType.Technomancer:
                if (technomancerSkillState.isSkillActive)
                {
                    technomancerCooldownImage.enabled = false;
                }
                else
                {
                    technomancerCooldownImage.enabled = true;
                }
                break;
            case HeroType.Warrior:
                if (warriorSkillState.isSkillActive)
                {
                    warriorCooldownImage.enabled = false;
                }
                else
                {
                    warriorCooldownImage.enabled = true;
                }
                break;
        }
        
        activeHero = hero;
        activeHero.transform.position = previousHero.transform.position;
        activeHero.SetActive(true);
        Instantiate(swapEffect, activeHero.transform.position, Quaternion.identity);
        cameraFollowing.Target = activeHero.transform;
        previousHero.SetActive(false);
        activeHero.GetComponent<Rigidbody2D>().velocity = savedVelocity;
        heroesPanel.ChangeActiveHero(heroType);
    }

    private void HideAllCooldowns()
    {
        ninjaCooldownImage.enabled = false;
        tankCooldownImage.enabled = false;
        technomancerCooldownImage.enabled = false;
        warriorCooldownImage.enabled = false;
    }

    public void StartSkill(HeroType heroType)
    {
        switch (heroType)
        {
            case HeroType.Ninja:
                StartCoroutine(nameof(StartNinjaSkillCoroutine));
                break;
            case HeroType.Tank:
                StartCoroutine(nameof(StartTankSkillCoroutine));
                break;
            case HeroType.Technomancer:
                StartCoroutine(nameof(StartTechnomancerSkillCoroutine));
                break;
            case HeroType.Warrior:
                StartCoroutine(nameof(StartWarriorSkillCoroutine));
                break;
        }
    }

    private IEnumerator StartNinjaSkillCoroutine()
    {
        ninjaCooldownImage.enabled = true;
        ninjaSkillState.isSkillActive = false;
        var maxCooldownSecs = ninjaSkillState.maxCooldownSecs;
        for (var i = 0f; i <= maxCooldownSecs; i += 0.1f)
        {
            ninjaCooldownImage.fillAmount = i / maxCooldownSecs;
            yield return new WaitForSeconds(0.1f);
        }
        ninjaSkillState.isSkillActive = true;
        ninjaCooldownImage.enabled = false;
    }
    
    private IEnumerator StartTankSkillCoroutine()
    {
        tankCooldownImage.enabled = true;
        tankSkillState.isSkillActive = false;
        var maxCooldownSecs = tankSkillState.maxCooldownSecs;
        for (var i = 0f; i <= maxCooldownSecs; i += 0.1f)
        {
            tankCooldownImage.fillAmount = i / maxCooldownSecs;
            yield return new WaitForSeconds(0.1f);
        }
        tankSkillState.isSkillActive = true;
        tankCooldownImage.enabled = false;
    }

    private IEnumerator StartTechnomancerSkillCoroutine()
    {
        technomancerCooldownImage.enabled = true;
        technomancerSkillState.isSkillActive = false;
        var maxCooldownSecs = technomancerSkillState.maxCooldownSecs;
        for (var i = 0f; i <= maxCooldownSecs; i += 0.1f)
        {
            technomancerCooldownImage.fillAmount = i / maxCooldownSecs;
            yield return new WaitForSeconds(0.1f);
        }
        technomancerSkillState.isSkillActive = true;
        technomancerCooldownImage.enabled = false;
    }

    private IEnumerator StartWarriorSkillCoroutine()
    {
        warriorCooldownImage.enabled = true;
        warriorSkillState.isSkillActive = false;
        var maxCooldownSecs = warriorSkillState.maxCooldownSecs;
        for (var i = 0f; i <= maxCooldownSecs; i += 0.1f)
        {
            warriorCooldownImage.fillAmount = i / maxCooldownSecs;
            yield return new WaitForSeconds(0.1f);
        }
        warriorSkillState.isSkillActive = true;
        warriorCooldownImage.enabled = false;
    }
}
