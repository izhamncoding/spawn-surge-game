using UnityEngine;
using System.Collections;

public class BackgroundMusicController : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Ensure the AudioSource is assigned
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
            return;
        }

        // Start playing the background music when the game starts
        audioSource.Play();
        Debug.Log("Background music started.");
    }

    public void StopMusic()
    {
        // Start the fade-out coroutine
        StartCoroutine(FadeOutMusic(2f)); // Fade out over 2 seconds
    }

    private IEnumerator FadeOutMusic(float fadeDuration)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Reset volume for next play
    }
}