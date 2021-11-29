using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoles : MonoBehaviour
{
    public float bulletHoleTimer;
    public float bulletHoleTimerMax;

    void Update()
    {
        bulletHoleTimer += Time.deltaTime; // Increases timer by time.deltatime

        // If timer exceeds max value then destroy the object
        if (bulletHoleTimer > bulletHoleTimerMax)
        {
            Destroy(gameObject);
        }
    }
}
