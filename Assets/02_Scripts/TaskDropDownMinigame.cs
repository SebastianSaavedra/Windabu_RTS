using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;
using Photon.Pun;
using Photon.Realtime;

public class TaskDropDownMinigame : MonoBehaviourPunCallbacks, I_Interactable,IPunObservable
{
    public GameObject taskBarPanel;
    public ManagerMinijuegos managerMinijuegos;
    public PanelData panelData;
    GameObject objetoInstanciado;
    public int moneyToentry;
    public bool Interactonce;
    public bool stopoutsiders;
    public bool canInteract;

    private void Start()
    {
        canInteract = true;
        if (taskBarPanel == null)
        {

        }
    }

    public void StopOther(bool bruh) 
    {
        photonView.RPC("RpcOut", RpcTarget.MasterClient,bruh);
    }
    [PunRPC]
    public void RpcOut(bool bruh) 
    {
        stopoutsiders = bruh;
    }
    private void Update()
    {
    }

    public void OnInteract(bool call)
    {
        taskBarPanel.SetActive(true);
        taskBarPanel.GetComponent<WhatTeamIsCalling>().team = call;
        GameObject panel = Instantiate(panelData.PanelA, taskBarPanel.transform.position, Quaternion.identity);
            objetoInstanciado = panel;
            panel.transform.parent = taskBarPanel.transform;
            panel.transform.localScale = new Vector3(1, 1, 1);

        taskBarPanel.transform.DOMoveY(540, 1);
    }
    public void OnLeavePanel(bool call)
    {
        StopOther(false);
        taskBarPanel.transform.DOMoveY(1540, 1);
        Destroy(taskBarPanel.transform.GetChild(0).gameObject, 1.1f);
    }

    [PunRPC]
    public void OnFinishTask()
    {
        StartCoroutine(BlockTask());
    }

    public void RPCdata()
    {
        photonView.RPC("OnFinishTask", RpcTarget.AllBuffered);
    }


    [PunRPC]
    IEnumerator BlockTask()
    {
        canInteract = false;
        yield return new WaitForSeconds(5f);
        canInteract = true;
        yield break;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(stopoutsiders);
        }
        else if (stream.IsReading)
        {
            stopoutsiders = (bool)stream.ReceiveNext();
        }
    }
}
