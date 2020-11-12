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
    [SerializeField] GameObject playerPref;
    bool callJoin;
    private void Start()
    {

    }
    public void JoinTeam() 
{
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponentInParent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
            {             
        photonView.RPC("ActivateText", RpcTarget.AllBuffered,player.GetComponentInParent<PlayerId>().name);
            }
        }
  }

    [PunRPC]
    public void ActivateText(string name) 
    {
        Debug.Log("Llamado");
        FakeLobbyUsers.users[playerPos].gameObject.SetActive(true);
        FakeLobbyUsers.users[playerPos].text = name;
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
           GameObject player=  PhotonNetwork.Instantiate(this.playerPref.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
            player.GetComponentInParent<PlayerTeam>().TeamA = true;
            callJoin = true;
            Debug.Log("Llegaste aqui");
        }
    }

}
