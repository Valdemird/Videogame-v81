using UnityEngine;
public class SpawnerBehavior : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnAreaSize = 10f;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private int maxEnemies = 10;

    public static int currentEnemies = 0; // Current number of enemies

    private float nextSpawnTime;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime && currentEnemies < maxEnemies)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnAreaSize / 2, spawnAreaSize / 2),
            0,
            Random.Range(-spawnAreaSize / 2, spawnAreaSize / 2)
        );

        Instantiate(enemyPrefab, transform.position + spawnPosition, Quaternion.identity);
        currentEnemies++; // Increment the number of current enemies
    }

    // Draw the spawn area with Gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize, 0, spawnAreaSize));
    }
}