﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    GameObject itemSpawner;
    [SerializeField] Transform itemSpawnerPos;

    //Stats
    [SerializeField]
    float impresoraCost = 100f, impresoraDelay = 15f;

    //Prefabs
    public GameObject impresora;
    [SerializeField] bool team;
    [SerializeField] UIManager uiMan;


    private void Start()
    {
    }

    public void OtraCompra()
    {
        if (team) 
        {
        if(MinigameManager.dineroA >= impresoraCost)// volver a referencia teamManager
        {
            StartCoroutine(SpawnItem(impresoraDelay, impresora));
            MinigameManager.dineroA -= (int)impresoraCost;
            uiMan.CallRpc(team,1);
            //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
        }
        else
        {
            Debug.Log("Dinero Insuficiente Pe");
        }
        }
    }
    public void Impresora()
    {
        if (team)
        {
            if (MinigameManager.dineroA >= impresoraCost)// volver a referencia teamManager
            {
                StartCoroutine(SpawnItem(impresoraDelay, impresora));
                MinigameManager.dineroA -= (int)impresoraCost;
                uiMan.CallRpc(team, 0);
                //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
        else
        {
            if (MinigameManager.dineroB >= impresoraCost)// volver a referencia teamManager
            {
                StartCoroutine(SpawnItem(impresoraDelay, impresora));
                MinigameManager.dineroB -= (int)impresoraCost;
                uiMan.CallRpc(team, 0);
                //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
    }

    IEnumerator SpawnItem(float waitTime, GameObject itemToSpawn)
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(itemToSpawn, itemSpawnerPos);
    }
}
