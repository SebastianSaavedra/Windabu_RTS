using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotImpresora : MonoBehaviour, IDropHandler
{
    public float slotId;
    private float draggedId;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("drop");

        //Snap into position
        if (eventData.pointerDrag != null)
        {
            // Scan Id
            draggedId = eventData.pointerDrag.GetComponent<DragDropStay>().itemDragId;
            if (draggedId == slotId)
            {
                // Move to slot
                FindObjectOfType<AudioManager>().Play("DragDropPositive");
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.GetComponent<DragDropStay>().locked = true;
                GetComponentInParent<ProductImpresora>().AddCounter();

                gameObject.SetActive(false);
            }
            // Failed Scan
            else
            {
                FindObjectOfType<AudioManager>().Play("DragDropNegative");
            }
        }
    }
}
