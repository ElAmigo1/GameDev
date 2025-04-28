using UnityEngine;

public class SpotlightCollision : MonoBehaviour
{
    public AudioClip collisionSound; // The sound to play on collision
    private AudioSource audioSource; // AudioSource to play the sound

    private void Start()
    {
        // Get the AudioSource component attached to the Spotlight
        audioSource = GetComponent<AudioSource>();

        // Make sure the AudioSource is not playing anything on start
        if (audioSource != null)
        {
            audioSource.Stop(); // Stop any sound if it's accidentally started
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // Check if it's the Beetle
        {
            Debug.Log("Spotlight collided with Beetle!");
            PlaySound(); // Play the sound when collision happens
        }
    }

    private void PlaySound()
    {
        // Play the collision sound if AudioSource is enabled and sound is assigned
        if (audioSource != null && collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound); // Play the sound once
        }
        else
        {
            Debug.LogWarning("AudioSource or collisionSound is missing.");
        }
    }
}
