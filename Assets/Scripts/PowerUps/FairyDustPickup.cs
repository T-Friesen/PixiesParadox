using UnityEngine;

public class FairyDustPickup : MonoBehaviour
{
    public int pickupCount = 1;                          // Amount of pickup to be collected
    public PlayerUI playerUI;                            // Reference to the PlayerUI script
    public AudioClip pickupSound;                        // Sound clip to play when the pickup is collected
    public float pickupVolume = 1f;                      // Volume of the pickup sound

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if collided with the player
        if (collision.CompareTag("Player"))
        {
            // Increase the pickup count in the PlayerUI
            playerUI.AddPickupCount(pickupCount);

            // Play pickup sound if available
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position, pickupVolume);
            }

            // Destroy the pickup object
            Destroy(gameObject);
        }
    }
}
