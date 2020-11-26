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
    float impresoraCost = 100f, impresoraDelay = 15f;
    public bool canBuy_0, canBuy_1, canBuy_2;
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
                MinigameManager.dineroA -= (int)impresoraCost;
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
    public void RPCcor(float time, int whatItem) 
    {
        photonView.RPC("StartCor", RpcTarget.AllViaServer,time,whatItem);
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
        if (itemToSpawn == 0) 
        {
            GameObject spawned = Instantiate(impresora, itemSpawnerPos);
            spawned.GetComponentInChildren<TaskDropDownMinigame>().taskBarPanel = itemSpawnerPos.GetComponent<DataSaver>().originalPanel;
            spawned.GetComponentInChildren<TaskDropDownMinigame>().managerMinijuegos = itemSpawnerPos.GetComponent<DataSaver>().manager;
        }
        yield break;
    }
}
