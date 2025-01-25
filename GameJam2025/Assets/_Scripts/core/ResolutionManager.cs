using UnityEngine;

public class ResolutionManager : MonoBehaviour {
    public static ResolutionManager Instance; // Singleton instance

    [SerializeField] private Color backgroundColor = Color.black; // Color for empty screen space

    private void Awake() {
        // Ensure only one ResolutionManager exists
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void SetResolution(int width, int height, bool fullscreen) {
        Screen.SetResolution(width, height, fullscreen);

        // Adjust the camera viewport for square aspect ratio
        Camera camera = Camera.main;
        if (camera != null) {
            float screenAspect = (float)Screen.width / Screen.height;
            float targetAspect = 1f; // Square aspect ratio (1:1)

            if (screenAspect > targetAspect) {
                float inset = (screenAspect - targetAspect) / 2f / screenAspect;
                camera.rect = new Rect(inset, 0, 1 - 2 * inset, 1);
            } else {
                float inset = (targetAspect - screenAspect) / 2f / targetAspect;
                camera.rect = new Rect(0, inset, 1, 1 - 2 * inset);
            }

            // Set the background color for the letterbox/bars
            camera.backgroundColor = backgroundColor;
        }
    }
}
