using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControllerLara : MonoBehaviour
{
    public AudioClip deathSound; // Sterbesound
    private AudioSource audioSource; // AudioPlayer

    // Movement
    Rigidbody2D rigidbody2d;
    Vector2 move;

    public float normalSpeed = 3.0f;
    public float runSpeed = 5.5f;
    private float currentSpeed;

    private bool isSprinting = false;

    // Sprint Timer & Cooldown
    public float sprintDuration = 5f;
    public float sprintCooldown = 10f;

    private float sprintTimer = 0f;
    private float cooldownTimer = 0f;
    private bool sprintOnCooldown = false;

    public bool isBeingFollowed = false;

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
    public float jump;
    void Start()
    {
        SaveCurrentScene();

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        currentSpeed = normalSpeed;

        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.AddForce(new Vector2(rigidbody2d.velocity.x, jump));
        }
        // Sprint starten per einmaligem Shift, wenn nicht auf Cooldown
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSprinting && !sprintOnCooldown)
        {
            isSprinting = true;
            currentSpeed = runSpeed;
            sprintTimer = sprintDuration;
            Debug.Log("Sprint started!");
        }

        // Sprint Timer runterzählen
        if (isSprinting)
        {
            sprintTimer -= Time.deltaTime;
            if (sprintTimer <= 0)
            {
                // Sprint beenden & Cooldown starten
                isSprinting = false;
                currentSpeed = normalSpeed;
                sprintOnCooldown = true;
                cooldownTimer = sprintCooldown;
                Debug.Log("Sprint ended! Cooldown started.");
            }
        }
        else if (sprintOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                sprintOnCooldown = false;
                Debug.Log("Sprint cooldown finished! You can sprint again.");
            }
        }

        // Bewegung lesen
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        move = new Vector2(moveX, moveY).normalized;

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        animator.SetFloat("Move X", moveDirection.x);
        animator.SetFloat("Move Y", moveDirection.y);

        // Optional: Animationsgeschwindigkeit je nach Verfolgung
        animator.speed = isBeingFollowed ? 1.5f : 1.0f;

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
                isInvincible = false;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position + move * currentSpeed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth == 0)
        {
            GameOver();
        }

        isInvincible = true;
        damageCooldown = timeInvincible;
    }

    void GameOver()
    {
        StartCoroutine(HandleGameOver());
    }

    IEnumerator HandleGameOver()
    {
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
            yield return new WaitForSeconds(deathSound.length + 1);
        }
        else
        {
            Debug.LogWarning("Death sound or audio source missing!");
            yield return new WaitForSeconds(2f);
        }

        SceneManager.LoadScene("GameOver");
    }

    void SaveCurrentScene()
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
    }
}
