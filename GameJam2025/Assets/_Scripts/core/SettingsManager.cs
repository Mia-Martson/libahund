using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start() {
        // Temporarily remove listeners to avoid triggering onValueChanged during initialization
        musicSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();

        // Initialize sliders with saved volume values
        if (SoundManager.Instance != null) {
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
            float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

            Debug.Log("Initializing sliders:");
            Debug.Log("Music Volume: " + musicVolume);
            Debug.Log("SFX Volume: " + sfxVolume);

            // Set the slider values
            musicSlider.value = musicVolume;
            sfxSlider.value = sfxVolume;
        }

        // Reassign listeners after initialization
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void SetMusicVolume(float volume) {
        if (SoundManager.Instance != null) {
            Debug.Log("SetMusicVolume called with: " + volume);
            SoundManager.Instance.SetMusicVolume(volume);
            Debug.Log("Music volume changed to: " + volume);
        }
    }

    private void SetSFXVolume(float volume) {
        if (SoundManager.Instance != null) {
            Debug.Log("SetSFXVolume called with: " + volume);
            SoundManager.Instance.SetSFXVolume(volume);
        }
    }

    private void OnDestroy() {
        // Remove listeners to prevent memory leaks
        musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
    }
}
