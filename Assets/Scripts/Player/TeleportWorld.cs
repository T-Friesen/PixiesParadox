using UnityEngine;

public class TeleportWorld : MonoBehaviour
{
    [SerializeField]
    private float upwardMovementDistance = 2f;   // Distance to move the player upward
    [SerializeField]
    private float downwardMovementDistance = 2f; // Distance to move the player downward

    public GameObject lightEnvironment;         // Reference to the light environment GameObject
    public GameObject darkEnvironment;          // Reference to the dark environment GameObject
    public AudioSource cameraAudioSource;       // Reference to the AudioSource component on the camera

    private Vector3 initialPosition;             // Initial position of the player
    private bool isMovingUp = false;              // Flag indicating if the player is currently moving up
    private bool isMovingDown = false;            // Flag indicating if the player is currently moving down

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            if (!isMovingUp && !isMovingDown)
            {
                // Player is at the initial position, teleport the player upward from the current position
                Vector3 targetPosition = transform.position + Vector3.up * upwardMovementDistance;
                TeleportPlayer(targetPosition);

                // Disable light environment and enable dark environment
                if (lightEnvironment != null)
                    lightEnvironment.SetActive(false);
                if (darkEnvironment != null)
                    darkEnvironment.SetActive(true);

                // Adjust camera sound
                if (cameraAudioSource != null)
                    cameraAudioSource.volume = 0.5f;

                isMovingUp = true;
            }
            else if (isMovingUp)
            {
                // Player is already moving up, teleport the player downward from the current position
                Vector3 targetPosition = transform.position - Vector3.up * downwardMovementDistance;
                TeleportPlayer(targetPosition);

                // Enable light environment and disable dark environment
                if (lightEnvironment != null)
                    lightEnvironment.SetActive(true);
                if (darkEnvironment != null)
                    darkEnvironment.SetActive(false);

                // Adjust camera sound
                if (cameraAudioSource != null)
                    cameraAudioSource.volume = 1f;

                isMovingUp = false;
                isMovingDown = true;
            }
            else if (isMovingDown)
            {
                // Player is already moving down, teleport the player upward from the current position
                Vector3 targetPosition = transform.position + Vector3.up * upwardMovementDistance;
                TeleportPlayer(targetPosition);

                // Disable light environment and enable dark environment
                if (lightEnvironment != null)
                    lightEnvironment.SetActive(false);
                if (darkEnvironment != null)
                    darkEnvironment.SetActive(true);

                // Adjust camera sound
                if (cameraAudioSource != null)
                    cameraAudioSource.volume = 0.5f;

                isMovingDown = false;
                isMovingUp = true;
            }
        }
    }

    private void TeleportPlayer(Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }
}
