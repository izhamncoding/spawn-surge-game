using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform firePoint;     // Where the bullet spawns
    public float bulletForce = 5f;  // Speed of the bullet (adjust as needed)
    private float spawnDelay = 1f;  // Delay before bullets can be spawned
    private float timer = 0f;       // Timer to track the delay

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if the spawn delay has passed
        if (timer >= spawnDelay)
        {
            // Shoot when the player presses the left mouse button (Fire1)
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }

            // Shoot when the player presses the right mouse button (Fire2)
            if (Input.GetButtonDown("Fire2"))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        if (firePoint == null)
        {
            Debug.LogError("FirePoint is not assigned!");
            return;
        }

        // Create a bullet at the fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Add force to the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Use firePoint.right for horizontal movement (adjust as needed)
            rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
            Debug.Log("Bullet velocity: " + rb.linearVelocity); // Debug the bullet's velocity
        }
    }
}