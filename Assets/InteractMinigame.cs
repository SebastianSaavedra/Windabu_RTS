using System.Collections;
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
            if (objectToInteract.GetComponent<I_Interactable>() != null && objectToInteract.GetComponent<TaskDropDownMinigame>().canInteract)
        {
            GetComponent<TEST_Movement>().enabled = false;
               // objectToInteract.GetComponent<I_Interactable>().OnInteract(true);
                objectToInteract.GetComponent<I_Interactable>().OnInteract(GetComponent<PlayerTeam>().team);
                alreadyInteracted = true;
        }
    }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            objectToInteract.GetComponent<I_Interactable>().OnLeavePanel(true);
            GetComponent<TEST_Movement>().enabled = true;
            alreadyInteracted = false;
        }
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
                objectToInteract.GetComponent<I_Interactable>().OnLeavePanel(GetComponent<PlayerTeam>().team);
                objectToInteract = null;
            }
        }
    }
}
