using System;
using System.Collections;
using System.Collections.Generic;
using Com.MaluCompany.TestGame;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
public static class Minijuegos
{
    //public static Action<int> m_cartel;
    public static Action<int> m_clicksA;
    public static Action<int> m_clicksB;
    public static Action<int> compraA;
    public static Action<int> compraB;
}

public class MinigameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Variables
    [SerializeField]
    public static int dineroA;
    public static int dineroB;
    ManagerMinijuegos managerMinijuegos;
    #endregion

    private void Awake()
    {
        managerMinijuegos = this.GetComponent<ManagerMinijuegos>();
    }

    void Start()
    {
        //Minijuegos.m_cartel += Carteles;
        Minijuegos.m_clicksA += RPCClickA;
        Minijuegos.m_clicksB += RPCClickB;
        Minijuegos.compraA   += RPCCompraA;
        Minijuegos.compraB   += RPCCompraB;
    }
    public void RPCClickA(int valor)
    {
        photonView.RPC("ClicksA", RpcTarget.MasterClient, valor);
        Debug.Log("MoneySent");
    }
    public void RPCClickB(int valor)
    {
        photonView.RPC("ClicksB", RpcTarget.MasterClient, valor);
        Debug.Log("MoneySent");
    }
    public void RPCCompraA(int valor)
    {
        photonView.RPC("CompraA", RpcTarget.MasterClient, valor);
        Debug.Log("MoneySpent");
    }
    public void RPCCompraB(int valor)
    {
        photonView.RPC("CompraB", RpcTarget.MasterClient, valor);
        Debug.Log("MoneySpent");
    }

    [PunRPC]
    void ClicksA(int valor)
    {
        dineroA += valor;
    }
    [PunRPC]
    void ClicksB(int valor)
    {
        dineroB += valor;
    }
    [PunRPC]
    void CompraA(int valor)
    {
        dineroA -= valor;
    }
    [PunRPC]
    void CompraB(int valor)
    {
        dineroB -= valor;
    }

    public void FinishTask()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponentInParent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                player.GetComponentInParent<TEST_Movement>().enabled = true;
                if (player.GetComponentInParent<PlayerTeam>().TeamA)
                {
                    player.GetComponentInParent<TEST_Interact>().speakingTo.Team1();
                }
                else if (player.GetComponentInParent<PlayerTeam>().TeamB)
                {
                    player.GetComponentInParent<TEST_Interact>().speakingTo.Team2();
                }
                managerMinijuegos.ReseteoDeCarteles(player.GetComponentInParent<TEST_Interact>().minigameID);
                player.GetComponentInParent<TEST_Interact>().objectToInteract.GetComponent<I_Interactable>().OnLeavePanel(player.GetComponentInParent<PlayerTeam>().team);
                player.GetComponentInParent<TEST_Interact>().alreadyInteracted=false;
                player.GetComponentInParent<TEST_Interact>().alreadyChanged=false;
                player.GetComponentInParent<TEST_Interact>().thisTask.RPCdata();
            }
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(dineroA);
            stream.SendNext(dineroB);
        }
        else if (stream.IsReading)
        {
            dineroA = (int)stream.ReceiveNext();
            dineroB = (int)stream.ReceiveNext();
        }
    }
}
