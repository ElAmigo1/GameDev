using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float followDistance = 3f;
    public float stopFollowDistance = 5f;
    public float followDuration = 1f; // wie lange verfolgen

    private bool isFollowing = false;
    private float followTimer = 0f;
    private bool alreadyFollowedOnce = false;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Reset erlauben, wenn Spieler weit genug weg war
        if (distance > stopFollowDistance)
        {
            alreadyFollowedOnce = false;
            isFollowing = false;
            followTimer = 0f;
        }

        // Nur starten, wenn noch nicht verfolgt wurde
        if (!alreadyFollowedOnce && distance <= followDistance)
        {
            isFollowing = true;
            followTimer = followDuration;
            alreadyFollowedOnce = true;
        }

        // Timer läuft
        if (isFollowing)
        {
            followTimer -= Time.deltaTime;
            if (followTimer <= 0f)
            {
                isFollowing = false;
            }
        }

        // Bewegung
        if (isFollowing)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }

        // Spieler informieren
        PlayerControllerLara playerController = player.GetComponent<PlayerControllerLara>();
        if (playerController != null)
        {
            playerController.isBeingFollowed = isFollowing;
        }
    }
}
