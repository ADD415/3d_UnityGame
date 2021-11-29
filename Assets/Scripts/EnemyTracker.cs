using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    public GameObject closestEnemy;

    void Update()
    {
        CheckDistanceToEnemies(enemyList);

        if (closestEnemy != null)
        {
            foreach (GameObject enemy in enemyList)
            {
                if (closestEnemy == enemy)
                {
                    enemy.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
                }
                else
                {
                    enemy.GetComponent<Renderer>().material.color = new Color(0, 1, 0);
                }
            }
        }
    }


    public void CheckDistanceToEnemies(List<GameObject> objectsToCheck)
    {
        float minDistance = float.MaxValue;
        foreach (GameObject enemy in objectsToCheck)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);

            if (distance < minDistance)
            {
                closestEnemy = enemy;
                minDistance = distance;
            }
        }
    }
}
