using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class SceneChanger : MonoBehaviour {
    public void ChangeSceneByName(string sceneName) {
        // Load the scene by name
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeSceneByIndex(int sceneIndex) {
        // Load the scene by index
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame() {
        // Quit the application
        Debug.Log("Game Quit!");
        Application.Quit();
    }
}
