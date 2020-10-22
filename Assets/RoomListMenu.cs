using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListMenu : MonoBehaviourPunCallbacks
{

    [SerializeField] Transform content;
    [SerializeField] GameObject _roomList;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList ) 
        {
            GameObject listing = Instantiate(_roomList, content);
            if (listing != null)
                listing.GetComponent<RoomList>().SetRoomInfo(info);
        }
    }
}
