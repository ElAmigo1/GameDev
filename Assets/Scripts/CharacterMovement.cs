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

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Move X", moveX);
        animator.SetFloat("Move Y", moveY);

        // Optional: Geschwindigkeit für Transition
        animator.SetFloat("Speed", Mathf.Abs(moveX) + Mathf.Abs(moveY));
    }


    private void FixedUpdate()
    {
        // Bewegung anwenden
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
}
