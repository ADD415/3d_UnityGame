using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    [Header("Spawn")]
    public Transform spawnPoint;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            CharacterController cc = col.GetComponent<CharacterController>(); // Reference character controller

            cc.enabled = false; // Disable character controller
            col.gameObject.transform.position = spawnPoint.position; // Takes player to spawn point position
            cc.enabled = true; // Re-enable character controller
        }
    }


}
