using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ShopManager : MonoBehaviourPunCallbacks
{
    GameObject itemSpawner;
    [SerializeField] Transform itemSpawnerPos;

    //Stats
    [SerializeField]
    float impresoraCost = 100f, impresoraDelay = 15f, imp3dCost = 200f, imp3dDelay = 30f, chapitaCost = 150f, chapitaDelay = 20f;
    public bool canBuy_0, canBuy_1, canBuy_2;
    //Prefabs
    public GameObject impresora, imp3d, chapita;
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
            StartCoroutine(SpawnItem(impresoraDelay, 0));
            Minijuegos.compraA((int)impresoraCost);
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
                RPCcor(impresoraDelay,0);
                Minijuegos.compraA((int)impresoraCost);
                uiMan.CallRpc(team, 0);
                canBuy_0 = true;
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
                RPCcor(impresoraDelay,0);
                MinigameManager.dineroB -= (int)impresoraCost;
                uiMan.CallRpc(team, 0);
                canBuy_0 = true;
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
            if (MinigameManager.dineroA >= (int)imp3dCost)// volver a referencia teamManager
            {
                RPCcor(imp3dDelay, 1);
                Minijuegos.compraA((int)imp3dCost);
                uiMan.CallRpc(team, 1);
                canBuy_0 = true;
                //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
        else
        {
            if (MinigameManager.dineroB >= (int)imp3dCost)// volver a referencia teamManager
            {
                RPCcor(imp3dDelay, 1);
                MinigameManager.dineroB -= (int)imp3dCost;
                uiMan.CallRpc(team, 1);
                canBuy_0 = true;
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
            if (MinigameManager.dineroA >= (int)chapitaCost)// volver a referencia teamManager
            {
                RPCcor(chapitaDelay, 2);
                Minijuegos.compraA((int)chapitaCost);
                uiMan.CallRpc(team, 2);
                canBuy_0 = true;
                //gameObject.GetComponent<TeamManager>().money -= impresoraCost;
            }
            else
            {
                Debug.Log("Dinero Insuficiente Pe");
            }
        }
        else
        {
            if (MinigameManager.dineroB >= (int)chapitaCost)// volver a referencia teamManager
            {
                RPCcor(chapitaDelay, 2);
                MinigameManager.dineroB -= (int)chapitaCost;
                uiMan.CallRpc(team, 2);
                canBuy_0 = true;
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
        // photonView.RPC("StartCor", RpcTarget.MasterClient,time,whatItem);
        StartCor(time, whatItem);
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
            GameObject spawned = PhotonNetwork.Instantiate(impresora.name, itemSpawnerPos.position, Quaternion.identity);
            spawned.transform.parent = itemSpawnerPos.transform;
            spawned.GetComponentInChildren<TaskDropDownMinigame>().taskBarPanel = itemSpawnerPos.GetComponent<DataSaver>().originalPanel;
                spawned.GetComponentInChildren<TaskDropDownMinigame>().managerMinijuegos = itemSpawnerPos.GetComponent<DataSaver>().manager;
                break;
            case 1:
                GameObject spawned2 = PhotonNetwork.Instantiate(imp3d.name, itemSpawnerPos.position, Quaternion.identity);
                spawned2.transform.parent = itemSpawnerPos.transform;
                spawned2.GetComponentInChildren<TaskDropDownMinigame>().taskBarPanel = itemSpawnerPos.GetComponent<DataSaver>().originalPanel;
                spawned2.GetComponentInChildren<TaskDropDownMinigame>().managerMinijuegos = itemSpawnerPos.GetComponent<DataSaver>().manager;
                break;
            case 2:
                GameObject spawned3 = PhotonNetwork.Instantiate(chapita.name, itemSpawnerPos.position, Quaternion.identity);
                spawned3.transform.parent = itemSpawnerPos.transform;
                spawned3.GetComponentInChildren<TaskDropDownMinigame>().taskBarPanel = itemSpawnerPos.GetComponent<DataSaver>().originalPanel;
                spawned3.GetComponentInChildren<TaskDropDownMinigame>().managerMinijuegos = itemSpawnerPos.GetComponent<DataSaver>().manager;
                break;
        }
        
        yield break;
    }
}
