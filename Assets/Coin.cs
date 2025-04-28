using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"OnTriggerEnter2D with {other.name}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with coin!");

            if (GameManager.instance != null)
            {
                Debug.Log("GameManager instance found. Collecting coin...");
                GameManager.instance.CollectCoin();
            }
            else
            {
                Debug.LogError("GameManager instance is NULL! Make sure it exists in the scene.");
            }

            Destroy(gameObject); // remove the coin
        }
        else
        {
            Debug.Log($"Collision with {other.name}, but it does not have the 'Player' tag.");
        }
    }
}
