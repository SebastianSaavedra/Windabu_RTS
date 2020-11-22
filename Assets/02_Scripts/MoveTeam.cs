using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MoveTeam : MonoBehaviourPunCallbacks
{
    //public List<Player> playersActuales = new List<Player>();
    //public GameObject teamAPos;
    //public GameObject teamBPos;
    //public GameObject[] players;



    //private void Start()
    //{
    //    players = new GameObject[6];
    //    if (PhotonNetwork.IsMasterClient)   //Tener en mente que este codigo puede producir errores a futuro.
    //    {
    //        //SetearUIMasterClient();
    //        playersActuales.Add(PhotonNetwork.LocalPlayer);
    //        Debug.Log("Maestro Unido");

    //    }
    //}
    //Player TargetPlayerByActorNumber(int playerActorNumber)
    //{
    //    //Solamente se tiene para mostrar el número de jugadores y su actorNumber
    //    foreach (var item in playersActuales)
    //    {
    //        Debug.Log("Players: " + item.ActorNumber);
    //    }

    //    //Determina a qué jugador debemos enviarle el RPC según su actor number 
    //    //(identificador de cada jugador en esta sala de juego)
    //    Player playerToReturn = null;
    //    for (int i = 0; i < playersActuales.Count; i++)
    //    {
    //        if (playersActuales[i].ActorNumber == playerActorNumber)
    //        {
    //            playerToReturn = playersActuales[i];
    //            break;
    //        }
    //    }
    //    return playerToReturn;
    //}
    //public void CallRpc() 
    //{
    //    photonView.RPC("MovePlayer",RpcTarget.AllViaServer);
    //}
    //[PunRPC]
    //public void MovePlayer()
    //{
    //       foreach(GameObject player in players) 
    //    {
    //        if (player.GetComponent<PlayerTeam>().TeamA)
    //        {
    //            player.transform.position = teamAPos.transform.position;
    //        }
    //        if (player.GetComponent<PlayerTeam>().TeamA)
    //        {
    //            player.transform.position = teamBPos.transform.position;
    //        }
    //    }
    //}
    //public override void OnPlayerEnteredRoom(Player newPlayer)
    //{
    //    Debug.Log("Jugador para mover entra a room");
    //    Debug.Log(newPlayer.NickName);
    //    playersActuales.Add(newPlayer);
    //}

}
