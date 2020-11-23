using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductImpresora : MonoBehaviour
{
    public GameObject text;
    private int partesCounter;
    public int maxPartes;

    private void Start()
    {
        text.SetActive(false);
    }

    public void AddCounter()
    {
        // Complete MJ
        partesCounter++;
        if (partesCounter == maxPartes)
        {
            StartCoroutine(WaitToFinish());
        }
    }
    IEnumerator WaitToFinish()
    {
        text.SetActive(true);
        yield return new WaitForSeconds(15);

        text.SetActive(false);
        GetComponentInParent<FinishMinigameManager>().AddSlotCounter();
        Debug.Log("Carga2");
    }
}
