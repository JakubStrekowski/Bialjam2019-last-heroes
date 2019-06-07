﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public float TimeToLose
    {
        get { return time; }
        set
        {
            time = value;
            if(time>0)
            timeTxt.text = time.ToString("f1");
            else
            {
                timeTxt.text = "XX,X";
                timeTxt.color = Color.red;
            }
        }
    }
    public TextMeshProUGUI timeTxt;
    public GameObject gameLostPanel;

    float time;
    bool gameLost = false;
    private void Start()
    {
        time = 5;
        StartCoroutine("CountDown");
    }
    private void Awake()
    {
        gameLostPanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetAxis("Submit") > 0&&gameLost)
        {
            Debug.Log("lol");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void LoseGame()
    {
        Debug.Log("Game Lost");
        gameLostPanel.SetActive(true);
    }

    IEnumerator CountDown()
    {
        while (TimeToLose > 0)
        {
            TimeToLose -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        if (gameLost == false)
        {
            gameLost = true;
            LoseGame();
        }
    }

}