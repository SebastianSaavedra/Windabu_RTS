using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;
using Photon.Pun;
using Photon.Realtime;

public class TaskDropDown : MonoBehaviourPunCallbacks,I_Interactable
{
    [SerializeField] GameObject taskBarPanelA;
    [SerializeField] GameObject taskBarPanelB;
    [SerializeField] ManagerMinijuegos managerMinijuegos;
    public enum WiiMinigame {m1_1=0,m1_2=1,m2_1=2,m2_2=3,m3_1=4,m3_2=5};
    public WiiMinigame thisMinigame;
    
    public bool canInteract;
    [SerializeField] int thisMinigameis;

    private void Start()
    {
        thisMinigameis = (int)thisMinigame;
        canInteract = true;
        if (taskBarPanelA || taskBarPanelB == null) 
        {

        }
    }

    public void OnInteract(bool call) 
    {
        if (call) 
        {
        taskBarPanelA.SetActive(true);
        taskBarPanelA.transform.DOMoveY(136, 1);
        Debug.Log("Hola");
        }
        else 
        {
            taskBarPanelB.SetActive(true);
            taskBarPanelB.transform.DOMoveY(136, 1);
            Debug.Log("Hola");
        }
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
        if (call) 
        {
        taskBarPanelA.transform.DOMoveY(201, 1);
        }
        else 
        {
        taskBarPanelB.transform.DOMoveY(201, 1);
        }
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
