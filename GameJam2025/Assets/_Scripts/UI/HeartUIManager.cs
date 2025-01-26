using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeartUIManager : MonoBehaviour {
    [Header("Heart Sprites")]
    [SerializeField] private Sprite fullHeart; // Full heart sprite
    [SerializeField] private Sprite halfHeart; // Half heart sprite
    [SerializeField] private Sprite emptyHeart; // Empty heart sprite

    [Header("Heart Images")]
    [SerializeField] private Image[] hearts; // Array to hold heart images

    private PlayerHealth playerHealth;

    private void Start() {
        // Reference the PlayerHealth script on the player object
        playerHealth = FindObjectOfType<PlayerHealth>();
        UpdateHeartsUI(); // Initialize the UI
    }

    private void Update() {
        UpdateHeartsUI(); // Continuously update the UI (optional optimization: call only when health changes)
    }

    public void UpdateHeartsUI() {
        int currentHealth = playerHealth.currentHealth; // Get the player's current health
        int maxHealth = playerHealth.maxHealth;

        for (int i = 0; i < hearts.Length; i++) {
            int heartIndex = i * 2; // Each heart represents 2 health points

            if (currentHealth >= heartIndex + 2) {
                // Full heart
                hearts[i].sprite = fullHeart;
            } else if (currentHealth == heartIndex + 1) {
                // Half heart
                hearts[i].sprite = halfHeart;
            } else {
                // Empty heart
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void StartHealthRegen(float regenDelay) {
        // Start the health regeneration coroutine
        StartCoroutine(HealthRegenCoroutine(regenDelay));
    }

    private IEnumerator HealthRegenCoroutine(float regenDelay) {
        while (playerHealth.currentHealth < playerHealth.maxHealth) {
            yield return new WaitForSeconds(regenDelay); // Wait before regenerating
            playerHealth.currentHealth = Mathf.Min(playerHealth.currentHealth + 1, playerHealth.maxHealth);
            UpdateHeartsUI(); // Update the UI after regen
        }
    }
}
