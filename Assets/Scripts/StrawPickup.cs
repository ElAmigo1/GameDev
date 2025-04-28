using UnityEngine;

public class StrawPickup : MonoBehaviour
{

    public int healAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.AddHealth(healAmount);
            Destroy(gameObject); // Erdbeere verschwindet nach dem Einsammeln
        }
    }
}


