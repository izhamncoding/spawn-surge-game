using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos; // Stores the initial position of the background
    private float repeatWidth; // Stores the width of the background
    public float speed = 5f; // Speed of background movement (adjust in the Inspector)

    void Start()
    {
        // Set the initial position of the background
        startPos = transform.position;
        Debug.Log("Start Position: " + startPos);

        // Calculate the width of the background using the SpriteRenderer
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            repeatWidth = spriteRenderer.bounds.size.x; // Use full width
            Debug.Log("Repeat Width: " + repeatWidth);
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found on the background!");
        }
    }

    void Update()
    {
        // Move the background to the left
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        Debug.Log("Background Position: " + transform.position);

        // Check if the background has moved fully off the screen
        if (transform.position.x < startPos.x - repeatWidth)
        {
            // Reset the background position to the starting position
            transform.position = startPos;
            Debug.Log("Background Reset to: " + startPos);
        }
    }
}