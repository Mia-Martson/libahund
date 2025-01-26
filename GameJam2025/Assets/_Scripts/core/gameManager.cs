using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour {
    public static gameManager Instance { get; private set; }
    public bool IsGameOver { get; private set; } = false;
    public AudioClip sceneMusic;

    // hävitab koik teised instanceid araaa!!!!!!!!!
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }

        
    }

    void Start()
    {
        if (SoundManager.Instance.musicSource.clip != sceneMusic || !SoundManager.Instance.musicSource.isPlaying)
        {
            SoundManager.Instance.PlayMusic(sceneMusic);
        }
    }

    public void TransitionToRoom(string roomName) {
        Debug.Log($"Transitioning to room: {roomName}");
        SceneManager.LoadScene(roomName);
    }

    public void SetGameOver() {
        IsGameOver = true;
    }
}
