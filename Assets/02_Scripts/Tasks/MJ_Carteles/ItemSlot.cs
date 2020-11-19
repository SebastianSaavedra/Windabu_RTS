using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public float slotId;
    private float draggedId;

    private int partesCounter;
    public int partesAmount;

    private void Start()
    {
        partesCounter = 0;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("drop");

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

                // Complete Impresora
                partesCounter++;
                if(partesCounter == partesAmount)
                {
                    // Terminar minijuego + Permitir uso de impresora para todos
                }

                //Disable
                gameObject.SetActive(false);
            }
            // Failed Scan
            else
            {
                Debug.Log("Aqui no es bro");
            }
        }
    }
}
