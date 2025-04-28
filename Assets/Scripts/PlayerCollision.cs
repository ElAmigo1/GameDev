using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private Animator animator;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Barrel"))
        {
            playerHealth.TakeDamage(1);
            animator.SetTrigger("Hurt");

            Debug.Log("Barrel getroffen! Leben verringert.");
            Debug.Log("Aktuelles Leben: " + playerHealth.currentHealth);
        }
    }
}
