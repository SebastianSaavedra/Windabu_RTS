using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinTeamA : MonoBehaviourPunCallbacks
{
    int playerPos;
    int idPos;
    [SerializeField] GameObject playerPref;
    Transform instpos;
    bool callJoin;

    private void Start()
    {
        instpos = GameObject.Find("TeamAPos").GetComponent<Transform>();
    }
    public void JoinTeam() 
{
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponentInParent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                if (player.GetComponentInParent<PlayerTeam>().TeamA && player.GetComponentInParent<PlayerTeam>().TeamB)
                    return;
                player.GetComponentInParent<PlayerTeam>().TeamA = true;
                photonView.RPC("ActivateText", RpcTarget.AllBuffered, player.GetComponentInParent<PlayerId>().name);
                photonView.RPC("AddPlayerId", RpcTarget.AllBuffered, player.GetComponentInParent<PlayerId>().id);
            }
        }
  }

    [PunRPC]
    public void ActivateText(string name)
    {
        FakeLobbyUsers.users[playerPos].gameObject.SetActive(true);
        FakeLobbyUsers.users[playerPos].text = name;
        playerPos = playerPos + 1;     
    }
    [PunRPC]
    public void AddPlayerId(int id) 
    {
        PhotonManager.teamA_id[idPos] = id;
        idPos = idPos + 1;
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
           GameObject player=  PhotonNetwork.Instantiate(this.playerPref.name, instpos.position, Quaternion.identity, 0);           
            callJoin = true;
            Debug.Log("Llegaste aqui");
        }
    }

}
