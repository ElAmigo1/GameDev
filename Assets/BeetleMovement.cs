using UnityEngine;

public class BeetleMovement : MonoBehaviour
{
    public float moveSpeed = 2f;     // Geschwindigkeit
    public float topLimit = 4f;       // Obere Grenze (Weltposition Y)
    public float bottomLimit = 0f;    // Untere Grenze (Weltposition Y)

    private Vector2 moveDirection = Vector2.up;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();

        // Check, ob wir das obere Limit erreichen
        if (transform.position.y >= topLimit)
        {
            moveDirection = Vector2.down;
            FlipVertically();
        }
        // Check, ob wir das untere Limit erreichen
        else if (transform.position.y <= bottomLimit)
        {
            moveDirection = Vector2.up;
            FlipVertically();
        }
    }

    void Move()
    {
        rb.velocity = moveDirection.normalized * moveSpeed;
    }

    void FlipVertically()
    {
        Vector3 localScale = transform.localScale;
        localScale.y *= -1;   // Spiegeln in Y
        transform.localScale = localScale;
    }
}
