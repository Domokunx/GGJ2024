using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    private GameObject currentItem;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item Detected");

        if (!eventData.pointerDrag.CompareTag(tag))
        {
            Debug.Log("Wrong tag");
            Destroy(eventData.pointerDrag.GetComponent<ClosetItem>().dragItem);
            return;
        }

        if (currentItem != null)
        {
            Destroy(currentItem);
        }

        if (eventData.pointerDrag != null)
        {
            currentItem = eventData.pointerDrag.GetComponent<ClosetItem>().dragItem;
            currentItem.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }
}
