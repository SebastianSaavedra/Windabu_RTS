using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinTeamB : MonoBehaviourPunCallbacks
{
    int playerPos;
    int idPos;
    [SerializeField] GameObject playerPref;
    Transform instpos;
    bool callJoin;
    public GameObject eKey;
    public GameObject tKey;
    public GameObject fKey;
    public GameObject rKey;
    public GameObject escKey;

    private void Start()
    {
        instpos = GameObject.Find("TeamBPos").GetComponent<Transform>();
    }
    public void JoinTeam()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponentInParent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                if (player.GetComponentInParent<PlayerTeam>().TeamA && player.GetComponentInParent<PlayerTeam>().TeamB)
                    return;
                player.GetComponentInParent<PlayerTeam>().TeamB = true;
                photonView.RPC("ActivateText", RpcTarget.AllBuffered, player.GetComponentInParent<PlayerId>().name);
                photonView.RPC("AddPlayerId", RpcTarget.AllBuffered, player.GetComponentInParent<PlayerId>().id);
            }
        }
    }

    [PunRPC]
    public void ActivateText(string name)
    {
        FakeLobbyUsers.users2[playerPos].gameObject.SetActive(true);
        FakeLobbyUsers.users2[playerPos].text = name;
        playerPos = playerPos + 1;
    }
    [PunRPC]
    public void AddPlayerId(int id)
    {
        PhotonManager.teamB_id[idPos] = id;
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
            GameObject player = PhotonNetwork.Instantiate(this.playerPref.name, instpos.position, Quaternion.identity, 0);
            player.GetComponentInParent<PlayerTeam>().TeamB= true;
            callJoin = true;
            player.GetComponent<FeedbackTrigger>().eKey = eKey;
            player.GetComponent<FeedbackTrigger>().rKey = rKey;
            player.GetComponent<FeedbackTrigger>().tKey = tKey;
            player.GetComponent<FeedbackTrigger>().fKey = fKey;
            player.GetComponent<FeedbackTrigger>().escKey = escKey;
            Debug.Log("Llegaste aqui");
        }
    }

}
