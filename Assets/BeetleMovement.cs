using UnityEngine;

public class BeetleMovement : MonoBehaviour
{
    public float moveSpeed = 2f;

    public float topLimit = 4f;     // Optional, kannst du erstmal ignorieren
    public float bottomLimit = 0f;  // Optional
    public float leftLimit = -3f;   // Optional

    public float followDistance = 3f; // Abstand zum Spieler zum Folgen
    private Transform player;

    public enum Direction
    {
        Up,
        Left,
        Down
    }

    private Direction moveDirection = Direction.Up;
    private Rigidbody2D rb;

    private bool isFollowing = false;

    private float directionChangeTimer = 0f;
    public float changeInterval = 3f;  // Zeit in Sekunden bis Richtungswechsel

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        isFollowing = distance <= followDistance;

        if (!isFollowing)
        {
            directionChangeTimer += Time.deltaTime;
            if (directionChangeTimer >= changeInterval)
            {
                ChangeDirection();
                directionChangeTimer = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        if (isFollowing)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            rb.velocity = directionToPlayer * moveSpeed;

            // Optional: Käfer drehen je nach Richtung
            if (directionToPlayer.x != 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Sign(directionToPlayer.x) * Mathf.Abs(scale.x);
                transform.localScale = scale;
            }
        }
        else
        {
            MoveInPattern();
        }
    }

    void MoveInPattern()
    {
        Vector2 velocity = Vector2.zero;

        switch (moveDirection)
        {
            case Direction.Up:
                velocity = Vector2.up;
                break;
            case Direction.Left:
                velocity = Vector2.left;
                break;
            case Direction.Down:
                velocity = Vector2.down;
                break;
        }

        rb.velocity = velocity * moveSpeed;
    }

    void ChangeDirection()
    {
        // Reihenfolge: Up -> Left -> Down -> Up -> ...
        switch (moveDirection)
        {
            case Direction.Up:
                moveDirection = Direction.Left;
                break;
            case Direction.Left:
                moveDirection = Direction.Down;
                break;
            case Direction.Down:
                moveDirection = Direction.Up;
                break;
        }
    }
}
