using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    private byte maxPlayerPerRoom = 4;
    [SerializeField] TextMeshProUGUI roomName;
 public void OnClick_CreateRoom() 
    {
        if (!PhotonNetwork.IsConnected)
            return;
        PhotonNetwork.JoinOrCreateRoom(roomName.text, new RoomOptions { MaxPlayers = maxPlayerPerRoom }, TypedLobby.Default);

    }



    public override void OnCreatedRoom()
    {
        Debug.Log("Room Created " + roomName.text);
    }
}
