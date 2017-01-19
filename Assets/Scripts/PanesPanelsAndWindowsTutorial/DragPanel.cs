using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragPanel : MonoBehaviour, IPointerDownHandler, IDragHandler {

    private Vector2 pointerOffset;
    private RectTransform canvasRectTransform;
    private RectTransform panelRectTransform;

    void Awake() {
        Canvas canvas = GetComponentInParent<Canvas>();

        if (canvas != null) { // this is one example where "!canvas" doesn't work.
            canvasRectTransform = canvas.transform as RectTransform;
            panelRectTransform = transform.parent as RectTransform;
        }
    }

    public void OnPointerDown(PointerEventData data) {
        panelRectTransform.SetAsLastSibling(); //makes this object rendered last so it's on top.

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            panelRectTransform, 
            data.position, 
            data.pressEventCamera, 
            out pointerOffset
        );
    }

    public void OnDrag(PointerEventData data) {
        if (!panelRectTransform) return;

        Vector2 pointerPosition = ClampToWindow(data);

        Vector2 localPointerPosition;
        if ( RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform, pointerPosition, data.pressEventCamera, out localPointerPosition) ) {
            panelRectTransform.localPosition = localPointerPosition - pointerOffset;
        }
        
    }

    Vector2 ClampToWindow(PointerEventData data) {
        Vector2 rawPointerPosition = data.position;

        Vector3[] canvasCorners = new Vector3[4];
        canvasRectTransform.GetWorldCorners(canvasCorners);

        float clampX = Mathf.Clamp(rawPointerPosition.x, canvasCorners[0].x + 10, canvasCorners[2].x - 10);
        float clampY = Mathf.Clamp(rawPointerPosition.y, canvasCorners[0].y + 10, canvasCorners[2].y - 10);

        Vector2 newPointerPosition = new Vector2(clampX, clampY);
        return newPointerPosition;
    }

}