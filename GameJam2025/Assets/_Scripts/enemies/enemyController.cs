using UnityEngine;

public class EnemyController : MonoBehaviour {
    [SerializeField] private float moveSpeed = 2f;
    private Transform player;

    private void Start() {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update() {
        if (player != null) {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}
