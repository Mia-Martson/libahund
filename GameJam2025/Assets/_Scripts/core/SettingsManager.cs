using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start() {
        Debug.Log($"Music Slider Assigned: {musicSlider != null}");
        Debug.Log($"SFX Slider Assigned: {sfxSlider != null}");

        // Temporarily remove listeners to avoid triggering onValueChanged during initialization
        if (musicSlider != null) musicSlider.onValueChanged.RemoveAllListeners();
        if (sfxSlider != null) sfxSlider.onValueChanged.RemoveAllListeners();

        // Initialize sliders with saved volume values
        if (SoundManager.Instance != null) {
            float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
            float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

            Debug.Log("Initializing sliders:");
            Debug.Log("Music Volume: " + musicVolume);
            Debug.Log("SFX Volume: " + sfxVolume);

            if (musicSlider != null) musicSlider.value = musicVolume;
            if (sfxSlider != null) sfxSlider.value = sfxVolume;
        }

        // Reassign listeners after initialization
        if (musicSlider != null) musicSlider.onValueChanged.AddListener(SetMusicVolume);
        if (sfxSlider != null) sfxSlider.onValueChanged.AddListener(SetSFXVolume);
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
        // Remove listeners only if the sliders are not null
        if (musicSlider != null) {
            musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        }
        if (sfxSlider != null) {
            sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
        }
    }
}
