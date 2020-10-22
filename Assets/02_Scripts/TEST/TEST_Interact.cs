using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TEST_Interact : MonoBehaviourPunCallbacks
{
    public
    GameObject objectToInteract;

    private void Update()
    {
        if (!objectToInteract) return;
        if (photonView.IsMine) 
        {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectToInteract.GetComponent<I_Interactable>()!=null)
            {
                objectToInteract.GetComponent<I_Interactable>().OnInteract();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            objectToInteract.GetComponent<I_Interactable>().OnLeavePanel();
        }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.IsMine) 
        {
        objectToInteract = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (photonView.IsMine) 
        {
        objectToInteract.GetComponent<I_Interactable>().OnLeavePanel();
        objectToInteract = null;
        }
    }

}
