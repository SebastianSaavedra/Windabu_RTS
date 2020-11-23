using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProductChapita : MonoBehaviour, IDropHandler
{
    public float slotId;
    private float draggedId;

    public GameObject chapita;
    public GameObject resetPos;

    bool chapitaIn;
    int chapitaCounter;
    public int maxChapitas;

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
                chapita = eventData.pointerDrag;

                // Move to slot
                chapita.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                chapita.GetComponent<DragDropStay>().locked = true;
                chapitaIn = true;
            }
            // Failed Scan
            else
            {
                Debug.Log("Aqui no es bro");
            }
        }
    }

    public void AddChapita()
    {
        if (chapitaIn)
        {
            chapitaIn = false;
            chapitaCounter++;

            if(chapitaCounter == maxChapitas)
            {
                GetComponentInParent<FinishMinigameManager>().AddSlotCounter();
                Debug.Log("Listopo");
            }
            else
            {
                chapita.transform.position = resetPos.transform.position;
                chapita.GetComponent<DragDropStay>().locked = false;
                chapita.GetComponent<CanvasGroup>().blocksRaycasts = true;
                Debug.Log("Faltan");
            }
        }
        else { Debug.Log("Falta Chapita"); }
    }
}
