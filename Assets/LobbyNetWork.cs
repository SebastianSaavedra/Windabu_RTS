using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyNetWork : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        print("Connecting to server");
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        print("Connected to master");
    }
}
