using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class RoomList : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    void Start()
    {
        
    }

   public void SetRoomInfo(RoomInfo roomInfo) 
    {
        text.text = roomInfo.MaxPlayers + ", " + roomInfo.Name;
    }
    
}
