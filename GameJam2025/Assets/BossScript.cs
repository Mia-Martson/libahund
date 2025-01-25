using UnityEngine;

public class BossFollow : MonoBehaviour
{
    public Transform player;    // Reference to the player
    public float moveSpeed = 3f; // Movement speed of the boss

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
            // Calculate direction to the player
            Vector2 direction = (player.position - transform.position).normalized;

            // Move the boss toward the player
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
}