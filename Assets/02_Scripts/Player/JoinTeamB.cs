﻿using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinTeamB : MonoBehaviourPunCallbacks
{
    int playerPos;
    [SerializeField] GameObject playerPref;
    bool callJoin;
    public void JoinTeam()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponentInParent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                if (player.GetComponentInParent<PlayerTeam>().TeamA && player.GetComponentInParent<PlayerTeam>().TeamB)
                    return;
                player.GetComponentInParent<PlayerTeam>().TeamB = true;
                photonView.RPC("ActivateText", RpcTarget.AllBuffered, player.GetComponentInParent<PlayerId>().name, player.GetComponentInParent<PlayerId>().id);
            }
        }
    }

    [PunRPC]
    public void ActivateText(string name, int id)
    {
        Debug.Log("Llamado");
        FakeLobbyUsers.users2[playerPos].gameObject.SetActive(true);
        FakeLobbyUsers.users2[playerPos].text = name;
        if (photonView.IsMine) 
        {
            PhotonManager.teamB_id[playerPos] = id;
        }
        playerPos = playerPos + 1;
    }
    public void InstPlayer()
    {
        if (callJoin)
            return;
        if (playerPref == null)
        {
            Debug.LogError("Bruh");
        }
        else
        {
            Debug.LogFormat("Instantiating Player");
            GameObject player = PhotonNetwork.Instantiate(this.playerPref.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
            player.GetComponentInParent<PlayerTeam>().TeamB= true;
            callJoin = true;
            Debug.Log("Llegaste aqui");
        }
    }

}
