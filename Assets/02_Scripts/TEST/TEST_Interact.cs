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
    public bool alreadyChanged;

    private void Update()
    {
        if (!objectToInteract) return;
        if (photonView.IsMine) 
        {

    
        if (Input.GetKeyDown(KeyCode.E) && !alreadyInteracted)
        {
            if (objectToInteract.GetComponent<I_Interactable>()!=null && objectToInteract.GetComponent<TaskDropDown>().canInteract)
            {
                    if (objectToInteract.GetComponent<TaskDropDown>().minijuegoData.numeroDeJugadores==0)
                    {                
                minigameID = (int)objectToInteract.GetComponent<TaskDropDown>().thisMinigame;
                objectToInteract.GetComponent<I_Interactable>().OnInteract(GetComponent<PlayerTeam>().team);
                 GetComponent<TEST_Movement>().enabled = false;
                 alreadyInteracted = true;
                    }
                    else 
                    {
                        minigameID = (int)objectToInteract.GetComponent<TaskDropDown>().thisMinigame;
                        objectToInteract.GetComponent<I_Interactable>().OnInteract(GetComponent<PlayerTeam>().team);
                        GetComponent<TEST_Movement>().enabled = false;
                        GameObject.Find("MinijuegosManager").GetComponent<ManagerMinijuegos>().CambiarInteractable(GetComponentInParent<PlayerId>().id,minigameID);
                        alreadyChanged = true;
                        alreadyInteracted = true;
                    }

            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            objectToInteract.GetComponent<I_Interactable>().OnLeavePanel(GetComponent<PlayerTeam>().team);
                GetComponent<TEST_Movement>().enabled = true;
                alreadyInteracted = false;
                alreadyChanged = false;
                GameObject.Find("MinijuegosManager").GetComponent<ManagerMinijuegos>().ResetearMuchosValores(minigameID);
            }
        }
        if (objectToInteract.GetComponent<TaskDropDown>().minijuegoData.completado) 
        {
            objectToInteract.GetComponent<I_Interactable>().OnLeavePanel(GetComponent<PlayerTeam>().team);
                GetComponent<TEST_Movement>().enabled = true;
        }
                if(objectToInteract.GetComponent<TaskDropDown>().minijuegoData.numeroDeJugadores == 2 && !alreadyChanged) 
                {
            GameObject.Find("MinijuegosManager").GetComponent<ManagerMinijuegos>().CambiarInteractable(GetComponentInParent<PlayerId>().id,minigameID);
                alreadyChanged = true;
                }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.IsMine) 
        {
            if (collision.GetComponent<TaskDropDown>()) 
            {
            objectToInteract = collision.gameObject;
            speakingTo = collision.GetComponentInParent<CPManager>();
            thisTask = collision.GetComponent<TaskDropDown>();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (photonView.IsMine) 
        {
            if (collision.GetComponent<TaskDropDown>()) 
            {
            objectToInteract.GetComponent<I_Interactable>().OnLeavePanel(GetComponent<PlayerTeam>().team);
            objectToInteract = null;
            speakingTo = null;
            thisTask = null;
             alreadyChanged = false;
            alreadyInteracted = false;
            }
        }
    }

}
