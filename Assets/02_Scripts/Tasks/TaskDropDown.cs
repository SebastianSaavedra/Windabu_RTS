using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;
using Photon.Pun;
using Photon.Realtime;

public class TaskDropDown : MonoBehaviourPunCallbacks,I_Interactable
{
    [SerializeField] GameObject taskBarPanel;
    [SerializeField] ManagerMinijuegos managerMinijuegos;
    [SerializeField] PanelData panelData;
    public Minijuego minijuegoData;
    GameObject objetoInstanciado;
    public enum WiiMinigame {m1_1=0,m1_2=1,m2_1=2,m2_2=3,m3_1=4,m3_2=5};
    public WiiMinigame thisMinigame;
    
    public bool canInteract;
    public int thisMinigameis;

    private void Start()
    {
        thisMinigameis = (int)thisMinigame;
        canInteract = true;
        if (taskBarPanel== null) 
        {

        }
    }

    public void OnInteract(bool call) 
    {
        taskBarPanel.SetActive(true);
        taskBarPanel.GetComponent<WhatTeamIsCalling>().team = call;
        taskBarPanel.GetComponent<WhatTeamIsCalling>().id = (int)thisMinigame;
        if (call) 
        {
            GameObject panel = Instantiate(panelData.PanelA, taskBarPanel.transform.position, Quaternion.identity);
            objetoInstanciado = panel;
            panel.transform.parent = taskBarPanel.transform;
            panel.transform.localScale = new Vector3(1, 1, 1);
        }
        else 
        {
            GameObject panel = Instantiate(panelData.PanelB, taskBarPanel.transform.position, Quaternion.identity);
            objetoInstanciado = panel;
            panel.transform.parent = taskBarPanel.transform;
            panel.transform.localScale = new Vector3(1, 1, 1);
        }
        taskBarPanel.transform.DOMoveY(540, 1);
        int playerActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log("Desde el script TaskDropDown se printea el playerActor number: " + playerActorNumber);
        photonView.RPC("JugadorComienzaMinijuego1", RpcTarget.MasterClient, playerActorNumber,(int)thisMinigame);
        managerMinijuegos.idMinijuego = (int)thisMinigame;
    }

    [PunRPC] //esto lo llama el jugador
    public void JugadorComienzaMinijuego1(int playerActorNumber, int minigame)
    {
        managerMinijuegos.ComenzarUnMinijuego(minigame, playerActorNumber);
        Debug.Log("El minijuego lo comenzo el jugador: " + playerActorNumber);
    }

    //[PunRPC] //esto lo llama el jugador
    //public void JugadorComienzaMinijuego2(int playerActorNumber, int minigame)
    //{
    //    managerMinijuegos.ComenzarUnMinijuego(minigame, playerActorNumber);
    //    Debug.Log("El minijuego lo comenzo el jugador: " + playerActorNumber);
    //}

    //[PunRPC] //esto lo llama el jugador
    //public void JugadorComienzaMinijuego3(int playerActorNumber, int minigame)
    //{
    //    managerMinijuegos.ComenzarUnMinijuego(minigame, playerActorNumber);
    //    Debug.Log("El minijuego lo comenzo el jugador: " + playerActorNumber);
    //}
    public void OnLeavePanel(bool call)
    {
        taskBarPanel.transform.DOMoveY(1540, 1);
        Destroy(taskBarPanel.transform.GetChild(0).gameObject,1.1f);

    }
   
    [PunRPC]
    public void OnFinishTask()
    {
        StartCoroutine(BlockTask());
        managerMinijuegos.ResetearMuchosValores((int)thisMinigame);
    }

    public void RPCdata() 
    {
        photonView.RPC("OnFinishTask", RpcTarget.AllBuffered);
    }

    [PunRPC]
    IEnumerator BlockTask() 
    {
        canInteract = false;
        managerMinijuegos.minijuegos[(int)thisMinigame].completado = true;
        yield return new WaitForSeconds(35f);
        managerMinijuegos.minijuegos[(int)thisMinigame].completado = false;
        canInteract = true;
        yield break;
    }
}
