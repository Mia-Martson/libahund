using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {
    [SerializeField] private GameObject pauseMenuPanel; // Reference to the Pause Menu Panel
    private bool isPaused = false; // Tracks whether the game is paused

    private void Update() {
        // Toggle pause menu when Esc or Pause button is pressed
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
            if (isPaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public void PauseGame() {
        isPaused = true;
        pauseMenuPanel.SetActive(true); // Show the pause menu
        Time.timeScale = 0f; // Freeze the game
        Debug.Log("Game Paused");
    }

    public void ResumeGame() {
        isPaused = false;
        pauseMenuPanel.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f; // Resume the game
        Debug.Log("Game Resumed");
    }

    public void RestartGame() {
        Time.timeScale = 1f; // Ensure the game is running before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    public void ReturnToMainMenu() {
        Time.timeScale = 1f; // Reset timeScale before changing scenes
        SceneManager.LoadScene("Menu"); // Replace with your main menu scene name
    }

    public void QuitGame() {
        Time.timeScale = 1f; // Ensure the game is running before quitting
        Debug.Log("Quitting Game...");
        Application.Quit(); // Quit the application
    }
}
