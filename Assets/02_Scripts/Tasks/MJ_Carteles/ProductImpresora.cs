using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductImpresora : MonoBehaviour
{
    public GameObject text;
    public GameObject timerBar;
    public GameObject loadingSprite;
    private int partesCounter;
    public int maxPartes;

    private void Start()
    {
        text.SetActive(false);
        timerBar.SetActive(false);
        if (loadingSprite != null)
        {
            loadingSprite.SetActive(false);
        }
    }

    public void AddCounter()
    {
        // Complete MJ
        partesCounter++;
        Debug.Log(partesCounter);
        if (partesCounter == maxPartes)
        {
            StartCoroutine(WaitToFinish());
        }
    }
    IEnumerator WaitToFinish()
    {
        text.SetActive(true);
        timerBar.SetActive(true);
        if (loadingSprite != null)
        {
            loadingSprite.SetActive(true);
        }
        yield return new WaitForSeconds(15);

        if (loadingSprite != null)
        {
            loadingSprite.SetActive(false);
        }
        text.SetActive(false);
        timerBar.SetActive(false);
        GetComponentInParent<FinishMinigameManager>().AddSlotCounter();
        Debug.Log("Carga2");
    }
}
