using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject gameMaster;
    private TimeManager timeManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        timeManager = gameMaster.GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            timeManager.NewLevel();
        }
    }
}
