using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject[] waypoints;
    public float speed;
    int current = 0;
    public float rotSpeed;
    public float WPradius = 1;
    
    void Update()
    {
      if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius) // Check distance between current waypoint and current object position. Move to next waypoint.
      {
            current++; // If less than waypoint radius then trigger this. Current++ adds onto the current value.
            if (current >= waypoints.Length) // Checks if int value of current is larger or equal the index value of waypoints. Reached last checkpoint back to start.
            {
                current = 0;
            }
      }
      transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed); // Moves the object to the waypoint current transform.position moves by vector 3, allow speed to be changed.

    }
}
