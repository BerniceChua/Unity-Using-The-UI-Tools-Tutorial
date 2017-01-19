using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FocusPanel : MonoBehaviour, IPointerDownHandler {

    private RectTransform panel;

    // Use this for initialization
    void Awake () {
        panel = GetComponent<RectTransform>();

    }
	
	public void OnPointerDown(PointerEventData data) {
        panel.SetAsLastSibling();
    }
}