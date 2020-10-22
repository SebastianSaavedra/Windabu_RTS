using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IPointerDownHandler, IBeginDragHandler, IEndDragHandler
{
    //public enum OBJETO
    //{
    //    RODILLO,OTRO
    //};
    //[SerializeField] OBJETO objetos = OBJETO.RODILLO;
    [SerializeField] private Canvas canvas;

    Vector3 posInicial;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        posInicial = rectTransform.localPosition;
    }
    //Click
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked");
    }

    //1st frame of drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        //if (objetos == OBJETO.RODILLO)
        //{
            rectTransform.localScale = new Vector3(2f, 2f, 1f);
        //}
    }

    //Every frame dragged
    public void OnDrag(PointerEventData eventData)
    {
        //Move like cursor
        rectTransform.anchoredPosition += eventData.delta;
    }

    //Unclick
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
        //if(objetos == OBJETO.RODILLO)
        //{
            rectTransform.localPosition = posInicial;
        //}
    }
}
