using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;                         // Maximum number of lives
    public int currentLives;                         // Current number of lives
    public AudioClip damageSound;                    // Sound clip to play when the player gets damaged
    public AudioClip increaseHealthSound;                    // Sound clip to play when the player gets damaged
    public AudioClip gameOverSound;                  // Sound clip to play when the player loses all lives
    public GameObject healthImagePrefab;             // Prefab for the health image
    public Transform healthImagesContainer;          // Parent transform for the health images
    public float spacing = 10f;                       // Spacing between health images
    public float damageCooldown = 1f;                 // Cooldown time between taking damage

    private AudioSource audioSource;
    private bool isAlive = true;                      // Tracks if the player is alive or dead
    private bool canTakeDamage = true;                // Flag indicating if the player can take damage
    public GameOverMenu GameOverMenu;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentLives = maxLives;
        GenerateHealthImages();
        UpdateHealthUI();
    }

    private void GenerateHealthImages()
    {
        for (int i = 0; i < maxLives; i++)
        {
            GameObject healthImage = Instantiate(healthImagePrefab, healthImagesContainer);
            healthImage.transform.localPosition = new Vector3(i * spacing, 0f, 0f);
        }
    }

    public void TakeDamage()
    {
        if (!isAlive || !canTakeDamage)
            return;

        currentLives--;

        if (damageSound != null)
            audioSource.PlayOneShot(damageSound);

        if (currentLives <= 0)
        {
            // Player is out of lives, trigger game over
            GameOver();
        }
        else
        {
            UpdateHealthUI();
        }

        canTakeDamage = false;
        Invoke(nameof(EnableDamage), damageCooldown);
    }

public void AddHealth()
{
    currentLives++;

    if (currentLives > maxLives)
        currentLives = maxLives;

    if (increaseHealthSound != null)
        audioSource.PlayOneShot(increaseHealthSound);

    UpdateHealthUI();
}

    private void EnableDamage()
    {
        canTakeDamage = true;
    }

    private void GameOver()
    {
        if (gameOverSound != null)
            audioSource.PlayOneShot(gameOverSound);

        // Perform any necessary game over actions (e.g., restart level, show game over screen, etc.)
        GameOverMenu.Setup();
        isAlive = false;
        Time.timeScale = 0;
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < healthImagesContainer.childCount; i++)
        {
            GameObject healthImage = healthImagesContainer.GetChild(i).gameObject;
            healthImage.SetActive(i < currentLives);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collided with enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if collided with an enemy
        if (collision.CompareTag("Enemy"))
        {
            PlayerHealth playerHealth = GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage();
            }
        }
    }
}
