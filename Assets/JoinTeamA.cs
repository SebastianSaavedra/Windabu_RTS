﻿using Photon.Pun;
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
        Debug.Log("TeamA");
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Debug.Log(player.name);
            if (player.GetComponentInParent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                player.GetComponentInParent<PlayerTeam>().TeamA = true;
                FakeLobbyUsers.users[playerPos].gameObject.SetActive(true);
                FakeLobbyUsers.users[playerPos].text = PhotonNetwork.LocalPlayer.NickName;
            }
        }
        playerPos = playerPos + 1;
    }

}
