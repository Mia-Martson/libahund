using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance; // Singleton instance

    [Header("Audio Sources")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private void Awake() {
        // Ensure only one SoundManager exists
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes
            LoadVolumeSettings(); // Load saved volume settings
        } else if (Instance != this) {
            Destroy(gameObject); // Destroy duplicate SoundManager
            return;
        }
    }

    public void SetMusicVolume(float volume) {
        Debug.Log("SetMusicVolume called with: " + volume);
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume); // Save to PlayerPrefs
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume) {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume); // Save to PlayerPrefs
        PlayerPrefs.Save();
    }

    public void PlayMusic(AudioClip clip) {
        if (musicSource.clip != clip) {
            musicSource.clip = clip;
            musicSource.Play();
        } else if (!musicSource.isPlaying) {
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip) {
        sfxSource.PlayOneShot(clip);
    }

    private void LoadVolumeSettings() {
        // Load saved volumes or set defaults
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "Main") {
            musicSource.Stop();
        }
    }
}
