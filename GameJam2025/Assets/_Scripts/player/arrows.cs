using UnityEngine;

public class arrow : MonoBehaviour {
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] public int damage = 10;
    private Vector2 moveDirection;

    private void Start() {
        // Calculate mouse position in world space
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the normalized direction to move towards the mouse
        moveDirection = (mousePosition - (Vector2)transform.position).normalized;

        // Assign the direction
        SetDirection(moveDirection);

        // Destroy the arrow after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    private void Update() {
        // Move the arrow in the assigned direction
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    public void SetDirection(Vector2 direction) {
        moveDirection = direction; // Assign the direction
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Kurat")) {
            Debug.Log("Kurat hit!");

            bosshealth bossHealth = collision.GetComponent<bosshealth>();
            //kurat take damage
            bossHealth.takeDamage(damage);
            Debug.Log(bossHealth.currentHealth);

            if(bossHealth.currentHealth < 0)
            {
                //kurat saab surma
                Debug.Log("kurat sai surma");
            }

            Destroy(gameObject);          // Destroy arrow
        }
    }
}
