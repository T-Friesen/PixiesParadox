using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;               // Maximum number of lives
    public int currentLives;               // Current number of lives
    public AudioClip damageSound;          // Sound clip to play when the player gets damaged
    public AudioClip gameOverSound;        // Sound clip to play when the player loses all lives
    private AudioSource audioSource;
    private Rigidbody2D rb;
    private bool isAlive = true;           // Tracks if the player is alive or dead
    public GameOverMenu GameOverMenu;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        currentLives = maxLives;
    }

    public void TakeDamage()
    {
        if (!isAlive)
            return;

        currentLives--;

        if (damageSound != null)
            audioSource.PlayOneShot(damageSound);

        if (currentLives <= 0)
        {
            // Player is out of lives, trigger game over
            GameOver();
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if collided with enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }

}
