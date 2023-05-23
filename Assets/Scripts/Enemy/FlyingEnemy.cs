using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float moveSpeed = 5f;                     // Speed at which the enemy moves
    public int maxHealth = 5;                        // Maximum health of the enemy

    private int currentHealth;                       // Current health of the enemy
    private Transform player;                         // Reference to the player's transform

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (player != null)
        {
            // Move towards the player
            Vector2 direction = player.position - transform.position;
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
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
