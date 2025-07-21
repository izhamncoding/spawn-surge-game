using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 5f; // Speed of movement

    void Update()
    {
        // Move the object to the left
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Destroy the object if it goes off-screen
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}