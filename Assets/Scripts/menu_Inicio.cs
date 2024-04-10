using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_Inicio : MonoBehaviour
{

    public void PlayGame(string Scene1)
    {
        SceneManager.LoadScene(Scene1);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }
}
