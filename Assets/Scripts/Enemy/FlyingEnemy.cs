using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlyingEnemy : MonoBehaviour
{
    public float moveSpeed = 5f;                     // Speed at which the enemy moves
    public int maxHealth = 5;                        // Maximum health of the enemy
    public AudioClip deathSound;                     // Sound clip to play when the enemy dies
    public float deathSoundVolume = 1f;              // Volume of the death sound
    public GameObject healthPickupPrefab;            // Prefab of the health pickup
    public float healthPickupChance = 0.5f;          // Chance of spawning a health pickup on death (0-1)

    private int currentHealth;                       // Current health of the enemy
    private Transform player;                         // Reference to the player's transform
    private Animator animator;                        // Animator component for animation
    private Vector2 movementDirection;                // Direction of enemy movement
    private AudioSource audioSource;                  // Reference to the AudioSource component

    public event Action OnDeath;                      // Event triggered when the enemy dies

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (player != null)
        {
            // Move towards the player
            movementDirection = (player.position - transform.position).normalized;
            transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
        }

        // Set animation parameters
        animator.SetBool("IsMoving", movementDirection.magnitude > 0);

        // Flip the object on the x-axis based on movement direction
        if (movementDirection.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (movementDirection.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            // Enemy is defeated
            StartCoroutine(PlayDeathSoundAndDestroy());

            // Check if a health pickup should be spawned
            if (Random.value < healthPickupChance && healthPickupPrefab != null)
            {
                Instantiate(healthPickupPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    private IEnumerator PlayDeathSoundAndDestroy()
    {
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound, deathSoundVolume);
            yield return new WaitForSeconds(deathSound.length); // Wait for the sound to finish playing
        }

        // Trigger the OnDeath event
        OnDeath?.Invoke();

        // Destroy the enemy object
        Destroy(gameObject);
    }
}
