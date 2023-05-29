using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeleportWorld : MonoBehaviour
{
    public GameObject lightEnvironment;         // Reference to the light environment GameObject
    public GameObject darkEnvironment;          // Reference to the dark environment GameObject
    public AudioSource cameraAudioSource;       // Reference to the AudioSource component on the camera
    public AudioClip lightWorldSound;           // Sound clip for the light world
    public AudioClip darkWorldSound;            // Sound clip for the dark world
    public float fadeDuration = 1f;             // Duration of the fade effect in seconds
    public Image fadePanel;                     // Reference to the UI image used for fading

    private bool isLightWorld = true;            // Flag indicating if the player is currently in the light world
    private Color fadeColor = Color.black;       // Color used for fading

    private void Start()
    {
        // Set initial environment states
        if (lightEnvironment != null)
            lightEnvironment.SetActive(true);
        if (darkEnvironment != null)
            darkEnvironment.SetActive(false);

        // Set initial audio clip
        if (cameraAudioSource != null)
            cameraAudioSource.clip = lightWorldSound;

        // Play initial audio clip
        if (cameraAudioSource != null)
            cameraAudioSource.Play();

        // Hide the fade panel
        fadePanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(SwitchWorld());
        }
    }

    private IEnumerator SwitchWorld()
    {
        // Show the fade panel
        fadePanel.gameObject.SetActive(true);

        // Start with the fade panel fully transparent
        fadePanel.color = Color.clear;

        // Fade out
        float fadeTimer = 0f;
        while (fadeTimer < fadeDuration)
        {
            float normalizedTime = fadeTimer / fadeDuration;
            fadePanel.color = Color.Lerp(Color.clear, fadeColor, normalizedTime);
            fadeTimer += Time.deltaTime;
            yield return null;
        }

        // Toggle between light and dark worlds
        if (isLightWorld)
        {
            // Switch to dark world
            if (lightEnvironment != null)
                lightEnvironment.SetActive(false);
            if (darkEnvironment != null)
                darkEnvironment.SetActive(true);

            // Set the audio clip to the dark world sound
            if (cameraAudioSource != null)
                cameraAudioSource.clip = darkWorldSound;
        }
        else
        {
            // Switch to light world
            if (lightEnvironment != null)
                lightEnvironment.SetActive(true);
            if (darkEnvironment != null)
                darkEnvironment.SetActive(false);

            // Set the audio clip to the light world sound
            if (cameraAudioSource != null)
                cameraAudioSource.clip = lightWorldSound;
        }

        // Play the audio clip
        if (cameraAudioSource != null)
            cameraAudioSource.Play();

        // Fade in
        fadeTimer = 0f;
        while (fadeTimer < fadeDuration)
        {
            float normalizedTime = fadeTimer / fadeDuration;
            fadePanel.color = Color.Lerp(fadeColor, Color.clear, normalizedTime);
            fadeTimer += Time.deltaTime;
            yield return null;
        }

        // Hide the fade panel
        fadePanel.gameObject.SetActive(false);

        // Update the world state
        isLightWorld = !isLightWorld;
    }

    public bool IsDarkWorld()
    {
        return !isLightWorld;
    }
}
