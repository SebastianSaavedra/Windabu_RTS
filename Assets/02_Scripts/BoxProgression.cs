using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Com.MaluCompany.TestGame;

public class BoxProgression : MonoBehaviourPunCallbacks
{
    public WhatTeamIsCalling whatTeamIsCalling;
    public TaskDropDownMinigame taskDropDownMinigame;
    public GameObject oP;
    [HideInInspector]
    public bool inRoom;
    bool inLvl1;
    [SerializeField] bool team;

    // Tras comprar - Listo para armar - Disponible para producir - Mejorado
    public GameObject born, ready, upgraded;

    private void Start()
    {
        inRoom = false;
        inLvl1 = false;

        born.SetActive(true);
        ready.SetActive(false);
        upgraded.SetActive(false);
    }

    private void Update()
    {
        //switch (team) 
        //{
        //    case true:
        //        gameObject.transform.parent = GameObject.Find("ImpresoraA").GetComponent<DataSaver>().gameObject.transform;
        //        break;
        //    case false:
        //        gameObject.transform.parent = GameObject.Find("ImpresoraB").GetComponent<DataSaver>().gameObject.transform;
        //        break;
        //}
    }

    public void CallFinish() 
    {
        photonView.RPC("Finished", RpcTarget.AllViaServer, false, true);
        if (PhotonNetwork.LocalPlayer.ActorNumber == GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerId>().id) 
        {
        GameObject.FindGameObjectWithTag("Player").GetComponent<TEST_Movement>().enabled = true;
        taskDropDownMinigame.OnLeavePanel(true);
        Debug.Log("Bruh");
        }
    }
    [PunRPC]
    public void Finished(bool deactive, bool active) 
    {
        inRoom = active;
        ready.SetActive(active);
        born.SetActive(deactive);
        whatTeamIsCalling.mjFinished = deactive;
        Debug.Log("Impresora Hecha");
    }

    public void Traveling()
    {
        GetComponentInChildren<BoxCollider2D>().enabled = false;
    }

   
    public void Delivered()
    {
        inRoom = true;
        born.GetComponent<TaskDropDownMinigame>().enabled = true;
        born.GetComponent<TaskDropDownMinigame>().canInteract = true;
    }
}
