using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxProgression : MonoBehaviour
{
    public WhatTeamIsCalling whatTeamIsCalling;
    public TaskDropDownMinigame taskDropDownMinigame;

    [HideInInspector]
    public bool inRoom;
    bool inLvl1;

    // Tras comprar - Listo para armar - Disponible para producir - Mejorado
    public GameObject born, ready, upgraded;

    private void Start()
    {
        inRoom = false;
        inLvl1 = false;

        born.SetActive(true);
        born.GetComponent<TaskDropDownMinigame>().enabled = false;

        ready.SetActive(false);
        upgraded.SetActive(false);
    }

    private void Update()
    {
        if (born.GetComponent<TaskDropDownMinigame>().taskBarPanel.GetComponent<WhatTeamIsCalling>().mjFinished)
        {
            whatTeamIsCalling = born.GetComponent<TaskDropDownMinigame>().taskBarPanel.GetComponent<WhatTeamIsCalling>();
            taskDropDownMinigame = born.GetComponent<TaskDropDownMinigame>();
            ready.SetActive(true);
            born.SetActive(false);
            taskDropDownMinigame.OnLeavePanel(true);
            whatTeamIsCalling.mjFinished = false;
        }
    }

    public void Traveling()
    {
        GetComponentInChildren<BoxCollider2D>().enabled = false;
    }

    public void Delivered()
    {
        inRoom = true;
        born.GetComponent<TaskDropDownMinigame>().enabled = true;
        born.GetComponent<TaskDropDownMinigame>().canInteract = true;
    }
}
