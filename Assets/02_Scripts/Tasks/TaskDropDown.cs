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
    public enum WiiMinigame {m1_1=0,m1_2=1,m2_1=2,m2_2=3,m3_1=4,m3_2=5};
    public WiiMinigame thisMinigame;
    
    public bool canInteract;
    [SerializeField] int thisMinigameis;

    private void Start()
    {
        thisMinigameis = (int)thisMinigame;
        canInteract = true;
        if (taskBarPanel == null) 
        {

        }
    }

    public void OnInteract() 
    {
        taskBarPanel.SetActive(true);
        Debug.Log("Hola");
        taskBarPanel.transform.DOMoveY(540,1);

        int playerActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log("Desde el script TaskDropDown se printea el playerActor number: " + playerActorNumber);
        photonView.RPC("JugadorComienzaMinijuego1", RpcTarget.MasterClient, playerActorNumber,(int)thisMinigame);
    }

    [PunRPC] //esto lo llama el jugador
    public void JugadorComienzaMinijuego1(int playerActorNumber, int minigame)
    {
        managerMinijuegos.ComenzarUnMinijuego(minigame, playerActorNumber);
        Debug.Log("El minijuego lo comenzo el jugador: " + playerActorNumber);
    }
    public void OnLeavePanel()
    {
        taskBarPanel.transform.DOMoveY(1540, 1);
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
        yield return new WaitForSeconds(5f);
        canInteract = true;
        yield break;
    }
}
