using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsController : MonoBehaviour {
    [Header("Sliders")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    private void Start() {
        // Initialize sliders with saved values or defaults
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // Add listeners to handle slider value changes
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float volume) {
        SoundManager.Instance.SetMusicVolume(volume); // Update SoundManager volume
        PlayerPrefs.SetFloat("MusicVolume", volume);  // Save value
        Debug.Log($"Music volume changed to: {volume}");
    }

    public void SetSFXVolume(float volume) {
        SoundManager.Instance.SetSFXVolume(volume);   // Update SoundManager volume
        PlayerPrefs.SetFloat("SFXVolume", volume);   // Save value
        Debug.Log($"SFX volume changed to: {volume}");
    }
}
