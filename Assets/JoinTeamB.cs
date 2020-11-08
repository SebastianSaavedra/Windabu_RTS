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
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponentInParent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                if (player.GetComponentInParent<PlayerTeam>().TeamA == true || player.GetComponentInParent<PlayerTeam>().TeamB == true)
                    return;
                player.GetComponentInParent<PlayerTeam>().TeamB = true;
                photonView.RPC("ActivateText", RpcTarget.AllBuffered, player.GetComponentInParent<PlayerId>().name);
            }
        }
    }

    [PunRPC]
    public void ActivateText(string name)
    {
        Debug.Log("Llamado");
        FakeLobbyUsers.users2[playerPos].gameObject.SetActive(true);
        FakeLobbyUsers.users2[playerPos].text = name;
        playerPos = playerPos + 1;
    }

}
