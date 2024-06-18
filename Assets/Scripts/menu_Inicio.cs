using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio; 

public class menu_Inicio : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public AudioMixer audioMixer;

    public void PlayGame(string Scene1)
    {
        SceneManager.LoadScene(Scene1);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }

    public void Options()
    {
        Debug.Log("Opening options menu...");
        optionsMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        Debug.Log("Closing options menu...");
        optionsMenu.SetActive(false);
    }

    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void CambiarVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

}
