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

        if (objectToInteract.GetComponent<TaskDropDown>().minijuegoData.completado) 
        {
                objectToInteract.GetComponent<I_Interactable>().OnLeavePanel(GetComponent<PlayerTeam>().team);
                Debug.Log(transform.name + " termino el versus");
            GetComponent<TEST_Movement>().enabled = true;
        }
            
        if (Input.GetKeyDown(KeyCode.E) && !alreadyInteracted)
        {

                #region MinijuegoBruh
                if (objectToInteract.GetComponent<I_Interactable>()!=null && objectToInteract.GetComponent<TaskDropDown>().canInteract)
            {
                    switch (objectToInteract.GetComponent<TaskDropDown>().minijuegoData.numeroDeJugadores) 
                    {
                        case 0:
                            switch (objectToInteract.GetComponent<TaskDropDown>().thisMinigameis) 
                            {
                                case 0:
                                    if (LocalPlayerManager.paperCounter > 0)
                                    {
                                        callSingle();
                                        LocalPlayerManager.paperCounter--;
                                    }
                                    break;
                                case 1:
                                    if (LocalPlayerManager.paperCounter > 0)
                                    {
                                        callSingle();
                                        LocalPlayerManager.paperCounter--;
                                    }
                                    break;
                                default:
                                    callSingle();
                                    break;
                            }
                            break;
                        case 1:
                            switch (objectToInteract.GetComponent<TaskDropDown>().thisMinigameis)
                            {
                                case 0:
                                    if (LocalPlayerManager.paperCounter > 0)
                                    {
                                        callMP();
                                    }
                                    break;
                                case 1:
                                    if (LocalPlayerManager.paperCounter > 0)
                                    {
                                        callMP();
                                    }
                                    break;
                                default:
                                   callMP();
                                    break;
                            }
                            break;
                        case 2:
                            Debug.Log("No puedes entrar");
                            break;
                    }
            }
                #endregion
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
                if(objectToInteract.GetComponent<TaskDropDown>().minijuegoData.numeroDeJugadores == 2 && !alreadyChanged) 
                {
            GameObject.Find("MinijuegosManager").GetComponent<ManagerMinijuegos>().CambiarInteractable(GetComponentInParent<PlayerId>().id,minigameID);
                alreadyChanged = true;
                }
    }

    public void callSingle() 
    {
        minigameID = (int)objectToInteract.GetComponent<TaskDropDown>().thisMinigame;
        objectToInteract.GetComponent<I_Interactable>().OnInteract(GetComponent<PlayerTeam>().team);
        GetComponent<TEST_Movement>().enabled = false;
        alreadyInteracted = true;
    }
    public void callMP()
    {
        minigameID = (int)objectToInteract.GetComponent<TaskDropDown>().thisMinigame;
        objectToInteract.GetComponent<I_Interactable>().OnInteract(GetComponent<PlayerTeam>().team);
        GetComponent<TEST_Movement>().enabled = false;
        GameObject.Find("MinijuegosManager").GetComponent<ManagerMinijuegos>().CambiarInteractable(GetComponentInParent<PlayerId>().id, minigameID);
        alreadyChanged = true;
        alreadyInteracted = true;

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
            objectToInteract = null;
            speakingTo = null;
            thisTask = null;
             alreadyChanged = false;
            alreadyInteracted = false;
            objectToInteract.GetComponent<I_Interactable>().OnLeavePanel(GetComponent<PlayerTeam>().team);
            }
        }
    }

}
