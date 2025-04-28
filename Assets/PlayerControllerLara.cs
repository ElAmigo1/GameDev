using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControllerLara : MonoBehaviour
{
    // Movement variables
    public InputAction moveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    float speed = 3.0f;

    // Health system
    public int maxHealth = 5;
    int currentHealth;
    public int health { get { return currentHealth; } }

    // Invincibility
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;

    // Animation
    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);

    // UI
    public Image healthBar;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        // Enable input action
        if (moveAction != null)
            moveAction.Enable();
    }

    void Update()
    {
        if (moveAction != null)
            move = moveAction.ReadValue<Vector2>();

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        animator.SetFloat("Move X", moveDirection.x);
        animator.SetFloat("Move Y", moveDirection.y);
        //animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
                isInvincible = false;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position + move * speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }
}