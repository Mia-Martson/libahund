using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance; // Singleton instance

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private void Awake() {
        // Ensure only one SoundManager exists
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes
        } else {
            Destroy(gameObject);
        }
    }
    // settingutes
    public void SetMusicVolume(float volume) {
        musicSource.volume = volume;
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    // settingutes
    public void SetSFXVolume(float volume) {
        sfxSource.volume = volume;
    }
    
    // heliefektid mujal
    public void PlaySFX(AudioClip clip) {
        sfxSource.PlayOneShot(clip);
    }
}
