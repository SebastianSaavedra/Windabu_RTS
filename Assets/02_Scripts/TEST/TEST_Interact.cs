using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Com.MaluCompany.TestGame;

public class TEST_Interact : MonoBehaviourPunCallbacks
{
    public GameObject objectToInteract;
    public CPManager speakingTo;
    public TaskDropDown thisTask;
    public bool alreadyInteracted;
    public int minigameID;

    private void Update()
    {
        if (!objectToInteract) return;
        if (photonView.IsMine) 
        {
        
        if (Input.GetKeyDown(KeyCode.E) && !alreadyInteracted)
        {
            if (objectToInteract.GetComponent<I_Interactable>()!=null && objectToInteract.GetComponent<TaskDropDown>().canInteract)
            {
                 minigameID = (int)objectToInteract.GetComponent<TaskDropDown>().thisMinigame;
                objectToInteract.GetComponent<I_Interactable>().OnInteract(GetComponent<PlayerTeam>().team);
                    GetComponent<TEST_Movement>().enabled = false;
                    alreadyInteracted = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            objectToInteract.GetComponent<I_Interactable>().OnLeavePanel(GetComponent<PlayerTeam>().team);
                GetComponent<TEST_Movement>().enabled = true;
                alreadyInteracted = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.IsMine) 
        {
            if (collision.GetComponent<TaskDropDown>()) 
            {
        objectToInteract = collision.gameObject;
            }
            speakingTo = collision.GetComponentInParent<CPManager>();
            thisTask = collision.GetComponent<TaskDropDown>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (photonView.IsMine) 
        {
            objectToInteract.GetComponent<I_Interactable>().OnLeavePanel(GetComponent<PlayerTeam>().team);
            objectToInteract = null;
            speakingTo = null;
            thisTask = null;
            alreadyInteracted = false;
        }
    }

}
