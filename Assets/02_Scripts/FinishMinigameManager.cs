using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishMinigameManager : MonoBehaviour
{
    [SerializeField]
    private int finishedMinigame, slotAmount;

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
        switch (finishedMinigame)
        {
            // ----------  Construir ------------

            // Impresora
            case 1:
                break;

            // Impre3D
            case 2:
                break;

            // Maquina Chapitas
            case 3:
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
