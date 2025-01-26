using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour {
    [Header("Audio Clips")]
    [SerializeField] private AudioClip menuMusic; // Music track for the main menu
    [SerializeField] private AudioClip buttonClickSFX; // SFX for button clicks

    private void Start() {
        // Play the main menu music using the SoundManager
        if (SoundManager.Instance != null) {
            SoundManager.Instance.PlayMusic(menuMusic);
        }
    }

    public void PlayButtonClickSound() {
        // Play the button click sound effect
        if (SoundManager.Instance != null) {
            SoundManager.Instance.PlaySFX(buttonClickSFX);
        }
    }

    public void LoadScene(string sceneName) {
        // Play button click sound when loading a scene
        PlayButtonClickSound();

        if (sceneName == "Main" && SoundManager.Instance != null) {
            StartCoroutine(FadeOutMusicAndLoadScene(sceneName));
        } else {
            SceneManager.LoadScene(sceneName);
        }
    }

    private IEnumerator FadeOutMusicAndLoadScene(string sceneName) {
        float fadeDuration = 1f; // Duration of the fade
        AudioSource musicSource = SoundManager.Instance.musicSource;
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0) {
            musicSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume; // Reset volume for future music
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame() {
        // Play button click sound when quitting
        PlayButtonClickSound();

        // Quit the application
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
