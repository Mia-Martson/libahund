using UnityEngine;
using UnityEngine.UI;

public class Abilitybars : MonoBehaviour {
    [Header("Transform Bar Settings")]
    [SerializeField] private Image transformBarFill;
    [SerializeField] private float transformCooldown = 5f;
    [SerializeField] private Color transformReadyColor = Color.yellow;
    private float transformTimer;

    [Header("Dash Bar Settings")]
    [SerializeField] private Image dashBarFill;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private Color dashReadyColor = Color.cyan;
    private float dashTimer;

    private bool isMelee = false; // Indicates whether the player is in melee mode
    private bool transformReady = false;
    private bool dashReady = false;

    private void Update() {
        UpdateTransformBar();
        UpdateDashBar();
    }

    private void UpdateTransformBar() {
        if (transformTimer < transformCooldown) {
            transformTimer += Time.deltaTime;
            float fillAmount = Mathf.Clamp01(transformTimer / transformCooldown);
            transformBarFill.fillAmount = fillAmount;
            transformBarFill.color = Color.Lerp(Color.green, transformReadyColor, Mathf.Sin(Time.time * 5f) * 0.5f + 0.5f);
        } else {
            if (!transformReady) {
                transformReady = true;
                transformBarFill.color = transformReadyColor; // Make the bar oscillate when ready
            }
        }
    }

    private void UpdateDashBar() {
        if (isMelee) {
            dashBarFill.gameObject.SetActive(true); // Show dash bar in melee mode

            if (dashTimer < dashCooldown) {
                dashTimer += Time.deltaTime;
                float fillAmount = Mathf.Clamp01(dashTimer / dashCooldown);
                dashBarFill.fillAmount = fillAmount;
                dashBarFill.color = Color.Lerp(Color.blue, dashReadyColor, Mathf.Sin(Time.time * 5f) * 0.5f + 0.5f);
            } else {
                if (!dashReady) {
                    dashReady = true;
                    dashBarFill.color = dashReadyColor; // Make the bar oscillate when ready
                }
            }
        } else {
            dashBarFill.gameObject.SetActive(false); // Hide dash bar when not in melee mode
        }
    }

    public void UseTransform() {
        if (transformReady) {
            transformTimer = 0f;
            transformReady = false;
        }
    }

    public void UseDash() {
        if (dashReady) {
            dashTimer = 0f;
            dashReady = false;
        }
    }

    public void SetMeleeMode(bool meleeMode) {
        isMelee = meleeMode;
    }
}
