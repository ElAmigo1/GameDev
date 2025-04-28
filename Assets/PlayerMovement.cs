using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float baseMoveSpeed = 5f;
    public float baseJumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private float scaleFactor = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Hole die aktuelle Skalierung (X reicht für 2D meistens)
        scaleFactor = transform.localScale.x;

        // Optional: debug
        // Debug.Log("Scale Factor: " + scaleFactor);
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * baseMoveSpeed * scaleFactor, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, baseJumpForce * scaleFactor);
        }

        // Ground Check mit Raycast
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f * scaleFactor, LayerMask.GetMask("Ground"));
    }

    public bool IsJumping()
    {
        return !isGrounded;
    }

    public bool IsRunning()
    {
        return Mathf.Abs(rb.velocity.x) > 0.1f;
    }
}
