using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Interactable
{
   void OnInteract(bool call);

    void OnLeavePanel(bool call);
    void OnFinishTask();

    void RPCdata();

}
