using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;                          // Direction of the projectile
    private float speed;                                // Speed of the projectile
    public float lifespan = 5f;                          // Lifespan of the projectile in seconds
    public AudioClip fireSound;                         // Sound clip to play when the projectile is fired
    public AudioClip hitSound;                          // Sound clip to play when the projectile hits an enemy

    private AudioSource audioSource;
    private float destroyTime;                           // Time at which the projectile should be destroyed

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

        if (fireSound != null)
            audioSource.PlayOneShot(fireSound);
    }

private void Update()
{
    // Move the projectile in the set direction and speed
    transform.position += direction * speed * Time.deltaTime;

    // Check if the projectile's lifespan has ended
    if (Time.time >= destroyTime)
    {
        Destroy(gameObject);
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

        // Destroy the projectile
        Destroy(gameObject);
    }
}
}
