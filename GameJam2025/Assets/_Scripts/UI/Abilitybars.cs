using UnityEngine;
using UnityEngine.UI;

public class AbilityBars : MonoBehaviour {
    [Header("Transform Bar Settings")]
    [SerializeField] private Image transformBarFill; // Fill for the Transform bar
    [SerializeField] private Color transformReadyColor = Color.yellow; // Oscillation color when ready

    [Header("Dash Bar Settings")]
    [SerializeField] private Image dashBarFill; // Fill for the Dash bar
    [SerializeField] private Color dashReadyColor = Color.cyan; // Oscillation color when ready

    [Header("Player Reference")]
    [SerializeField] private PlayerController player; // Reference to the player controller

    private Color transformDefaultColor; // Store the default color for the Transform bar
    private Color dashDefaultColor; // Store the default color for the Dash bar

    private void Start() {
        // Save the initial colors of the bars at runtime
        transformDefaultColor = transformBarFill.color;
        dashDefaultColor = dashBarFill.color;
    }

    private void Update() {
        if (player != null) {
            UpdateTransformBar();
            UpdateDashBar();
        }
    }

    private void UpdateTransformBar() {
        if (player == null) return;

        float transformCooldown = player.transformationCooldown;
        float transformTimer = player.transformationTimer;

        // Calculate fill amount
        float fillAmount = Mathf.Clamp01(1f - (transformTimer / transformCooldown)); // Fill as time decreases
        transformBarFill.fillAmount = fillAmount;

        // Oscillate color when ready
        if (transformTimer <= 0) {
            transformBarFill.color = Color.Lerp(transformDefaultColor, transformReadyColor, Mathf.Sin(Time.time * 5f) * 0.5f + 0.5f);
        } else {
            transformBarFill.color = transformDefaultColor; // Reset to default color when not ready
        }
    }

    private void UpdateDashBar() {
        if (player == null) return;

        float dashCooldown = player.dashCooldown;
        float dashTimer = player.dashTimer;

        // Show the Dash bar only in melee mode
        if (player.isMelee) {
            dashBarFill.gameObject.SetActive(true);

            // Calculate fill amount
            float fillAmount = Mathf.Clamp01(1f - (dashTimer / dashCooldown)); // Fill as time decreases
            dashBarFill.fillAmount = fillAmount;

            // Oscillate color when ready
            if (dashTimer <= 0) {
                dashBarFill.color = Color.Lerp(dashDefaultColor, dashReadyColor, Mathf.Sin(Time.time * 5f) * 0.5f + 0.5f);
            } else {
                dashBarFill.color = dashDefaultColor; // Reset to default color when not ready
            }
        } else {
            dashBarFill.gameObject.SetActive(false); // Hide dash bar in ranged mode
        }
    }


    public void UseTransform() {
        // Called when the player transforms
        transformBarFill.fillAmount = 0f;
    }

    public void UseDash() {
        // Called when the player dashes
        dashBarFill.fillAmount = 0f;
    }
}
