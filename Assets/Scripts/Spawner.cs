using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstacles; // Array of obstacle prefabs
    public GameObject[] enemies;   // Array of enemy prefabs
    public float minSpawnInterval = 1f; // Minimum time between spawns
    public float maxSpawnInterval = 3f; // Maximum time between spawns
    public float groundLevel = -3.47f; // Fixed Y-position for spawn points (adjust as needed)

    void Start()
    {
        // Debug: Check if arrays are assigned
        if (obstacles.Length == 0)
        {
            Debug.LogError("Obstacles array is empty! Assign prefabs in the Inspector.");
        }
        if (enemies.Length == 0)
        {
            Debug.LogError("Enemies array is empty! Assign prefabs in the Inspector.");
        }

        // Debug: Log the number of obstacles and enemies
        Debug.Log("Number of obstacles: " + obstacles.Length);
        Debug.Log("Number of enemies: " + enemies.Length);

        // Start spawning objects with a random delay
        ScheduleNextSpawn();
    }

    void ScheduleNextSpawn()
    {
        // Generate a random delay between minSpawnInterval and maxSpawnInterval
        float randomDelay = Random.Range(minSpawnInterval, maxSpawnInterval);

        // Schedule the next spawn
        Invoke("SpawnObject", randomDelay);
    }

    void SpawnObject()
    {
        // Randomly spawn obstacle or enemy
        if (Random.Range(0, 2) == 0)
        {
            if (obstacles.Length > 0)
            {
                // Spawn obstacle
                SpawnAtFixedY(obstacles);
            }
        }
        else
        {
            if (enemies.Length > 0)
            {
                // Spawn enemy
                SpawnAtFixedY(enemies);
            }
        }

        // Schedule the next spawn
        ScheduleNextSpawn();
    }

    void SpawnAtFixedY(GameObject[] objects)
    {
        // Choose a random object from the array
        int index = Random.Range(0, objects.Length);
        GameObject objectToSpawn = objects[index];

        // Calculate the correct spawn position based on the object's height
        float objectHeight = GetObjectHeight(objectToSpawn);
        Vector3 spawnPosition = new Vector3(12.29f, groundLevel + (objectHeight / 2), 0f);

        // Spawn the object
        Debug.Log("Spawning object at position: " + spawnPosition);
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }

    float GetObjectHeight(GameObject obj)
    {
        // Get the height of the object using its SpriteRenderer or Collider
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            return spriteRenderer.bounds.size.y;
        }

        Collider2D collider = obj.GetComponent<Collider2D>();
        if (collider != null)
        {
            return collider.bounds.size.y;
        }

        // Default height if no SpriteRenderer or Collider is found
        Debug.LogWarning("No SpriteRenderer or Collider found on object. Using default height.");
        return 1f;
    }
}