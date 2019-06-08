using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereOfTime : MonoBehaviour
{
    private HashSet<GameObject> frozenEnemies;
    
    // Start is called before the first frame update
    void Start()
    {
        frozenEnemies = new HashSet<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        foreach (var frozenEnemy in frozenEnemies)
        {
            frozenEnemy.SendMessage(nameof(BaseEnemy.Resume));
        }
        frozenEnemies.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var gameObj = other.gameObject;
            gameObj.SendMessage(nameof(BaseEnemy.Stop));
            frozenEnemies.Add(gameObj);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var gameObj = other.gameObject;
            gameObj.SendMessage(nameof(BaseEnemy.Resume));
            frozenEnemies.Remove(gameObj);
        }
    }
}
