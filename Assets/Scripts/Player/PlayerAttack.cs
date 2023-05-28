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
        // Raycast from the camera to the mouse position to get the world position
        Ray mouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.back, Vector3.zero);
        float rayDistance;
        Vector3 mousePositionWorld = Vector3.zero;

        if (groundPlane.Raycast(mouseRay, out rayDistance))
        {
            mousePositionWorld = mouseRay.GetPoint(rayDistance);
        }

        // Calculate the direction from the player to the mouse position
        Vector3 direction = mousePositionWorld - transform.position;
        direction.Normalize();

        // Instantiate the projectile and set its position and rotation
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Set the projectile's direction and speed
        Projectile projectileComponent = projectile.GetComponent<Projectile>();
        if (projectileComponent != null)
        {
            projectileComponent.SetDirection(direction);
            projectileComponent.SetSpeed(projectileSpeed);
        }
    }
}
