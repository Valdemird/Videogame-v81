using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerLives;
    public Slider healthBar;
    public int internalPlayerLives;

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
        DontDestroyOnLoad(gameObject);
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

    void GameOver()
    {
        Debug.Log("Game Over");
        BulletPool.Instance.ResetPool();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        playerLives = 3;
    }
}