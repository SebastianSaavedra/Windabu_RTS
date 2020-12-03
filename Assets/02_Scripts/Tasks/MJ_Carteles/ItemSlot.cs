using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public float slotId;
    private float draggedId;

    public void OnDrop(PointerEventData eventData)
    {
        //Snap into position
        if (eventData.pointerDrag != null)
        {
            // Scan Id
            draggedId = eventData.pointerDrag.GetComponent<DragDropStay>().itemDragId;
            if(draggedId == slotId)
            {
                // Move to slot
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.GetComponent<DragDropStay>().locked = true;

                // Complete MJ
                FindObjectOfType<AudioManager>().Play("DragDropPositive");
                GetComponentInParent<FinishMinigameManager>().AddSlotCounter();

                //Disable
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
