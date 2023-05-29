using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public PlayerHealth playerHealth;       // Reference to the PlayerHealth script

private void OnTriggerEnter2D(Collider2D collision)
{
    // Check if collided with the player
    if (collision.CompareTag("Player"))
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            if (playerHealth.currentLives < playerHealth.maxLives)
            {
                // Add 1 health to the player
                playerHealth.AddHealth();

                // Destroy the pickup object
                Destroy(gameObject);
            }
        }
    }
}
}
