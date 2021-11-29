using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    [Header("Movement")]
    public float speed = 10f;
    Vector3 velocity;
    public float gravity = -8f;

    [Header("Ground stuff")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    [Header("Health")]
    public int lives = 3;
    public int livesmax = 5;
    public GameObject GameOver;
    public GameObject HealthA;
    public GameObject HealthB;
    public GameObject HealthC;
    public GameObject HealthD;
    public GameObject HealthE;

    void Start()
    {
        controller = GetComponent<CharacterController>(); // Check the game object the script is attached too and get the component of the specified type.
    }

    void Update()
    {
        // Check to detect a collision.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // First parameter is the position of the game object, second is radius and third is specified physics layer.

        if (isGrounded && velocity.y < 0) // Check results of the physics check and current velocity then removing excess ve;ocity.
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal"); // Horizontal movement
        float z = Input.GetAxis("Vertical"); // Vertical movement

        Vector3 move = transform.right * x + transform.forward * z; // Transform.right takes the direction to the right of the player and transform.forward takes the forward direction of the player.
        controller.Move(move * speed * Time.deltaTime); // Direction to move. Time.deltaTime is the completion time in seconds since the last frame. Regardless of the fps, the game will be executed at the same speed.

        if (Input.GetButtonDown("Jump") && isGrounded) // Check if can jump and get value
        {
            velocity.y = 5f;       
        }

        // Simulates gravity applying a constant velocity to the player
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Damage") // If collide with game object tagged damage then lose a life
        {
            lives--;
        }

        if (col.gameObject.tag == "HealthUp" && lives < livesmax)
        {
            lives++;
            Destroy(col.gameObject);
        }
        

        if (lives <= 4) // If lives is less than or equal to 2 remove health from health bar
        {
            HealthE.gameObject.SetActive(false);
        }
        else
        {
            HealthE.gameObject.SetActive(true);
        }

        if (lives <= 3) // If lives is less than or equal to 2 remove health from health bar
        {
            HealthD.gameObject.SetActive(false);
        }
        else
        {
            HealthD.gameObject.SetActive(true);
        }

        if (lives <= 2) // If lives is less than or equal to 2 remove health from health bar
        {
            HealthA.gameObject.SetActive(false);
        }
        else
        {
            HealthA.gameObject.SetActive(true);
        }

        if (lives <= 1) // If lives is less than or equal to 1 remove health from health bar
        {
            HealthB.gameObject.SetActive(false);
        }
        else
        {
            HealthB.gameObject.SetActive(true);
        }

        if (lives <= 0) // If lives is less than or equal to 0 remove health from health bar and display game over
        {
            HealthC.gameObject.SetActive(false);
            GameOver.gameObject.SetActive(true);
            Time.timeScale = 0; // Pauses game
        }

    }

}
