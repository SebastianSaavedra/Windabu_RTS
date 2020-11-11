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

    private void Update()
    {
        if (!objectToInteract) return;
        if (photonView.IsMine) 
        {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectToInteract.GetComponent<I_Interactable>()!=null && objectToInteract.GetComponent<TaskDropDown>().canInteract)
            {
                objectToInteract.GetComponent<I_Interactable>().OnInteract();
                    GetComponent<TEST_Movement>().enabled = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            objectToInteract.GetComponent<I_Interactable>().OnLeavePanel();
                GetComponent<TEST_Movement>().enabled = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.IsMine) 
        {
        objectToInteract = collision.gameObject;
            speakingTo = collision.GetComponentInParent<CPManager>();
            thisTask = collision.GetComponent<TaskDropDown>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (photonView.IsMine) 
        {
        objectToInteract.GetComponent<I_Interactable>().OnLeavePanel();
            objectToInteract = null;
            speakingTo = null;
            thisTask = null;
        }
    }

}
