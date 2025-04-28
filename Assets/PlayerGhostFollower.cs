using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerGhostFollower : MonoBehaviour
{
    public float delay = 1f;
    public float followSpeed = 5f;
    private int currentIndex = 0;
    private List<PlayerGhostRecorder.PlayerState> recordedStates;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(StartFollowing());
    }

    IEnumerator StartFollowing()
    {
        yield return new WaitForSeconds(delay);

        recordedStates = PlayerGhostRecorder.Instance.GetRecordedStates();

        while (true)
        {
            if (currentIndex < recordedStates.Count)
            {
                var state = recordedStates[currentIndex];

                Vector2 direction = (state.position - transform.position);
                rb.velocity = new Vector2(direction.x * followSpeed, rb.velocity.y);

                // Optional: Springen
                if (state.isJumping && IsGrounded())
                {
                    rb.velocity = new Vector2(rb.velocity.x, 10f); // passe Sprungstärke an
                }

                // Animationen setzen
                animator.SetBool("isRunning", state.isRunning);
                animator.SetBool("isJumping", state.isJumping);

                currentIndex++;
            }

            yield return new WaitForSeconds(PlayerGhostRecorder.Instance.recordInterval);
        }
    }

    bool IsGrounded()
    {
        // Ground check (z. B. mit einem kleinen Raycast nach unten)
        return Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
    }
}
