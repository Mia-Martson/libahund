using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public float bulletTimeToLive = 5f;
    [SerializeField] public int damage;
    private Vector2 moveDirection;

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Optional: Destroy bullet after a certain time to avoid clutter
        Destroy(gameObject,bulletTimeToLive);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is tagged as "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("inimenesaipihta");
            // Destroy the bullet
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

    }
}