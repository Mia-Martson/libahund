using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class WinLoseManager : MonoBehaviour {
    [Header("UI Panels")]
    [SerializeField] private GameObject winPanel; // Reference to the Win Panel
    [SerializeField] private GameObject losePanel; // Reference to the Lose Panel

    private Animator winAnimator; // Animator for the Win Panel
    private Animator loseAnimator; // Animator for the Lose Panel

    private void Start() {
        // Get Animators from the panels
        if (winPanel != null) winAnimator = winPanel.GetComponent<Animator>();
        if (losePanel != null) loseAnimator = losePanel.GetComponent<Animator>();

        // Ensure panels are initially inactive
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    public void ShowWinScreen() {

        if (winPanel != null) {
            StartCoroutine(WaitSomeSeconds(winPanel));
            /*winPanel.SetActive(true); // Activate the panel
            winAnimator.Play("WinPanelZoomIn"); // Play the zoom-in animation*/
        }
    }

    public void ShowLoseScreen() {

        if (losePanel != null) {
            StartCoroutine(WaitSomeSeconds(losePanel));
            /*losePanel.SetActive(true); // Activate the panel
            loseAnimator.Play("LosePanelZoomIn"); // Play the zoom-in animation*/
        }
    }

    IEnumerator WaitSomeSeconds(GameObject panel)
    {
        yield return new WaitForSeconds(3f);
        panel.SetActive(true);
        if (panel == losePanel) loseAnimator.Play("LosePanelZoomIn");
        else winAnimator.Play("WinPanelZoomIn");
    }
}
