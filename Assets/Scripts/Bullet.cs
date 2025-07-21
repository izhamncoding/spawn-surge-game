using UnityEngine;

public class Bullet : MonoBehaviour
{
    public PlayerController playerController; // Reference to the PlayerController component
    public AudioSource enemyDeathSound; // Reference to the AudioSource component

    private void Start()
    {
        // Find the PlayerController if not assigned
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
            if (playerController == null)
            {
                Debug.LogError("PlayerController not found!");
            }
        }

        // Ensure the enemy death sound AudioSource is assigned
        if (enemyDeathSound == null)
        {
            Debug.LogError("Enemy death sound AudioSource is not assigned!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet collides with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Play the enemy death sound
            if (enemyDeathSound != null)
            {
                enemyDeathSound.Play();
                Debug.Log("Enemy death sound played.");
            }

            // Add 100 to the player's score
            if (playerController != null)
            {
                playerController.AddScore(100);
            }

            Destroy(collision.gameObject); // Destroy the enemy
            Destroy(gameObject);           // Destroy the bullet
        }

        // Check if the bullet collides with an obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject); // Destroy the bullet
        }
    }

    private void OnBecameInvisible()
    {
        // Destroy the bullet when it goes off-screen
        Destroy(gameObject);
    }
}