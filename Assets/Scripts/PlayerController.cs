using UnityEngine;
using TMPro; // Required for TextMeshPro
using System.Collections; // Required for IEnumerator

public class PlayerController : MonoBehaviour
{
    public int distance = 0; // Distance traveled
    public int score = 0;    // Player's score

    public TextMeshProUGUI distanceText; // Reference to the distance TextMeshPro UI
    public TextMeshProUGUI scoreText;    // Reference to the score TextMeshPro UI

    public float jumpForce = 10f; // Force applied when jumping
    public float moveSpeed = 5f;  // Speed at which the player moves left/right
    private Rigidbody2D rb;       // Reference to the Rigidbody2D component
    private bool isGrounded;      // Tracks whether the player is on the ground
    private GameOver gameOverManager; // Reference to the GameOver script
    public AudioSource deathSound; // Reference to the death sound AudioSource

    void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the player!");
        }
        else
        {
            Debug.Log("Rigidbody2D component found!");
        }

        // Find the GameOver script at runtime
        gameOverManager = FindObjectOfType<GameOver>();
        if (gameOverManager == null)
        {
            Debug.LogError("GameOverManager not found!");
        }

        // Ensure the death sound AudioSource is assigned
        if (deathSound == null)
        {
            Debug.LogError("Death sound AudioSource is not assigned!");
        }

        UpdateUI(); // Initialize the UI
        StartCoroutine(IncreaseDistanceAndScore()); // Start the distance and score increase coroutine
    }

    void Update()
    {
        // Handle horizontal movement
        float moveInput = Input.GetAxis("Horizontal"); // Get left/right input (A/D or Left Arrow/Right Arrow)
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Jump when Spacebar or Up Arrow is pressed and the player is grounded
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Apply jump force
            isGrounded = false; // Player is no longer grounded after jumping
            Debug.Log("Player jumped");
        }
    }

    // Method to update the UI
    private void UpdateUI()
    {
        if (distanceText != null)
        {
            distanceText.text = "Distance: " + distance.ToString() + "m";
        }

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // Method to add score when an enemy is killed
    public void AddScore(int amount)
    {
        score += amount; // Add to the score
        UpdateUI();      // Update the UI
    }

    // Coroutine to increase distance and score every second
    private IEnumerator IncreaseDistanceAndScore()
    {
        while (true) // Infinite loop
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            distance += 10; // Increase distance by 10
            score += 10;    // Increase score by 10
            UpdateUI();     // Update the UI
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with an object tagged as "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Player is grounded
            Debug.Log("Player is grounded");
        }

        // Check if the player collides with an object tagged as "Enemy" or "Obstacle"
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over! Player hit an enemy or obstacle.");

            // Play the death sound
            if (deathSound != null)
            {
                deathSound.Play();
                Debug.Log("Death sound played.");

                // Delay game over until the sound finishes playing
                StartCoroutine(DelayedGameOver(deathSound.clip.length));
            }
            else
            {
                // Trigger game over immediately if no death sound is assigned
                TriggerGameOver();
            }
        }
    }

    private IEnumerator DelayedGameOver(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the sound to finish

        // Trigger game over
        if (gameOverManager != null)
        {
            gameOverManager.TriggerGameOver(); // Trigger game over
        }
        else
        {
            Debug.LogError("GameOverManager is null!");
        }
    }

    private void TriggerGameOver()
    {
        // Trigger game over immediately (used if no death sound is assigned)
        if (gameOverManager != null)
        {
            gameOverManager.TriggerGameOver();
        }
        else
        {
            Debug.LogError("GameOverManager is null!");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player stops colliding with an object tagged as "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; // Player is not grounded
            Debug.Log("Player is not grounded");
        }
    }
}