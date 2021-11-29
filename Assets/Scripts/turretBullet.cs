using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBullet : MonoBehaviour
{
   
    void Start()
    {
        StartCoroutine("LifetimeExpiry");
    }


    public IEnumerator LifetimeExpiry()
    {
        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
