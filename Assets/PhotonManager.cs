﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks,IPunObservable
{
    public static int[] teamA_id;
    public static int[] teamB_id;
    public int[] teamA_Id_Var;
    public int[] teamB_Id_Var;
    [SerializeField] int receivedInt;
    [SerializeField] Text[] teamA;
    [SerializeField] Text[] teamB;
    private void Awake()
    {
        PhotonNetwork.AddCallbackTarget(this);
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 10;
    }
    void Start()
    {
        teamA_id = new int[3];
        teamB_id = new int[3];
    }    

    // Update is called once per frame
    void Update()
    {
        teamA_Id_Var = teamA_id;     
        teamB_Id_Var = teamB_id;
        teamA[0].text = ""+  teamA_id[0];
        teamA[1].text = "" + teamA_id[1];
        teamA[2].text = "" + teamA_id[2];
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(teamA_id[0]);
            stream.SendNext(teamA_id[1]);
            stream.SendNext(teamA_id[2]);
            stream.SendNext(teamB_id[0]);
            stream.SendNext(teamB_id[1]);
            stream.SendNext(teamB_id[2]);
        }
        else if (stream.IsReading)
        {
            teamA_id[0] = (int)stream.ReceiveNext();
            teamA_id[1] = (int)stream.ReceiveNext();
            teamA_id[2] = (int)stream.ReceiveNext();
            teamB_id[0] = (int)stream.ReceiveNext();
            teamB_id[1] = (int)stream.ReceiveNext();
            teamB_id[2] = (int)stream.ReceiveNext();
        }
    }

}
