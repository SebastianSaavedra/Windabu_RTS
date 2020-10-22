using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cartel : MonoBehaviour
{
    [SerializeField] GameObject cartel;
    [SerializeField] GameObject theParent;
    Camera cam;
    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    public void SpawnCartel()
    {
        Debug.Log("Dio click al Cartel");
        //Vector3 screenPos = Input.mousePosition;
        //Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
        GameObject cartelito = Instantiate(cartel,transform.position,Quaternion.identity);
        cartelito.transform.parent = theParent.transform;
        cartelito.transform.DOLocalMove(new Vector3(0f,0f,0f),1f,true);
        //cartelito.transform.DOScale(new Vector2(1.25f, 1.25f),1f);
    }
}
