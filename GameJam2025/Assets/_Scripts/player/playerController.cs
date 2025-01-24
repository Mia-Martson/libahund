using UnityEngine;

public class playerController : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private Vector2 moveInput;
    private Vector2 mousePosition;

    void Update() {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement() {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(moveInput * moveSpeed * Time.deltaTime, Space.World);

        // Optional: Rotate player towards mouse
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        transform.up = direction;
    }

    private void HandleShooting() {
        if (Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }

    private void Shoot() {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
