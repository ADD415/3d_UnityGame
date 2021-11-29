using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Score : MonoBehaviour
{
    public Manager manager;

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Points")
        {
            manager.Score_increase();
        }

        if (col.gameObject.tag == "Damage")
        {
            manager.Score_Decrease();

        }

    }

}
