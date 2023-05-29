using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;                          // Direction of the projectile
    private float speed;                                // Speed of the projectile
    public float lifespan = 5f;                          // Lifespan of the projectile in seconds
    public AudioClip fireSound;                         // Sound clip to play when the projectile is fired
    public AudioClip hitSound;                          // Sound clip to play when the projectile hits an enemy

    private AudioSource audioSource;
    private float destroyTime;                           // Time at which the projectile should be destroyed

    private SpriteRenderer spriteRenderer;              // Reference to the SpriteRenderer component
    private UnityEngine.Rendering.Universal.Light2D light2D;                             // Reference to the Light2D component
    private ParticleSystem particleSystem;               // Reference to the ParticleSystem component

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        destroyTime = Time.time + lifespan;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        light2D = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        particleSystem = GetComponentInChildren<ParticleSystem>();

        if (fireSound != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }

    private void Update()
    {
        // Move the projectile in the set direction and speed
        transform.position += direction * speed * Time.deltaTime;

        // Check if the projectile's lifespan has ended
        if (Time.time >= destroyTime)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if collided with an enemy
        if (collision.CompareTag("Enemy"))
        {
            // Get the enemy component and apply damage
            FlyingEnemy enemy = collision.GetComponent<FlyingEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }

            // Play hit sound at the collision position
            if (hitSound != null)
                AudioSource.PlayClipAtPoint(hitSound, collision.transform.position);

            // Hide the visuals (optional)
            if (spriteRenderer != null)
            {
                spriteRenderer.enabled = false;
            }

            // Disable the collider
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            // Disable the Light2D component (optional)
            if (light2D != null)
            {
                light2D.enabled = false;
            }

            // Disable the ParticleSystem component (optional)
            if (particleSystem != null)
            {
                particleSystem.Stop();
            }

            // Start coroutine to destroy the projectile after sound finishes playing
            StartCoroutine(DestroyAfterSound());
        }
    }

    private IEnumerator DestroyAfterSound()
    {
        // Wait for the hit sound to finish playing
        yield return new WaitForSeconds(fireSound.length);

        // Destroy the projectile
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        // Destroy the projectile object
        Destroy(gameObject);
    }
}
