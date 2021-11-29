using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    [Header("Control")]
    public float sensitivity = 100f;
    public Transform body;
    float xRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // How the cursor should behave
    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime; // Using the built in Unity mouse system to move the x and y axis
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime; // Multiple mouseX and mouseY by the sensitivity variable and time.deltatime. Time.deltatime means the movement will be indepedent of framerate, allowing consistency.

        //Used to rotate the camera around the x-axis.
        xRotation -= mouseY; // Set xRotation to XRotation minus the value of mouseY.
        xRotation = Mathf.Clamp(xRotation, -70f, 70f); // Limit the value xRotation in between the two specified values using Mathf.Clamp().

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Handle the rotation of the camera using Quaternion.
        body.Rotate(Vector3.up * mouseX); // Rotates the Transform linked to the body variable around the Y-axis (Vector3.Up) by the value of mouseX.

    }

}
