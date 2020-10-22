using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinTeamB : MonoBehaviourPunCallbacks
{
    int playerPos;
    public void JoinTeam()
    {
        Debug.Log("TeamB");
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Debug.Log(player.name);
            if (player.GetComponentInParent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                player.GetComponentInParent<PlayerTeam>().TeamB = true;
                FakeLobbyUsers.users2[playerPos].gameObject.SetActive(true);
                FakeLobbyUsers.users2[playerPos].text = PhotonNetwork.LocalPlayer.NickName;
            }
        }
        playerPos = playerPos + 1;
    }
}
