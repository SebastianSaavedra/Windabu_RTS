﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

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
        whatTeamIsCalling = GameObject.Find("OriginPanel").GetComponent<WhatTeamIsCalling>();
        oP = GameObject.Find("OriginPanel").GetComponent<WhatTeamIsCalling>().gameObject;
        inRoom = false;
        inLvl1 = false;

        born.SetActive(true);
        born.GetComponent<TaskDropDownMinigame>().enabled = false;
        ready.SetActive(false);
        upgraded.SetActive(false);
    }

    private void Update()
    {
        switch (team) 
        {
            case true:
                gameObject.transform.parent = GameObject.Find("ImpresoraA").GetComponent<DataSaver>().gameObject.transform;
                break;
            case false:
                gameObject.transform.parent = GameObject.Find("ImpresoraB").GetComponent<DataSaver>().gameObject.transform;
                break;
        }
        if (born.GetComponent<TaskDropDownMinigame>().taskBarPanel.GetComponent<WhatTeamIsCalling>().mjFinished)
        {
        photonView.RPC("Finished", RpcTarget.AllViaServer,false,true);
        taskDropDownMinigame.OnLeavePanel(true);
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
