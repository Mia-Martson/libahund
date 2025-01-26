using UnityEngine;

public class TitleBobbing : MonoBehaviour
{
    [SerializeField] private float bobSpeed = 1f; // Speed of the bobbing motion
    [SerializeField] private float bobHeight = 30f; // Height of the bobbing motion in pixels

    private RectTransform rectTransform; // Reference to the RectTransform
    private Vector2 startAnchoredPosition; // Initial anchored position of the title

    private void Start()
    {
        // Get the RectTransform component
        rectTransform = GetComponent<RectTransform>();

        // Record the starting anchored position
        startAnchoredPosition = rectTransform.anchoredPosition;
    }

    private void Update()
    {
        // Calculate the new Y position for anchoredPosition
        float newY = startAnchoredPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;

        // Apply the new anchored position
        rectTransform.anchoredPosition = new Vector2(startAnchoredPosition.x, newY);
    }
}
