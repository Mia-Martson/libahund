using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 moveDirection;

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Optional: Destroy bullet after a certain time to avoid clutter
        Destroy(gameObject, 2f);
    }
}