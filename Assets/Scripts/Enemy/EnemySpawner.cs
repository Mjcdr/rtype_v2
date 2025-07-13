using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public float spawnInterval = 2f; // Time interval between spawns
    public Transform[] spawnPoints; // Array of spawn points for the enemies

    private void Start()
    {
        // Start the enemy spawning coroutine
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        // Randomly select a spawn point
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        // Instantiate the enemy at the selected spawn point
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}