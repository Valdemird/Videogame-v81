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

    [SerializeField] private GameObject gameOverMenu;

    public static menuGame Instance { get; private set; }

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

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
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
        HideAllMenus(); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void HideAllMenus()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    public void Options()
    {
        Debug.Log("Opening options menu...");
        HideAllMenus();
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

    public void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;  // Pausa el juego cuando el menú de game over esté activo
    }

    public void HideGameOverMenu()
    {
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;  // Reanuda el juego (útil si reinicias el nivel)
    }

        public void Home(string Scene0)
    {
        HideAllMenus(); 
        Time.timeScale = 1f;
        // Cargar el menú principal
        SceneManager.LoadScene(Scene0);
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Solo se ejecuta dentro del Editor de Unity
    #else
        Application.Quit(); // Se ejecuta en una aplicación compilada
    #endif
    }



}
