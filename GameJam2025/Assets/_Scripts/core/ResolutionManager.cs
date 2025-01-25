using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionManager : MonoBehaviour {
    public static ResolutionManager Instance; // Singleton instance

    [SerializeField] private Color backgroundColor = Color.black; // Color for empty screen space
    [SerializeField] private TMP_Dropdown resolutionDropdown; // dropdown
    private void Awake() {
        // Ensure only one ResolutionManager exists
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        // Add listener to the dropdown for resolution changes
        if (resolutionDropdown != null) {
            resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
            // Set the dropdown value to the saved resolution or default to 0
            int savedResolution = PlayerPrefs.GetInt("ResolutionOption", 0);
            resolutionDropdown.value = savedResolution;
            resolutionDropdown.RefreshShownValue(); // Update the dropdown visually
            ChangeResolution(savedResolution); // Apply the saved resolution
        } else {
            Debug.LogError("Resolution Dropdown is not assigned in the Inspector!");
        }
    }

    public void SetResolution(int width, int height, bool fullscreen) {
        Debug.Log($"ATTEPT NOW!!!!!!!!Resolution option selected: {fullscreen}");
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

    public void ChangeResolution(int option) {
        Debug.Log($"Resolution option selected: {option}");
        switch (option) {
            case 0: // 320x320
                SetResolution(320, 320, false);
                break;
            case 1: // 640x640
                SetResolution(640, 640, false);
                break;
            case 2: // Fullscreen
                SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                break;
        }

        // Save the resolution setting
        PlayerPrefs.SetInt("ResolutionOption", option);
        PlayerPrefs.Save();
    }
}
