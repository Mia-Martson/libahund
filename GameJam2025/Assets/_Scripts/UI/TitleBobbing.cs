using UnityEngine;

public class TitleBobbing : MonoBehaviour
{
    [SerializeField] private float bobSpeed = 1f; // Speed of the bobbing motion
    [SerializeField] private float bobHeight = 0.5f; // Height of the bobbing motion

    private Vector3 startPosition; // Initial position of the title

    private void Start()
    {
        // Record the starting position
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the new Y position
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;

        // Apply the new position
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
