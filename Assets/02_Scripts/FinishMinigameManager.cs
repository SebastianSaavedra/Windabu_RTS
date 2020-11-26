using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishMinigameManager : MonoBehaviour
{
    [SerializeField]
    private int finishedMinigameId, slotAmount;

    private int slotCounter;

    private void Start()
    {
        slotCounter = 0;
    }

    public void AddSlotCounter()
    {
        if(slotCounter >= slotAmount -1)
        {
            SlotsFilled();
        }
        else
        {
            slotCounter++;
        }
    }

    private void SlotsFilled()
    {
        switch (finishedMinigameId)
        {
            // ----------  Construir ------------

            // Impresora
            case 1:
                Debug.Log("Terminado");
                GetComponentInParent<WhatTeamIsCalling>().mjFinished = true;
                break;

            // Impre3D
            case 2:
                GetComponentInParent<WhatTeamIsCalling>().mjFinished = true;
                break;

            // Maquina Chapitas
            case 3:
                GetComponentInParent<WhatTeamIsCalling>().mjFinished = true;
                break;

            // Super Pc
            case 4:
                break;

            // -----------  Producir -------------

            // Cartel - Impresora
            case 11:
                LocalPlayerManager.paperCounter++;
                Debug.Log(LocalPlayerManager.paperCounter);
                break;

            // Lightstick - Impre3D
            case 12:
                LocalPlayerManager.stickCounter++;
                Debug.Log(LocalPlayerManager.stickCounter);
                break;

            // Merchandise - Chapita
            case 13:
                LocalPlayerManager.chapaCounter++;
                Debug.Log(LocalPlayerManager.chapaCounter);
                break;

            // ----------  Upgrade  ---------------

            // Impresora
            case 21:
                break;

            // Impres3D
            case 22:
                Debug.Log("Imp3d mejorada");
                break;

            // Chapa
            case 23:
                break;
        }
    }
}
