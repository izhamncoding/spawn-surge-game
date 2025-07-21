using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen; // Assign the game over screen in the Inspector
    public BackgroundMusicController musicController; // Reference to the BackgroundMusicController

    public void TriggerGameOver()
    {
        // Activate the game over screen
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        // Stop the background music
        if (musicController != null)
        {
            musicController.StopMusic();
        }

        // Pause the game
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // Hide the game over screen
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        // Reload the current scene to restart the game
        Time.timeScale = 1f; // Unpause the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}