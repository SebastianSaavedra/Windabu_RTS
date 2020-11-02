using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;
using Photon.Pun;

public class TaskDropDown : MonoBehaviourPunCallbacks,I_Interactable
{
    [SerializeField] GameObject taskBarPanel;
    public bool canInteract;

    private void Start()
    {
        canInteract = true;
        if (taskBarPanel == null) 
        {

        }
    }

    public void OnInteract() 
    {
        Debug.Log("Hola");
        taskBarPanel.transform.DOMoveY(540,1);
    }
    public void OnLeavePanel()
    {
        taskBarPanel.transform.DOMoveY(1540, 1);
    }
   
    [PunRPC]
    public void OnFinishTask()
    {
        StartCoroutine(BlockTask());
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
