using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ProductChapita : MonoBehaviour, IDropHandler
{
    public float slotId;
    private float draggedId;

    public GameObject chapita;
    public GameObject resetPos;
    public TextMeshProUGUI counterText;
    int counterNum = 5;

    public int chapitaIn;
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
                FindObjectOfType<AudioManager>().Play("DragDropPositive");
                chapita.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                chapita.GetComponent<DragDropStay>().locked = true;
                GetComponentInParent<ChapaCountet>().AddChapa();
            }
            // Failed Scan
            else
            {
                FindObjectOfType<AudioManager>().Play("DragDropNegative");
            }
        }
    }

    public void AddChapita()
    {
        if (GetComponentInParent<ChapaCountet>().chapas == 3)
        {
            chapitaCounter++;

            if (chapitaCounter == maxChapitas)
            {
                GetComponentInParent<FinishMinigameManager>().AddSlotCounter();
                Debug.Log("Listo");
            }
            else
            {
                chapita.transform.position = resetPos.transform.position;
                chapita.GetComponent<DragDropStay>().locked = false;
                chapita.GetComponent<CanvasGroup>().blocksRaycasts = true;
                Debug.Log("Added");
            }
            GetComponentInParent<ChapaCountet>().NullChapas();

            // UI Counter
            if (counterText != null)
            {
                counterNum--;
                counterText.text = counterNum.ToString();
            }
        }
        else { Debug.Log("Faltan Chapitas"); }
    }

    public void ResetPos()
    {
        if (GetComponentInParent<ChapaCountet>().chapas == 3)
        {
            chapitaCounter++;
            if (chapitaCounter != maxChapitas)
            {
                chapita.transform.position = resetPos.transform.position;
                chapita.GetComponent<DragDropStay>().locked = false;
                chapita.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }
    }

    public void Buffer()
    {
        StartCoroutine(Buffering());
    }

    IEnumerator Buffering()
    {
        yield return new WaitForSeconds(1.4f);
        AddChapita();
    }
}
