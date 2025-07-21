using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenuScreen; // Assign the start menu Canvas in the Inspector
    public Spawner spawner; // Assign the Spawner script in the Inspector
    public MoveLeft moveLeftScript; // Assign the MoveLeft script in the Inspector

    void Start()
    {
        // Ensure the start menu is visible when the game starts
        startMenuScreen.SetActive(true);

        // Pause the game initially
        Time.timeScale = 0f;
    }

    public void StartNormalGame()
    {
        Debug.Log("Normal difficulty selected");
        PlayerPrefs.SetString("Difficulty", "Normal");
        PlayerPrefs.Save();
        StartGame();
    }

    public void StartHardGame()
    {
        Debug.Log("Hard difficulty selected");
        PlayerPrefs.SetString("Difficulty", "Hard");
        PlayerPrefs.Save();
        StartGame();
    }

    private void StartGame()
    {
        // Hide the start menu Canvas
        startMenuScreen.SetActive(false);

        // Apply the selected difficulty
        ApplyDifficulty();

        // Start the game
        Time.timeScale = 1f; // Unpause the game
    }

    private void ApplyDifficulty()
    {
        string difficulty = PlayerPrefs.GetString("Difficulty", "Normal"); // Default to Normal if not set
        Debug.Log("Applying difficulty: " + difficulty);

        if (difficulty == "Normal")
        {
            spawner.minSpawnInterval = 1.5f;
            spawner.maxSpawnInterval = 3f;
            moveLeftScript.speed = 5f;
        }
        else if (difficulty == "Hard")
        {
            spawner.minSpawnInterval = 1f;
            spawner.maxSpawnInterval = 2.5f;
            moveLeftScript.speed = 7f;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting games");
        Application.Quit(); // Quit the application
    }
}