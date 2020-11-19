using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Collections;

public class Cartel : MonoBehaviour
{
    [SerializeField] GameObject cartel;
    [SerializeField] GameObject theParent;
    [HideInInspector] public GameObject cartelito;
    public bool hayCartel = false;
    public void SpawnCartel()
    {
        if(!hayCartel)
        {
            Debug.Log("Dio click al Cartel");
            hayCartel = true;
            //Vector3 screenPos = Input.mousePosition;
            //Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
            cartelito = Instantiate(cartel, transform.position, Quaternion.identity);
            cartelito.transform.parent = theParent.transform;
            cartelito.transform.localScale = new Vector3(1.25f,1.25f,1.25f);
            cartelito.transform.DOLocalMove(new Vector3(0f, 0f, 0f), 1f, true);
            //cartelito.transform.DOScale(new Vector2(1.25f, 1.25f),1f);
        }
    }
}
