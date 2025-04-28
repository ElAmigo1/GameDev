using UnityEngine;

public class ChunkActivator : MonoBehaviour
{
    public Transform player; // Dein Wizard
    public float activateDistance = 15f;

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < activateDistance)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
