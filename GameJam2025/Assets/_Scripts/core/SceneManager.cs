using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management
using UnityEngine.UI; // Required for working with UI elements
using System.Collections; // Required for coroutines

public class SceneChanger : MonoBehaviour {
    [SerializeField] private Image fadeImage; // Reference to the black fade image
    [SerializeField] private float fadeDuration = 0.2f; // Duration of the fade effect

    private void Start() {
        // Start with a fade-in effect when the scene loads
        if (fadeImage != null) {
            StartCoroutine(FadeIn());
        }
    }

    public void ChangeSceneByName(string sceneName) {
        // Trigger the fade-out effect and load the scene by name
        if (fadeImage != null) {
            StartCoroutine(FadeOutAndLoadScene(sceneName));
        } else {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void ChangeSceneByIndex(int sceneIndex) {
        // Trigger the fade-out effect and load the scene by index
        if (fadeImage != null) {
            StartCoroutine(FadeOutAndLoadScene(SceneManager.GetSceneByBuildIndex(sceneIndex).name));
        } else {
            SceneManager.LoadScene(sceneIndex);
        }
    }

    public void QuitGame() {
        // Quit the application with a fade-out effect
        if (fadeImage != null) {
            StartCoroutine(FadeOutAndQuit());
        } else {
            Debug.Log("Game Quit!");
            Application.Quit();
        }
    }

    private IEnumerator FadeIn() {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            color.a = 1f - (elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        color.a = 0f;
        fadeImage.color = color; // Ensure fully transparent at the end
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName) {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            color.a = elapsedTime / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }
        color.a = 1f;
        fadeImage.color = color; // Ensure fully opaque at the end

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeOutAndQuit() {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            color.a = elapsedTime / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }
        color.a = 1f;
        fadeImage.color = color; // Ensure fully opaque at the end

        Debug.Log("Game Quit!");
        Application.Quit();
    }

    public void RestartLevel() {
        if (fadeImage != null) {
            StartCoroutine(FadeOutAndRestart());
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private IEnumerator FadeOutAndRestart() {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        // Fade to black
        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

