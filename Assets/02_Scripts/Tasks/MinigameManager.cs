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
    int carteles;
    public static int dinero;
    #endregion

    void Start()
    {
        Minijuegos.m_cartel += Carteles;
        Minijuegos.m_clicks += Clicks;
    }

    void Carteles(int valor)
    {
        carteles += valor;
        Debug.Log("Numero de carteles: " + carteles);

        if (carteles >= 3)
        {
            carteles = 0;
            FinishTask();
        }
    }

    [PunRPC]
    public void ResetCarteles()
    {
        carteles = 0;
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
                player.GetComponentInParent<TEST_Interact>().objectToInteract.GetComponent<I_Interactable>().OnLeavePanel();
                player.GetComponentInParent<TEST_Interact>().thisTask.RPCdata();
            }
        }
    }
}
