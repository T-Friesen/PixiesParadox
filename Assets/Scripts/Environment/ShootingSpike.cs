using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpike : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;                 // Damage inflicted by the obstacle

    [SerializeField]
    private float moveSpeed = 2f;           // Speed at which the spike moves
    [SerializeField]
    private float delayBeforeMove = 1f;     // Delay before the spike starts moving down
    [SerializeField]
    private float delayBeforeReset = 1f;    // Delay before the spike resets its position
    [SerializeField]
    private float movementDistance = 0.02f; // Distance the spike moves before changing direction


    private Vector3 initialPosition;        // Initial position of the spike
    private bool isMoving = false;          // Flag indicating if the spike is currently moving
    private bool isMovingDown = false;      // Flag indicating the direction of movement (down or up)
    private float moveTimer = 0f;           // Timer for tracking movement and delays

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (!isMoving)
        {
            // Wait for the delay before starting movement
            moveTimer += Time.deltaTime;
            if (moveTimer >= delayBeforeMove)
            {
                isMoving = true;
                isMovingDown = true;
                moveTimer = 0f;
            }
        }
        else
        {
            // Move the spike down or up based on the current direction
            float moveDistance = moveSpeed * Time.deltaTime;
            if (isMovingDown)
            {
                transform.Translate(Vector3.down * moveDistance);
                if (transform.position.y <= initialPosition.y - movementDistance)
                {
                    isMovingDown = false;
                }
            }
            else
            {
                transform.Translate(Vector3.up * moveDistance);
                if (transform.position.y >= initialPosition.y)
                {
                    isMovingDown = true;
                    moveTimer = 0f;

                    // Reset the position of the spike after a delay
                    StartCoroutine(ResetPositionAfterDelay());
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage();
        }
    }

    private IEnumerator ResetPositionAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeReset);
        transform.position = initialPosition;
    }
}
