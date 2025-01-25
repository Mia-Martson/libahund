using UnityEngine;

public class aspectRatioManager : MonoBehaviour {
    [SerializeField] private int minGameWidth = 320; // Minimum game width
    [SerializeField] private Color backgroundColor = Color.black; // Letterbox/bar color
    [SerializeField] private int pixelsPerUnit = 16; // Pixels per unit (matches Pixel Perfect Camera settings)

    private Camera mainCamera;

    private void Start() {
        mainCamera = Camera.main;

        if (mainCamera == null) {
            Debug.LogError("No Camera found! Ensure there is a Camera tagged as 'MainCamera' in the scene.");
            return;
        }

        // Set the background color for letterboxing
        mainCamera.backgroundColor = backgroundColor;

        // Initialize the viewport
        UpdateViewport();
    }

    private void Update() {
        if (mainCamera == null) {
            // Try to reassign the camera if it has been destroyed or is missing
            mainCamera = Camera.main;
            if (mainCamera == null) return; // Exit if no camera is found
        }

        // Check if window dimensions or aspect ratio have changed
        if (Screen.width / (float)Screen.height != 1f) {
            UpdateViewport();
        }
    }

    private void UpdateViewport() {
        if (mainCamera == null) return; // Prevent further execution if no camera is assigned

        float screenAspect = (float)Screen.width / Screen.height;
        float targetAspect = 1f; // Square aspect ratio (1:1)

        // Enforce 1:1 aspect ratio and minimum height
        if (Screen.height < minGameWidth) {
            int targetWidth = Mathf.RoundToInt(Screen.height); // Width matches height for 1:1
            Screen.SetResolution(targetWidth, Screen.height, false);
            return; // Exit to avoid repeated adjustments in one frame
        }

        // Handle camera viewport adjustments for 1:1 aspect ratio
        if (screenAspect > targetAspect) {
            // Window is too wide, add bars on the sides
            float inset = (screenAspect - targetAspect) / screenAspect / 2f;
            mainCamera.rect = new Rect(inset, 0, 1 - 2 * inset, 1);
        } else if (screenAspect < targetAspect) {
            // Window is too tall, add bars on top and bottom
            float inset = (targetAspect - screenAspect) / targetAspect / 2f;
            mainCamera.rect = new Rect(0, inset, 1, 1 - 2 * inset);
        } else {
            // Perfect 1:1 aspect ratio
            mainCamera.rect = new Rect(0, 0, 1, 1);
        }

        // Set the background color for the letterboxing
        mainCamera.backgroundColor = backgroundColor;
    }
}
