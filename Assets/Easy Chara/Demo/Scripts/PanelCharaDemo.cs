using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Beispiel-Inputs zum Testen:

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.Play("Jump P1 (Start)");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.Play("Walk");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.Play("Run");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.Play("Crouch");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.Play("Attack Melee");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.Play("Damage");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.Play("Death");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.Play("Roll");
        }
    }
}
