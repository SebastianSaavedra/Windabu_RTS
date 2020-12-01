using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField] TextMeshProUGUI cartel, chapita, stick;
    private void Start()
    {
        instpos = GameObject.Find("TeamBPos").GetComponent<Transform>();
    }
    public void JoinTeam(string name, int playerActor)
    {
        //foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        //{
        //    if (player.GetComponentInParent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
        //    {
        //        if (player.GetComponentInParent<PlayerTeam>().TeamA && player.GetComponentInParent<PlayerTeam>().TeamB)
        //            return;
        //        player.GetComponentInParent<PlayerTeam>().TeamA = true;
        //        //photonView.RPC("ActivateText", RpcTarget.AllBuffered, player.GetComponentInParent<PlayerId>().name);
        //        // photonView.RPC("AddPlayerId", RpcTarget.AllBuffered, player.GetComponentInParent<PlayerId>().id);
        //    }
        //}
        photonView.RPC("ActivateText", RpcTarget.AllBuffered, name);
        photonView.RPC("AddPlayerId", RpcTarget.AllBuffered, playerActor);
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
        else
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (player.GetComponentInParent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    if (player.GetComponentInParent<PlayerTeam>().TeamA || player.GetComponentInParent<PlayerTeam>().TeamB)
                        return;
                }
            }
        }

        if (playerPref == null)
        {
            Debug.LogError("Bruh");
        }
        else
        {
            Debug.LogFormat("Instantiating Player");
            GameObject player = PhotonNetwork.Instantiate(this.playerPref.name, instpos.position, Quaternion.identity, 0);
            player.GetComponent<PlayerTeam>().TeamB = true;
            player.GetComponent<FeedbackTrigger>().eKey = eKey;
            player.GetComponent<FeedbackTrigger>().rKey = rKey;
            player.GetComponent<FeedbackTrigger>().tKey = tKey;
            player.GetComponent<FeedbackTrigger>().fKey = fKey;
            player.GetComponent<FeedbackTrigger>().escKey = escKey;
            player.GetComponent<LocalPlayerManager>().cartel = cartel;
            player.GetComponent<LocalPlayerManager>().chapitas = chapita;
            player.GetComponent<LocalPlayerManager>().stick = stick;
            JoinTeam(PhotonNetwork.LocalPlayer.NickName, PhotonNetwork.LocalPlayer.ActorNumber);
            Debug.Log("Llegaste aqui");
            callJoin = true;
        }
    }

}
