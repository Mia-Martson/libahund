using UnityEngine;
using UnityEngine.EventSystems;

public class EventTriggerAssigner : MonoBehaviour {
    [SerializeField] private AudioClip hoverSound; // Sound to play on hover

    private void Start() {
        // Assign PointerEnter dynamically
        EventTrigger eventTrigger = GetComponent<EventTrigger>();

        if (eventTrigger != null) {
            // Clear existing triggers to prevent duplicates
            eventTrigger.triggers.Clear();

            // Add PointerEnter event
            EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
            pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
            pointerEnterEntry.callback.AddListener((data) => { OnPointerEnter(); });
            eventTrigger.triggers.Add(pointerEnterEntry);
        }
    }

    private void OnPointerEnter() {
        if (SoundManager.Instance != null && hoverSound != null) {
            SoundManager.Instance.PlaySFX(hoverSound);
        }
    }
}
