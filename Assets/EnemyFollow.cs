using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;

    void Start()
    {
        // Sucht nach dem Spieler anhand des Tags
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // Richtung zum Spieler berechnen
            Vector3 direction = (player.position - transform.position).normalized;

            // Bewegung in Richtung des Spielers
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
