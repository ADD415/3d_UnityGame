using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public GameObject tracker;
    public Manager manager;

    void Awake()
    {
        tracker = GameObject.FindGameObjectWithTag("Player");
        tracker.GetComponent<EnemyTracker>().enemyList.Add(gameObject);
    }

    public void Kill()
    {
        tracker.GetComponent<EnemyTracker>().enemyList.Remove(gameObject);
        Destroy(gameObject);
        manager.Score_increase();
    }
}
