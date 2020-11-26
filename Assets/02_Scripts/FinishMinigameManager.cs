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
        if(slotCounter >= slotAmount)
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
                break;

            // Impre3D
            case 2:
                break;

            // Maquina Chapitas
            case 3:
                break;

            // Super Pc
            case 4:
                break;

            // -----------  Producir -------------

            // Cartel - Impresora
            case 11:
                break;

            // Lightstick - Impre3D
            case 12:
                break;

            // Merchandise - Chapita
            case 13:
                Debug.Log("Chapita producida");
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
