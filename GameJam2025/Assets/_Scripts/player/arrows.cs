using UnityEngine;

public class arrow : MonoBehaviour {
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 2f;

    private void Start() {
        Destroy(gameObject, lifetime);
    }

    private void Update() {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            Debug.Log("Enemy hit!");
            Destroy(collision.gameObject); // Destroy enemy
            Destroy(gameObject);          // Destroy arrow
        }
    }
}
