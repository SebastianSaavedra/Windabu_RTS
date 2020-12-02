﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using Com.MaluCompany.TestGame;

public class ColSaver : MonoBehaviourPunCallbacks,IPunObservable
{
    public Collider2D cols;
    public Collider2D cols2;
    public bool team;
    public Minijuego room;
    public Transform goToOnInterrupt;
    public bool canRepair;
    
    int queJugador;

    private void Start()
    {
        photonView.RPC("ResetValueA", RpcTarget.AllViaServer);
        photonView.RPC("ResetValueB", RpcTarget.AllViaServer);
    }


    public void RPCDisable() 
    {
        photonView.RPC("DisableCols", RpcTarget.AllViaServer);
    }
    public void RPCEnable()
    {
        photonView.RPC("EnableCols", RpcTarget.AllViaServer);
    }
    public void RPCAddPlayer(int playerActor, bool playerTeam) 
    {
        photonView.RPC("AddPlayer", RpcTarget.MasterClient, playerActor,playerTeam);
    }

    [PunRPC]
    public void AddPlayer(int id,bool team) 
    {
        if (team) 
        {
        room.jugadorUno = id;
        }
        else 
        {
            room.jugadorDos = id;
        }

    }

    [PunRPC]
    public void ResetValueA()
    {
        room.jugadorUno = 0;
    }
    [PunRPC]
    public void ResetValueB()
    {
        room.jugadorDos = 0;
    }

    [PunRPC]
    public void DisableCols() 
    {
        canRepair = true;
            cols.enabled = false;
            cols2.enabled = false;
        
    }
    [PunRPC]
    public void EnableCols()
    {
            canRepair = false;
            cols.enabled = true;       
            cols2.enabled = true;       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerTeam>().TeamA)
        {
            RPCAddPlayer(collision.GetComponent<PlayerId>().id, true);
        }
        if (collision.GetComponent<PlayerTeam>().TeamB)
        {
            RPCAddPlayer(collision.GetComponent<PlayerId>().id, false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerTeam>().TeamA)
        {
            photonView.RPC("ResetValueA", RpcTarget.AllViaServer);
        }
        if (collision.GetComponent<PlayerTeam>().TeamB)
        {
            photonView.RPC("ResetValueB", RpcTarget.AllViaServer);           
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(room.jugadorUno);
            stream.SendNext(room.jugadorDos);
        }
        else if (stream.IsReading)
        {
            room.jugadorUno = (int)stream.ReceiveNext();
            room.jugadorDos = (int)stream.ReceiveNext();
        }
    }

}
