using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TaskDropDown : MonoBehaviour,I_Interactable
{
    [SerializeField] GameObject taskBarPanel;

    private void Start()
    {
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
}
