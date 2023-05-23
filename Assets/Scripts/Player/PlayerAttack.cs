using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;                // Prefab for the projectile
    public float projectileSpeed = 10f;                // Speed at which the projectile moves

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Check for player input (e.g., mouse click)
        if (Input.GetMouseButtonDown(0))
        {
            // Spawn a projectile
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Calculate the direction from player to mouse position
        Vector3 direction = mousePosition - transform.position;
        direction.Normalize();

        // Instantiate the projectile and set its direction and speed
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileComponent = projectile.GetComponent<Projectile>();
        if (projectileComponent != null)
        {
            projectileComponent.SetDirection(direction);
            projectileComponent.SetSpeed(projectileSpeed);
        }
    }
}
