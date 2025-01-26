using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 6;
    public int currentHealth;
    private PlayerController playerController;

    public float invincibilityDuration = 2f; // Duration of the invincibility period in seconds
    public float flashDuration = 0.1f; // Duration of each flash
    private bool isInvincible = false; // Tracks if the player is currently invincible

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    private HeartUIManager heartUIManager;

    public AudioClip hurtSound;

    void Start()
    {
        currentHealth = maxHealth;
        playerController = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        heartUIManager = FindObjectOfType<HeartUIManager>(); // Find the HeartUIManager
        
    }

    public void TakeDamage(int damage)
    {
        if (playerController.isDashing || isInvincible) 
        { 
            return; 
        }
        
        currentHealth -= damage;
        SoundManager.Instance.PlaySFX(hurtSound);
        Debug.Log("Player took damage! Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Start the invincibility period
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        // Add player death logic (e.g., restart game, show game over screen)
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true; // Set the player to invincible
        Debug.Log("Player is now invincible!");

        float elapsedTime = 0f;

        while (elapsedTime < invincibilityDuration)
        {
            // Alternate between white and the original color
            spriteRenderer.color = new Color(1f,1f,1f,1f);
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = new Color(1f,1f,1f,0.5f); // Revert to the original color
            yield return new WaitForSeconds(flashDuration);

            elapsedTime += flashDuration * 2; // Each flash cycle (white + original color)
        }

        spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // Reset to the original color
        isInvincible = false; // End invincibility
        Debug.Log("Player is no longer invincible!");
    }

    public void RegenerateHealth(float regenDelay) {
    // Start the health regeneration process
    heartUIManager.StartHealthRegen(regenDelay);
}
}
