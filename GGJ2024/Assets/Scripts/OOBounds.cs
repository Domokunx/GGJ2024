using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OOBounds : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OOBounds");
        if (eventData.pointerDrag != null)
        {
            Destroy(eventData.pointerDrag.GetComponent<ClosetItem>().dragItem);
        }
    }

}
