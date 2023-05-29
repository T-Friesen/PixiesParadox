using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;              // Prefab of the enemy to spawn
    public Transform playerTransform;           // Reference to the player's transform
    public float minSpawnRadius = 5f;           // Minimum distance from the player to spawn enemies
    public float maxSpawnRadius = 10f;          // Maximum distance from the player to spawn enemies
    public int maxEnemies = 5;                  // Maximum number of enemies to spawn
    public float spawnInterval = 2f;            // Interval between enemy spawns
    public LayerMask spawnLayerMask;            // Layer mask for valid spawn positions
    public TeleportWorld teleportWorld;         // Reference to the TeleportWorld script

    private bool isDarkWorld;                    // Flag indicating if the current world is the dark world
    private int currentEnemies;                  // Current number of spawned enemies

    private void Start()
    {
        isDarkWorld = teleportWorld.IsDarkWorld(); // Set initial world state
        currentEnemies = 0;

        // Start spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true) // Run indefinitely
        {
            // Check if the world state has changed
            if (teleportWorld.IsDarkWorld() != isDarkWorld)
            {
                isDarkWorld = teleportWorld.IsDarkWorld();

                if (isDarkWorld)
                {
                    // Dark world entered, reset enemy count
                    currentEnemies = 0;
                }
                else
                {
                    // Light world entered, stop spawning
                    currentEnemies = maxEnemies;
                }
            }

            // Spawn enemies if in the dark world and not at maximum limit
            if (isDarkWorld && currentEnemies < maxEnemies)
            {
                // Get a random position within the spawn radius range
                float spawnRadius = Random.Range(minSpawnRadius, maxSpawnRadius);
                Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
                Vector3 spawnPosition = playerTransform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

                // Check if the spawn position is valid
                if (IsValidSpawnPosition(spawnPosition))
                {
                    // Spawn an enemy at the valid position
                    GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                    currentEnemies++;
                    enemy.GetComponent<FlyingEnemy>().OnDeath += DecrementEnemyCount; // Subscribe to the enemy's OnDeath event
                }
            }

            // Wait for a certain interval before attempting to spawn more enemies
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private bool IsValidSpawnPosition(Vector3 position)
    {
        // Check if the position is valid for spawning by performing a sphere cast
        Collider2D hit = Physics2D.OverlapCircle(position, 1f, spawnLayerMask);
        return hit == null;
    }

    private void DecrementEnemyCount()
    {
        currentEnemies--;
    }
}
