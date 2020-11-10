using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class RPCparaClienteMaestro : MonoBehaviourPunCallbacks
{
    [SerializeField] ManagerMinijuegos managerMinijuegos;

    //Presiono boton jugar minijuego1 
    public void QuieroJugarMinijuego1()
    {
        int playerActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        photonView.RPC("JugadorComienzaMinijuego1", RpcTarget.MasterClient, playerActorNumber);
    }

    //-------- minijuego 1 carteles
    [PunRPC] //esto lo llama el jugador
    public void JugadorComienzaMinijuego1(int playerActorNumber)
    {
        managerMinijuegos.ComenzarUnMinijuego(0, playerActorNumber);
    }
}
