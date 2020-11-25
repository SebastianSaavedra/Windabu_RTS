using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class RPCManager : MonoBehaviourPunCallbacks
{
    [SerializeField] ManagerMinijuegos managerMinijuegos;
    public void RPCActualizarDatosA(int take,int Out)
    {
        Debug.Log("Inicio el RPCActualizarDatosA");
        photonView.RPC("ActualizarDatosA",RpcTarget.MasterClient,take,Out);
        Debug.Log("Debug del take: " + take + "Debug del out: " + Out);
    }

    public void RPCActualizarDatosB(int take, int Out)
    {
        Debug.Log("Inicio el RPCActualizarDatosB");
        photonView.RPC("ActualizarDatosB", RpcTarget.MasterClient, take, Out);
    }

    [PunRPC]
    void ActualizarDatosA(int take,int Out)
    {
        managerMinijuegos.minijuegos[take].barraVersusA = Out;
    }

    [PunRPC]
    void ActualizarDatosB(int take, int Out)
    {
        managerMinijuegos.minijuegos[take].barraVersusB = Out;
    }
}
