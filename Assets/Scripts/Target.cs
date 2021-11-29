using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public Manager manager;

    public void TakeDamage (float amount) // Argument feeds data into function, specify damage.
    {
        health -= amount; // Subtract amount from health
        if (health <= 0f)
        {
            Die(); // If health is less than zero then die.
        }
    }

    void Die ()
    {
        Destroy(gameObject); // Destroys game object.
        manager.Score_increase();
    }


}
