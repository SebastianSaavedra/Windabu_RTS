using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinTeamA : MonoBehaviourPunCallbacks
{
    int playerPos;

    public void JoinTeam() 
{
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponentInParent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                if (player.GetComponentInParent<PlayerTeam>().TeamA == true || player.GetComponentInParent<PlayerTeam>().TeamB==true)
                    return;
                player.GetComponentInParent<PlayerTeam>().TeamA = true;
                FakeLobbyUsers.users[playerPos].gameObject.SetActive(true);
                FakeLobbyUsers.users[playerPos].text = PhotonNetwork.LocalPlayer.NickName;
            }
        }
        playerPos = playerPos + 1;
    }
}
