using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    private RectTransform currentItem;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Item Detected");

        if (currentItem != null)
        {
            Destroy(currentItem.gameObject);
        }

        if (eventData.pointerDrag != null)
        {
            currentItem = eventData.pointerDrag.GetComponent<ClosetItem>().itemRT;
            currentItem.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
