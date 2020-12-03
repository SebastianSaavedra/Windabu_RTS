using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ProductMerch : MonoBehaviour, IDropHandler
{
    public float slotId;
    private float draggedId;

    public GameObject readySprite;
    public GameObject chapita;
    public GameObject resetPos;
    public TextMeshProUGUI counterText;
    int counterNum = 5;

    bool chapitaIn;
    [SerializeField]int chapitaCounter;
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
                readySprite.SetActive(true);
                chapita.GetComponent<DragDropStay>().locked = true;
                chapitaIn = true;
                chapita.SetActive(false);
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

            if (chapitaCounter == maxChapitas)
            {
                GetComponentInParent<FinishMinigameManager>().AddSlotCounter();
                Debug.Log("Listopo");
                readySprite.SetActive(false);
            }
            else
            {
                chapita.SetActive(true);
                readySprite.SetActive(false);
                chapita.transform.position = resetPos.transform.position;
                chapita.GetComponent<DragDropStay>().locked = false;
                chapita.GetComponent<CanvasGroup>().blocksRaycasts = true;
                Debug.Log("Faltan");
            }

            // UI Counter
            if (counterText != null)
            {
                counterNum--;
                counterText.text = counterNum.ToString();
            }
        }
        else { Debug.Log("Falta Chapita"); }
    }
}
