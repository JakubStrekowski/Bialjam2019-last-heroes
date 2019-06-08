using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class HeroesPanel : MonoBehaviour
{
    private Image ninjaMaskImage;
    private Image technomancerMaskImage;
    private Image tankMaskImage;
    private Image warriorMaskImage;
    private Image ninjaImage;
    private Image technomancerImage;
    private Image tankImage;
    private Image warriorImage;
    private bool instantiated = false;
    private HeroContainer.HeroType? activeHero;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        ninjaImage = transform.Find("Ninja").GetComponent<Image>();
        ninjaMaskImage = transform.Find("NinjaMask").GetComponent<Image>();
        technomancerImage = transform.Find("Technomancer").GetComponent<Image>();
        technomancerMaskImage = transform.Find("TechnomancerMask").GetComponent<Image>();
        tankImage = transform.Find("Tank").GetComponent<Image>();
        tankMaskImage = transform.Find("TankMask").GetComponent<Image>();
        warriorImage = transform.Find("Warrior").GetComponent<Image>();
        warriorMaskImage = transform.Find("WarriorMask").GetComponent<Image>();
        instantiated = true;
        if (activeHero.HasValue)
        {
            ChangeActiveHero(activeHero.Value);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeActiveHero(HeroContainer.HeroType heroType)
    {
        if (!instantiated)
        {
            activeHero = heroType;
            return;
        }
        
        switch (heroType)
        {
            case HeroContainer.HeroType.Ninja:
                Debug.Log(ninjaMaskImage == null);
                StartCoroutine(FlashPortraits(ninjaMaskImage));
                ResetImageAlpha(ninjaImage);
                FadeImage(tankImage);
                FadeImage(technomancerImage);
                FadeImage(warriorImage);
                break;
            case HeroContainer.HeroType.Tank:
                Debug.Log(tankMaskImage == null);
                StartCoroutine(FlashPortraits(tankMaskImage));
                FadeImage(ninjaImage);
                ResetImageAlpha(tankImage);
                FadeImage(technomancerImage);
                FadeImage(warriorImage);
                break;
            case HeroContainer.HeroType.Technomancer:
                Debug.Log(technomancerMaskImage == null);
                StartCoroutine(FlashPortraits(technomancerMaskImage));
                FadeImage(ninjaImage);
                FadeImage(tankImage);
                ResetImageAlpha(technomancerImage);
                FadeImage(warriorImage);
                break;
            case HeroContainer.HeroType.Warrior:
                Debug.Log(warriorMaskImage == null);
                StartCoroutine(FlashPortraits(warriorMaskImage));
                FadeImage(ninjaImage);
                FadeImage(tankImage);
                FadeImage(technomancerImage);
                ResetImageAlpha(warriorImage);
                break;
        }
    }

    private IEnumerator FlashPortraits(Image image)
    {
        for (float i = 0f; i <= 1f; i += 0.1f)
        {
            var color = image.color;
            image.color = new Color(color.r, color.g, color.b, i);
            yield return new WaitForSeconds(0.01f);
        }

        for (float i = 1f; i >= 0f; i -= 0.1f)
        {
            var color = image.color;
            image.color = new Color(color.r, color.g, color.b, i);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void FadeImage(Image image)
    {
        var color = image.color;
        image.color = new Color(color.r, color.g, color.b, 0.3f);
    }
    
    private void ResetImageAlpha(Image image)
    {
        var color = image.color;
        image.color = new Color(color.r, color.g, color.b, 1f);
    }
}
