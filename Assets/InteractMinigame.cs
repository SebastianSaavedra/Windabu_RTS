﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Com.MaluCompany.TestGame;

public class InteractMinigame : MonoBehaviourPunCallbacks
{
    public GameObject objectToInteract;
    public CPManager speakingTo;
    public TaskDropDown thisTask;
    public bool alreadyInteracted;
    public int minigameID;
    public bool alreadyChanged;
    [SerializeField] bool team;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine) 
        {
        if (!objectToInteract) return;
        if (Input.GetKeyDown(KeyCode.E) && !alreadyInteracted)
        {
            if (objectToInteract.GetComponent<I_Interactable>() != null && objectToInteract.GetComponent<TaskDropDownMinigame>().canInteract && !objectToInteract.GetComponent<TaskDropDownMinigame>().stopoutsiders)
        {
                    if (objectToInteract.GetComponent<TaskDropDownMinigame>().Interactonce) 
                    {
                        Interact(team);
                    }
                    else 
                    {
                        Interact(team);
                    }
        }
    }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
                if (!objectToInteract.GetComponent<TaskDropDownMinigame>().cantClosePanel)
                {
                    Debug.Log("Cagaste hermano no podi cerrar el panel");
                if (!objectToInteract.GetComponent<TaskDropDownMinigame>().stopoutsiders)
                {
                    objectToInteract.GetComponent<I_Interactable>().OnLeavePanel(true);
                    objectToInteract.GetComponent<TaskDropDownMinigame>().StopOther(false);
                    GetComponent<TEST_Movement>().enabled = true;
                    alreadyInteracted = false;
                }
                }
        }
        }
    }

    public void Interact(bool team) 
    {
        switch (team) 
        {
            case true:
                if (MinigameManager.dineroA >= objectToInteract.GetComponent<TaskDropDownMinigame>().moneyToentry)
                {
                        Minijuegos.compraA((int)objectToInteract.GetComponent<TaskDropDownMinigame>().moneyToentry);
                    GetComponent<TEST_Movement>().enabled = false;
                    // objectToInteract.GetComponent<I_Interactable>().OnInteract(true);
                    objectToInteract.GetComponent<I_Interactable>().OnInteract(GetComponent<PlayerTeam>().team);
                        objectToInteract.GetComponent<TaskDropDownMinigame>().StopOther(true);
                    alreadyInteracted = true;
                }
                break;

            case false:
                if (MinigameManager.dineroB >= objectToInteract.GetComponent<TaskDropDownMinigame>().moneyToentry)
                {
                        Minijuegos.compraB((int)objectToInteract.GetComponent<TaskDropDownMinigame>().moneyToentry);
                    GetComponent<TEST_Movement>().enabled = false;
                    // objectToInteract.GetComponent<I_Interactable>().OnInteract(true);
                    objectToInteract.GetComponent<I_Interactable>().OnInteract(GetComponent<PlayerTeam>().team);
                        objectToInteract.GetComponent<TaskDropDownMinigame>().StopOther(true);
                    alreadyInteracted = true;
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.IsMine)
        {
            if (collision.GetComponent<TaskDropDownMinigame>())
        {
            objectToInteract = collision.gameObject;
        }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (photonView.IsMine) 
        {
            if (collision.GetComponent<TaskDropDownMinigame>())
        {
            alreadyInteracted = false;
            objectToInteract = null;
            objectToInteract.GetComponent<I_Interactable>().OnLeavePanel(GetComponent<PlayerTeam>().team);
            }
        }
        }
 
}
