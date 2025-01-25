using UnityEngine;

public class BossFollow : MonoBehaviour
{
    public Transform player;    // Reference to the player
    public float moveSpeed = 3f; // Movement speed of the boss
    public float attackRange = 2f;  // Distance within which the boss attacks
    public float attackCooldown = 1.5f; // Time between attacks
    public int attackDamage = 10;   // Damage dealt by the boss

    private Rigidbody2D rb;
    private float lastAttackTime = 0f; // Tracks the time of the last attack

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceToPlayer > attackRange)
        {
            // Chase the player if out of attack range
            Vector2 direction = (player.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Stop moving and attack the player
            TryAttackPlayer();
        }
    }

    void TryAttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            // Perform the attack
            lastAttackTime = Time.time;
            Debug.Log("Boss attacks the player!");

            // Example: Reduce player's health (you'll need a PlayerHealth script for this)
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(attackDamage);
        }
    }


}