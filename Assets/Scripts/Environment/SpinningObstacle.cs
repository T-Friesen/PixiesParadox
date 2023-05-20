using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObstacle : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;                   // Damage inflicted by the enemy
    [SerializeField]
    public float spinSpeed = 5f;                  // The rotation speed of the object
    

    private void Update() {
        transform.Rotate(Vector3.back, spinSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage();
        }
    }
}
