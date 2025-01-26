using UnityEngine;

public class SlashEffect : MonoBehaviour {
    public float lifetime = 0.5f;

    private void Start() {
        Destroy(gameObject, lifetime);
    }
}
