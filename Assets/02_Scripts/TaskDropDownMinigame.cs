using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;
using Photon.Pun;
using Photon.Realtime;

public class TaskDropDownMinigame : MonoBehaviourPunCallbacks, I_Interactable
{
    public GameObject taskBarPanel;
    public ManagerMinijuegos managerMinijuegos;
    public PanelData panelData;
    GameObject objetoInstanciado;

    [SerializeField] GameObject parent;

    public bool canInteract;

    private void Start()
    {
        canInteract = true;
        if (taskBarPanel == null)
        {

        }
    }

    private void Update()
    {
        if (taskBarPanel == null)
        {
            taskBarPanel = parent.GetComponent<BoxProgression>().whatTeamIsCalling.GetComponent<WhatTeamIsCalling>().gameObject;
        }
    }

    public void OnInteract(bool call)
    {
        taskBarPanel.SetActive(true);
            GameObject panel = Instantiate(panelData.PanelA, taskBarPanel.transform.position, Quaternion.identity);
            objetoInstanciado = panel;
            panel.transform.parent = taskBarPanel.transform;
            panel.transform.localScale = new Vector3(1, 1, 1);

        taskBarPanel.transform.DOMoveY(540, 1);
    }
    public void OnLeavePanel(bool call)
    {
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
}
