using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float moveSpeed = 5f;                     // Speed at which the enemy moves
    public int maxHealth = 5;                        // Maximum health of the enemy

    private int currentHealth;                       // Current health of the enemy
    private Transform player;                         // Reference to the player's transform
    private Animator animator;                        // Animator component for animation
    private Vector2 movementDirection;                // Direction of enemy movement

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
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
            Destroy(gameObject);
        }
    }
}
