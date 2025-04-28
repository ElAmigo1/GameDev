using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private float moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Input lesen
        moveInput = Input.GetAxisRaw("Horizontal");

        // Animation setzen
        animator.SetBool("isRunning", moveInput != 0);

        // Flip Sprite bei Richtung
        if (moveInput != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
    }

    private void FixedUpdate()
    {
        // Bewegung anwenden
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
}
