using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ShopManager : MonoBehaviourPunCallbacks
{

    //Stats
    [SerializeField]
    float impresoraCost = 100f, impresoraDelay = 15f, imp3dCost = 200f, imp3dDelay = 30f, chapitaCost = 150f, chapitaDelay = 20f, ploterCost = 200f, ploterDelay = 1f;
    public bool canBuy_0, canBuy_1, canBuy_2;
    //Prefabs
    public GameObject impresora, imp3d, chapita,impresoraInd,merch,plotter,superComputadora_1, superComputadora_2, superComputadora_3;
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
            if (MinigameManager.dineroA >= (int)ploterCost && canBuy_2)// volver a referencia teamManager
            {
                RPCcor(ploterDelay, 2);
                Minijuegos.compraA((int)ploterCost);
                uiMan.CallRpc(team, 2);
                photonView.RPC("cantBuy", RpcTarget.AllViaServer, 2);
                //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
        else
        {
            if (MinigameManager.dineroB >= (int)ploterCost && canBuy_2)// volver a referencia teamManager
            {
                RPCcor(ploterDelay, 2);
                MinigameManager.dineroB -= (int)ploterCost;
                uiMan.CallRpc(team, 2);
                photonView.RPC("cantBuy", RpcTarget.AllViaServer, 2);
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
    IEnumerator SpawnItem(float waitTime, int itemToSpawn)
    {
        yield return new WaitForSeconds(waitTime);
        switch (itemToSpawn) 
        {
            case 0:
                //GameObject spawned = PhotonNetwork.Instantiate(impresora.name, itemSpawnerPos.position, Quaternion.identity);
                //spawned.transform.parent = itemSpawnerPos.transform;
                //spawned.GetComponentInChildren<TaskDropDownMinigame>().taskBarPanel = itemSpawnerPos.GetComponent<DataSaver>().originalPanel;
                //    spawned.GetComponentInChildren<TaskDropDownMinigame>().managerMinijuegos = itemSpawnerPos.GetComponent<DataSaver>().manager;
                impresora.SetActive(true);
                break;
            case 1:
                //GameObject spawned2 = PhotonNetwork.Instantiate(imp3d.name, itemSpawnerPos.position, Quaternion.identity);
                //spawned2.transform.parent = itemSpawnerPos.transform;
                //spawned2.GetComponentInChildren<TaskDropDownMinigame>().taskBarPanel = itemSpawnerPos.GetComponent<DataSaver>().originalPanel;
                //spawned2.GetComponentInChildren<TaskDropDownMinigame>().managerMinijuegos = itemSpawnerPos.GetComponent<DataSaver>().manager;
                imp3d.SetActive(true);
                break;
            case 2:
                //GameObject spawned3 = PhotonNetwork.Instantiate(chapita.name, itemSpawnerPos.position, Quaternion.identity);
                //spawned3.transform.parent = itemSpawnerPos.transform;
                //spawned3.GetComponentInChildren<TaskDropDownMinigame>().taskBarPanel = itemSpawnerPos.GetComponent<DataSaver>().originalPanel;
                //spawned3.GetComponentInChildren<TaskDropDownMinigame>().managerMinijuegos = itemSpawnerPos.GetComponent<DataSaver>().manager;
                chapita.SetActive(true);
                break;
            case 3:
                plotter.SetActive(true);
                break;
        }
        
        yield break;
    }
}
