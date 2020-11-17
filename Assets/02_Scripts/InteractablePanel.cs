using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

public class InteractablePanel : MonoBehaviourPunCallbacks,I_Interactable
{

    public void OnInteract() 
    {

    }
    public void OnLeavePanel()
    {
        this.gameObject.SetActive(false);
    }

    [PunRPC]
    public void OnFinishTask()
    {

    }

    public void RPCdata()
    {

    }

    [PunRPC]
    IEnumerator BlockTask()
    {
        yield break;
    }

}
