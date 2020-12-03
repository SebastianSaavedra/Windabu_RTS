using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class FakeLobbyKill : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject lobby;
 public void fakeloby() 
    {
        photonView.RPC("rpcthing", RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void rpcthing() 
    {
        lobby.SetActive(false);
    }
}
