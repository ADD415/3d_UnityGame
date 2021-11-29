using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Manager : MonoBehaviour
{

    [Header("Pausing")]
    public bool paused = false;
    public Slider fovSlider;
    public Camera cam;

    public float time = 0;
    public int playerScore = 0;
    public GameObject playerScoreUI;
    public GameObject timeUI;

    public GameObject PauseMenu;
    public GameObject options;

    // Start is called before the first frame update
    void Start()
    {
        // When game is paused use a slider to change the fov of the camera.
        fovSlider.gameObject.SetActive(false);
        cam.fieldOfView = fovSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeUI.gameObject.GetComponent<Text>().text = ("" + (int)time);
        playerScoreUI.gameObject.GetComponent<Text>().text = ("Score: " + playerScore);


        // If player presses pause 
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paused) // if the game is paused
            {
                Resume(); // Resume the game from the resume component
            }
            else
            {
                Pause();
            }
        }      
        cam.fieldOfView = fovSlider.value; // Assigns the cameras fov to that of the value of the slider.

    }

    void Resume()
    {
        Time.timeScale = 0f; // Set time.timescale to 0
        paused = false; // Set paused to false

        PauseMenu.gameObject.SetActive(true); // Activate pause menu
        Cursor.lockState = CursorLockMode.None; // Cursor control
        return; // return from script
    }

    void Pause()
    {
        Time.timeScale = 1f; // Set time.timescale to 1
        paused = true; // Set paused to true

        PauseMenu.gameObject.SetActive(false); // Disables pause menu
        fovSlider.gameObject.SetActive(false); // Silder cannot be accessed
        options.gameObject.SetActive(false); // Disable options
        Cursor.lockState = CursorLockMode.Locked; // Removes cursor control
    }

    public void Menu()
    {
        fovSlider.gameObject.SetActive(true); // Activate slider
        options.gameObject.SetActive(true); // Activate options
    }

    public void Quit()
    {
        Debug.Log("Quitting game"); // Load text in log for quitting
        Application.Quit();      
    }

    public void Score_increase()
    {
        playerScore += 10;
    }

    public void Score_Decrease()
    {
        playerScore -= 10;
    }

}
