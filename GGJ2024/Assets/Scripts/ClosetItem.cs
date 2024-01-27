using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClosetItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public static ClosetItem Instance;

    [SerializeField] private GameObject item;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject characterArea;

    private GameObject dragItem;
    [HideInInspector] public RectTransform itemRT;
    private void Awake()
    {
        Instance = this;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Instantiate item not parented to viewport, enable interaction through raycast
        dragItem = Instantiate(item, gameObject.transform);
        dragItem.transform.SetParent(characterArea.transform);
        dragItem.GetComponent<CanvasGroup>().blocksRaycasts = false;

        itemRT = dragItem.GetComponent<RectTransform>();

        gameObject.GetComponent<Image>().enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        itemRT.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        gameObject.GetComponent<Image>().enabled = true;

    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}
