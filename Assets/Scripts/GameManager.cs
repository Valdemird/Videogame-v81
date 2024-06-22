using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerLives = 3;
    public int livesLost = 0;
    public Slider healthBar;
    public TextMeshProUGUI scoreText;
    public int internalPlayerLives;
    private int score = 0;

    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            internalPlayerLives = playerLives;
            instance = this;
            DontDestroyOnLoad(gameObject);  // Mantén GameManager a través de las escenas si es necesario
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    

    public void gainScorePoints(int points)
    {
        score+= points;
        scoreText.text = "Score: " + score;
    }

    public void DecreaseHealth(int damage)
    {
        playerLives -= damage;
        healthBar.value = playerLives;
        if (playerLives <= 0)
        {
            livesLost++;
            if (livesLost >= 3)
            {
                GameOver();
            }
            else
            {
                RestartLevel();
            }
        }
    }

    private void RestartLevel()
    {
        playerLives = internalPlayerLives;
        healthBar.value = playerLives;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        menuGame.Instance.ShowGameOverMenu(); 
    }

     // Función para reiniciar el juego
    public void RestartGame()
    {
        livesLost = 0;  // Reinicia el contador de vidas perdidas
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    // Función para volver al menú principal
    public void HomeGame()
    {
        SceneManager.LoadScene("Start menu");
    }

    // Función para salir del juego
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Solo se ejecutará en el editor de Unity
#else
        Application.Quit();  // Se ejecuta cuando el juego está construido y ejecutado fuera del editor
#endif
    }

}