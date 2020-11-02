using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obj_Impresora : MonoBehaviour,I_Interactable
{
    public void OnInteract()
    {
        Debug.Log("Interacting");
    }

    public void OnLeavePanel()
    {
    }

    public void OnFinishTask() 
    { 
    }

    public void RPCdata() 
    {

    }
}
