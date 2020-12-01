﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ShopManager : MonoBehaviourPunCallbacks
{

    //Stats
    [SerializeField]
    float impresoraCost = 100f, impresoraDelay = 15f, imp3dCost = 200f, imp3dDelay = 30f, chapitaCost = 150f, chapitaDelay = 20f, ploterCost = 300f, ploterDelay = 15f,
          merchCost = 1f, merchDelay = 1f, impIndusCost = 1f, impIndusDelay = 1f, scCost = 1f, scDelay = 20;
    public bool canBuy_0, canBuy_1, canBuy_2, canBuy_3, canBuy_4, canBuy_5;
    [SerializeField] bool sc0, sc1, sc2;
    int howManySc;
    //Prefabs
    public GameObject impresora, imp3d, chapita, impIndus, merch, plotter, superComputadora_1, superComputadora_2, superComputadora_3;

    [SerializeField] bool team;
    [SerializeField] UIManager uiMan;


    private void Start()
    {
    }

    public void OtraCompra()
    {
        if (team)
        {
            if (MinigameManager.dineroA >= impresoraCost)// volver a referencia teamManager
            {
                StartCoroutine(SpawnItem(impresoraDelay, 0));//ActivateItem
                Minijuegos.compraA((int)impresoraCost);
                uiMan.CallRpc(team, 1);// Para Queue Line
                                       //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
    }
    [PunRPC]
    public void cantBuy(int wichItem) 
    {
        switch (wichItem) 
        {
            case 0:
                canBuy_0 = false;
                break;
            case 1:
                canBuy_1 = false;
                break;
            case 2:
                canBuy_2 = false;
                break;
            case 3:
                canBuy_3 = false;
                break;
            case 4:
                canBuy_4 = false;
                break;
            case 5:
                canBuy_5 = false;
                break;
            case 6:
                howManySc++;
                switch (howManySc) 
                {
                    case 1:
                        sc0 = false;
                        sc1 = true;
                        break;
                    case 2:
                        sc1 = false;
                        sc2 = true;
                        break;
                    case 3:
                        sc2 = false;
                        break;
                }
                break;
        }
    }
    public void Impresora()
    {
        Debug.Log(team);
        if (team)
        {
            if (MinigameManager.dineroA >= impresoraCost && canBuy_0)// volver a referencia teamManager
            {
                RPCcor(impresoraDelay,0);
                Minijuegos.compraA((int)impresoraCost);
                uiMan.CallRpc(team, 0);
                photonView.RPC("cantBuy", RpcTarget.AllViaServer, 0);
                //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
        else
        {
            if (MinigameManager.dineroB >= impresoraCost && canBuy_0)// volver a referencia teamManager
            {
                RPCcor(impresoraDelay,0);
                MinigameManager.dineroB -= (int)impresoraCost;
                uiMan.CallRpc(team, 0);
                photonView.RPC("cantBuy", RpcTarget.AllViaServer, 0);
                //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
    }
    public void Imp3d()
    {
        if (team)
        {
            if (MinigameManager.dineroA >= (int)imp3dCost && canBuy_1)// volver a referencia teamManager
            {
                RPCcor(imp3dDelay, 1);
                Minijuegos.compraA((int)imp3dCost);
                uiMan.CallRpc(team, 1);
                photonView.RPC("cantBuy", RpcTarget.AllViaServer, 1);
                //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
        else
        {
            if (MinigameManager.dineroB >= (int)imp3dCost && canBuy_1)// volver a referencia teamManager
            {
                RPCcor(imp3dDelay, 1);
                MinigameManager.dineroB -= (int)imp3dCost;
                uiMan.CallRpc(team, 1);
                photonView.RPC("cantBuy", RpcTarget.AllViaServer, 1);
                //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
    }
    public void Chapitas()
    {
        if (team)
        {
            if (MinigameManager.dineroA >= (int)chapitaCost && canBuy_2)// volver a referencia teamManager
            {
                RPCcor(chapitaDelay, 2);
                Minijuegos.compraA((int)chapitaCost);
                uiMan.CallRpc(team, 2);
                photonView.RPC("cantBuy", RpcTarget.AllViaServer,2);
                //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
        else
        {
            if (MinigameManager.dineroB >= (int)chapitaCost && canBuy_2)// volver a referencia teamManager
            {
                RPCcor(chapitaDelay, 2);
                MinigameManager.dineroB -= (int)chapitaCost;
                uiMan.CallRpc(team, 2);
                photonView.RPC("cantBuy", RpcTarget.AllViaServer,2);
                //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
    }
    public void Plotter()
    {
        if (team)
        {
            if (MinigameManager.dineroA >= (int)ploterCost && canBuy_3)// volver a referencia teamManager
            {
                RPCcor(ploterDelay, 3);
                Minijuegos.compraA((int)ploterCost);
                uiMan.CallRpc(team, 3);
                photonView.RPC("cantBuy", RpcTarget.AllViaServer, 3);
                //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
        else
        {
            if (MinigameManager.dineroB >= (int)ploterCost && canBuy_3)// volver a referencia teamManager
            {
                RPCcor(ploterDelay, 3);
                MinigameManager.dineroB -= (int)ploterCost;
                uiMan.CallRpc(team, 3);
                photonView.RPC("cantBuy", RpcTarget.AllViaServer, 3);
                //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
    }
    public void Merch()
    {
        if (team)
        {
            if (MinigameManager.dineroA >= (int)merchCost && canBuy_4)// volver a referencia teamManager
            {
                RPCcor(merchDelay, 4);
                Minijuegos.compraA((int)merchCost);
                uiMan.CallRpc(team, 4);
                photonView.RPC("cantBuy", RpcTarget.AllViaServer, 4);
                //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
        else
        {
            if (MinigameManager.dineroB >= (int)merchCost && canBuy_4)// volver a referencia teamManager
            {
                RPCcor(merchDelay, 4);
                MinigameManager.dineroB -= (int)merchCost;
                uiMan.CallRpc(team, 4);
                photonView.RPC("cantBuy", RpcTarget.AllViaServer, 4);
                //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
    }
    public void ImpIndus()
    {
        if (team)
        {
            if (MinigameManager.dineroA >= (int)impIndusCost && canBuy_5)// volver a referencia teamManager
            {
                RPCcor(impIndusDelay, 5);
                Minijuegos.compraA((int)impIndusCost);
                uiMan.CallRpc(team, 5);
                photonView.RPC("cantBuy", RpcTarget.AllViaServer, 5);
                //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
        else
        {
            if (MinigameManager.dineroB >= (int)impIndusCost && canBuy_5)// volver a referencia teamManager
            {
                RPCcor(impIndusDelay, 5);
                MinigameManager.dineroB -= (int)impIndusCost;
                uiMan.CallRpc(team, 5);
                photonView.RPC("cantBuy", RpcTarget.AllViaServer, 5);
                //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
    }
    public void SuperPc()
    {
        if (team)
        {
            if (MinigameManager.dineroA >= (int)scCost && (sc0 || sc1 || sc2))// volver a referencia teamManager
            {
                ActSuperPc(scDelay, howManySc);
                Minijuegos.compraA((int)scCost);
                uiMan.CallRpc(team, 6);
                photonView.RPC("cantBuy", RpcTarget.AllViaServer, 6);
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
        else
        {
            if (MinigameManager.dineroB >= (int)scCost && (sc0 || sc1 || sc2))// volver a referencia teamManager
            {
                ActSuperPc(scDelay, howManySc);
                MinigameManager.dineroB -= (int)scCost;
                uiMan.CallRpc(team, 6);
                photonView.RPC("cantBuy", RpcTarget.AllViaServer, 6);
                //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
    }


    public void RPCcor(float time, int whatItem) 
    {
         photonView.RPC("StartCor", RpcTarget.AllViaServer,time,whatItem);
        //StartCor(time, whatItem);
    }

    [PunRPC]
    public void StartCor(float waitTime,int whatItem)
    {
        StartCoroutine(SpawnItem(waitTime,whatItem));
    } 
    [PunRPC]
    public void ActSuperPc(float waitTime,int whatPc)
    {
        switch (whatPc) 
        {
            case 0:
                StartCoroutine(PC1(waitTime));
                break;
            case 1:
                StartCoroutine(PC2(waitTime));
                break;
            case 2:
                StartCoroutine(PC3(waitTime));
                break;
        
        }
    }
    [PunRPC]
    IEnumerator PC1(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        superComputadora_1.SetActive(true);
        yield break;
    }
    [PunRPC]
    IEnumerator PC2(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        superComputadora_2.SetActive(true);
        yield break;
    }
    [PunRPC]
    IEnumerator PC3(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        superComputadora_3.SetActive(true);
        yield break;
    }

    [PunRPC]
    IEnumerator SpawnItem(float waitTime, int itemToSpawn)
    {
        yield return new WaitForSeconds(waitTime);
        switch (itemToSpawn) 
        {
            case 0:
                impresora.SetActive(true);
                break;
            case 1:
                imp3d.SetActive(true);
                break;
            case 2:
                chapita.SetActive(true);
                break;
            case 3:
                plotter.SetActive(true);
                break;
            case 4:
                merch.SetActive(true);
                break;
            case 5:
                impIndus.SetActive(true);
                break;
            //case 6:
            //    Debug.Log("Cuantas super "+ howManySc);
            //    switch (howManySc) 
            //    {
            //        case 1:
            //            superComputadora_1.SetActive(true);
            //            break;
            //        case 2:
            //            superComputadora_2.SetActive(true);
            //            break;
            //        case 3:
            //            superComputadora_3.SetActive(true);
            //            break;
            //    }
            //    break;
        }       
        yield break;
    }
}
