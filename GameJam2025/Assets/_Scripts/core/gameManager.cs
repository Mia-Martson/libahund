using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour {
    public static gameManager Instance { get; private set; }

    // hävitab koik teised instanceid araaa!!!!!!!!!
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void TransitionToRoom(string roomName) {
        Debug.Log($"Transitioning to room: {roomName}");
        SceneManager.LoadScene(roomName);
    }
}
