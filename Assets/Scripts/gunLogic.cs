using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gunLogic : MonoBehaviour
{
    [Header("Camera")]
    public Camera cam;

    [Header("Fire Rate")]
    [SerializeField]
    private bool canShoot;
    public float fireRateTimer = 0f;
    public float fireRateTimerMax = 5f;

    [Header("Reloading")]
    [SerializeField]
    public int currentAmmo;
    public int ammoMax = 10;
    private bool reloading;
    private float reloadTimer;
    public float reloadTimerMax = 5f;
    private bool pressedRecently;

    [Header("Force")]
    public float force;

    [Header("Bullet Holes")]
    public GameObject[] bulletHolePrefabs;

    [Header("Damage")]
    public float damage = 10f;
    public float range = 100f;

    [Header("MuzzleFlash")]
    public GameObject MuzzleFlash;

    public AudioSource source;
    public AudioClip gun;
    public GameObject Bullets;
    public GameObject BulletIcon;
    public GameObject Reloading;

    void Start()
    {
        currentAmmo = ammoMax;
        MuzzleFlash.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

        if (currentAmmo <= 0f || reloading == true) // Ammo is less than 0 or player press r
        {
            reloading = true; // If current ammo is less than 0 then classes as reloading
            reloadTimer += Time.deltaTime; // Increment by time.deltatime

            // Finish reloading when reload timer passes max timer
            if (reloadTimer > reloadTimerMax)
            {
                currentAmmo = ammoMax; // Set current ammo to max
                reloading = false; // No longer classed as reloading, allowing player to shoot
                reloadTimer = 0f; // Reset reload timer
                pressedRecently = false; // Sets to false
            }

        }

        if (Input.GetKeyDown(KeyCode.R) && pressedRecently == false) // If player presses the r key and pressed recently is false then begin reloading, manual reloading
        {
            reloading = true; // Classed as reloading
            pressedRecently = true; // Set to true meaning this can't be pressed again until it is false
        }

        if (canShoot == false)
        {
            fireRateTimer += Time.deltaTime; // Increment by time.deltatime
            MuzzleFlash.gameObject.SetActive(false);

            // When the timer passes the max timer allow player to shoot again and reset timer.
            if (fireRateTimer > fireRateTimerMax)
            {
                canShoot = true;
                fireRateTimer = 0;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot == true && reloading == false) // If input from player, bool canshoot is true and bool reloading is false then shoot.
        {
            Shoot();
            ForceFire();
            MuzzleFlash.gameObject.SetActive(true);
        }

        if (currentAmmo >= 1 && reloading == false)
        {
            BulletIcon.gameObject.SetActive(true);
            Bullets.gameObject.SetActive(true);
            Reloading.gameObject.SetActive(false);
        }
        else
        {
            BulletIcon.gameObject.SetActive(false);
            Bullets.gameObject.SetActive(false);
            Reloading.gameObject.SetActive(true);
        }

        Bullets.gameObject.GetComponent<Text>().text = ("x: " + currentAmmo);
        Reloading.gameObject.GetComponent<Text>().text = ("Reloading: " + reloadTimer);

    }

    void Shoot()
    {
        currentAmmo--; // Decrease amount of current ammo when shoot
        canShoot = false; // Prevents shooting again
        source.PlayOneShot(gun);
    }

    public void ForceFire()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0)); // Defines where the ray cast is spawning from. By setting it to 0.5 we are using the middle of the screen.
        RaycastHit hit; // Object that is hit

        if (Physics.Raycast(ray, out hit))
        {

            GameObject choosenBulletHole = bulletHolePrefabs[Random.Range(0, bulletHolePrefabs.Length)]; // Spawn bullet holes
            var tempBullet = Instantiate(choosenBulletHole, hit.point, Quaternion.LookRotation(hit.normal)); // Make bullet hole a local variable and then assign it as a child to whatever is hit
            tempBullet.transform.parent = hit.transform;

            Target target = hit.transform.GetComponent<Target>(); // Get Target script
            if (target != null) // Only do this if found a component 
            {
                target.TakeDamage(damage); // Target takes damage
            }

            BasicEnemy remove = hit.transform.GetComponent<BasicEnemy>();
            if (remove != null)
            {
                remove.Kill();
            }

            if (hit.transform.gameObject.tag == "ForceAffected") // If game object with the tag ForceAffected is hit
            {
                var direction = new Vector3(hit.transform.position.x - transform.position.x, hit.transform.position.y - transform.position.y, hit.transform.position.z - transform.position.z); // Direction to apply force, subtract where current position from the position of what was hit 
                hit.rigidbody.AddForceAtPosition(force * Vector3.Normalize(direction), hit.point); // Normalise the vector, adding the force/length of the vector onto the force value.

            }

        }
        
    }

}
