using System;
using System.Collections;
using System.Collections.Generic;
using Com.MaluCompany.TestGame;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
public static class Minijuegos
{
    public static Action<int> m_cartel;
    public static Action<int> m_clicks;
}

public class MinigameManager : MonoBehaviourPunCallbacks
{
    #region Variables
    [SerializeField]
     int carteles;
    public static int dinero;
    ManagerMinijuegos managerMinijuegos;
    #endregion

    private void Awake()
    {
        managerMinijuegos = this.GetComponent<ManagerMinijuegos>();
    }

    void Start()
    {
        Minijuegos.m_cartel += Carteles;
        Minijuegos.m_clicks += Clicks;
    }

    void Carteles(int valor)
    {
        carteles += valor;
        

        if (carteles >= 3)
        {
            //carteles = 0;
            FinishTask();
        }
    }

    [PunRPC]
    public void ResetCarteles(int cartelesBaseValue)
    {
        Debug.Log("Se Reseteo o.0");
        carteles = cartelesBaseValue;
        Debug.Log(carteles);
    }

    void Clicks(int valor)
    {
        dinero += valor;
    }
    void FinishTask() 
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponentInParent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                player.GetComponentInParent<TEST_Movement>().enabled = true;
                if (player.GetComponentInParent<PlayerTeam>().TeamA) 
                {
                    player.GetComponentInParent<TEST_Interact>().speakingTo.Team1();
                }
               else if (player.GetComponentInParent<PlayerTeam>().TeamB)
                {
                    player.GetComponentInParent<TEST_Interact>().speakingTo.Team2();
                }
                managerMinijuegos.ReseteoDeCarteles(player.GetComponentInParent<TEST_Interact>().minigameID);
                player.GetComponentInParent<TEST_Interact>().objectToInteract.GetComponent<I_Interactable>().OnLeavePanel();
                player.GetComponentInParent<TEST_Interact>().thisTask.RPCdata();
            }
        }
    }
}
