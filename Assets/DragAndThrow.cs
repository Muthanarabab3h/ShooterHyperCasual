using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndThrow : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    private RectTransform rectTransform;
    public InventoryItem inventoryItem;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component is missing!");
        }

        // Dynamically find the InventoryItem component
        inventoryItem = GetComponentInChildren<InventoryItem>();
        if (inventoryItem == null)
        {
            Debug.LogWarning($"No InventoryItem assigned or found on {gameObject.name}!");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
        startPosition = rectTransform.position;

        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        startPosition = rectTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        rectTransform.position = Input.mousePosition; // Move the UI element with the mouse/finger
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        rectTransform.position = startPosition; // Reset the UI element's position

        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Pointer Up");

        if (inventoryItem != null)
        {
            // Convert the screen position to world position
            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 spawnPosition = hit.point;
                spawnPosition.y = 0; // Ensure the Y position is always 0
                inventoryItem.OnThrow(spawnPosition); // Call the OnThrow method
            }
            else
            {
                Debug.LogWarning("Raycast did not hit any surface!");
            }
        }
        else
        {
            Debug.LogWarning($"No InventoryItem assigned on {gameObject.name}!");
        }

        rectTransform.position = startPosition; // Reset the UI element's position
    }
}
