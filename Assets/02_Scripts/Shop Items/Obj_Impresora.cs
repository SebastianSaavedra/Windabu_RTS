using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obj_Impresora : MonoBehaviour,I_Interactable
{
    public void OnInteract(bool call)
    {
        Debug.Log("Interacting");
    }

    public void OnLeavePanel(bool call)
    {
    }

    public void OnFinishTask() 
    { 
    }

    public void RPCdata() 
    {

    }
}
