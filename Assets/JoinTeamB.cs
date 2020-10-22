using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinTeamB : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public void JoinTeam()
    {
        Debug.Log("TeamB");
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Debug.Log(player.name);
            if (player.GetComponentInParent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                player.GetComponentInParent<PlayerTeam>().TeamB = true;
            }
        }
    }
}
