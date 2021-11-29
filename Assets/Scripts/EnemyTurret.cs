using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    [Header("Rotation & Visibility")]
    public Vector3 targetDir;
    public GameObject player;
    public float speed = 1f;
    public float maxFollowAngle = 0.8f;
    public float maxFireAngle;
    public float maxVisibiltyDistance;
    public float thurst;

    [Header("Turret")]
    public Renderer turretBody;
    public GameObject turretArm;
    public GameObject bulletSpawn;
    public GameObject turretBullet;

    [Header ("Shooting Time")]
    [SerializeField]
    private float shootTimer;
    public float shootTimerMax;

    private bool turretActive = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Locate player
        StartCoroutine(CheckDistance());
    }

    // Update is called once per frame
    void Update()
    {
        if (turretActive == true) // Determine if player is visible and within distance.
        {
            turretBody.material.SetColor("_Color", new Color32(1, 255, 1, 1)); // Renderer. Use setcolor and get string name and colour value. R= 1 G= 255 B= 1 alpha= 1. Colour32 = 32 bits values between 0 - 255.
            turretArm.GetComponent<Renderer>().material.SetColor("_Color", new Color(0, 1, 0)); // Game object. Get component within object, renderer, then set colour. Values between 0 - 1.

            targetDir = player.transform.position - transform.position; // Get target direction
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, speed * Time.deltaTime, 0.0f); // Use the RotateTowards function and pass it the forward direction, new target, how quickly it moves and a max magnitude.
            transform.rotation = Quaternion.LookRotation(newDirection); // Set new rotation

            if (Vector3.Dot(transform.forward, (player.transform.position - transform.position).normalized) > maxFireAngle) // Dot product for forward transform of turret and current direction of player, if greater than max fire angle then make red.
            {
                turretBody.material.color = new Color(1, 0, 0); // New colour red
                turretArm.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

                shootTimer += Time.deltaTime; // Increase timer

                if (shootTimer > shootTimerMax)
                {
                    var bullet = Instantiate(turretBullet, new Vector3(bulletSpawn.transform.position.x, bulletSpawn.transform.position.y, bulletSpawn.transform.position.z), transform.localRotation);
                    bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * thurst);
                    shootTimer = 0;

                }
            }

            else
            {
                shootTimer = 0; // If leave visibility
            }


        }

        else // If not visible make colour black
        {
            turretBody.material.SetColor("_Color", Color.black); // Using unity built in function to get colour.
            turretArm.GetComponent<Renderer>().material.SetColor("_Color", Color.black); 

            shootTimer = 0; // When not in range set to 0
        }
    }

    private bool PlayerVisible()
    {
        float dot = Vector3.Dot(transform.forward, (player.transform.position - transform.position).normalized); // Dot product for viewing angle.

        if (dot > maxFollowAngle) // Determine if within set angle
            return true; // If greater
        return false; // If isn't greater
    }

    private bool PlayerWithinDistance()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position); // Calculate distance

        if (distance < maxVisibiltyDistance) // Determine distance
            return true; // If distance is less
        return false; // If it isn't less
    }

    public IEnumerator CheckDistance()
    {
        for (; ; )
        {
            turretActive = PlayerWithinDistance() ? PlayerVisible() : false;
            yield return new WaitForSeconds(0.1f);
        }
    }
    
}
