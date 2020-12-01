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
                switch (GetComponentInParent<WhatTeamIsCalling>().team) 
                {
                    case true:
                        GameObject.Find("ImpresoraAObj").GetComponent<BoxProgression>().CallFinish();
                        break;
                    case false:
                        GameObject.Find("ImpresoraBObj").GetComponent<BoxProgression>().CallFinish();
                        break;
                }
                break;

            // Impre3D
            case 2:
                switch (GetComponentInParent<WhatTeamIsCalling>().team)
                {
                    case true:
                        GameObject.Find("Obj_Imp3dA").GetComponent<BoxProgression>().CallFinish();
                        break;
                    case false:
                        GameObject.Find("Obj_Imp3dB").GetComponent<BoxProgression>().CallFinish();
                        break;
                }
                break;

            // Maquina Chapitas
            case 3:
                switch (GetComponentInParent<WhatTeamIsCalling>().team)
                {
                    case true:
                        GameObject.Find("Obj_ChapitasA").GetComponent<BoxProgression>().CallFinish();
                        break;
                    case false:
                        GameObject.Find("Obj_ChapitasB").GetComponent<BoxProgression>().CallFinish();
                        break;
                }
                break;

            // Plotter
            case 4:
                switch (GetComponentInParent<WhatTeamIsCalling>().team)
                {
                    case true:
                        GameObject.Find("Obj_PlotA").GetComponent<BoxProgression>().CallFinish();
                        break;
                    case false:
                        GameObject.Find("Obj_PlotB").GetComponent<BoxProgression>().CallFinish();
                        break;
                }
                break;

            // Merch
            case 5:
                switch (GetComponentInParent<WhatTeamIsCalling>().team)
                {
                    case true:
                        GameObject.Find("Obj_MerchA").GetComponent<BoxProgression>().CallFinish();
                        break;
                    case false:
                        GameObject.Find("Obj_MerchB").GetComponent<BoxProgression>().CallFinish();
                        break;
                }
                break;

            // Impresora industrial
            case 6:
                switch (GetComponentInParent<WhatTeamIsCalling>().team)
                {
                    case true:
                        GameObject.Find("Obj_ImpIndusA").GetComponent<BoxProgression>().CallFinish();
                        break;
                    case false:
                        GameObject.Find("Obj_ImpIndusB").GetComponent<BoxProgression>().CallFinish();
                        break;
                }
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

            case 14:
                LocalPlayerManager.paperCounter++;
                Debug.Log(LocalPlayerManager.paperCounter);
                break;
        }
    }
}
