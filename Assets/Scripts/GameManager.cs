using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerLives;
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
            GameOver();
        }
    }

    public void GameOver()
    {
        BulletPool.Instance.ResetPool();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        playerLives = 3;
    }
}