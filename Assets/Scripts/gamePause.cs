using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuGame : MonoBehaviour


{

    [SerializeField] private GameObject pauseButton;

    [SerializeField] private GameObject pauseMenu;

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


    public void Quit()
    {
        Debug.Log("Quit Game...");
        Application.Quit();
    }


}
