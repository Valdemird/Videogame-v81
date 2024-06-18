using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class menuGame : MonoBehaviour


{

    [SerializeField] private GameObject pauseButton;

    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject optionsMenu;

    [SerializeField] private AudioMixer audioMixer;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
    }


    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
    }


    public void Restart()
    {
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Options()
    {
        Debug.Log("Opening options menu...");
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        Debug.Log("Closing options menu...");
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }


    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void CambiarVolume(float Volume)
    {
        audioMixer.SetFloat("Volume", Volume);
    }
 


    public void Quit()
    {
        Debug.Log("Quit Game...");
        Application.Quit();
    }


}
