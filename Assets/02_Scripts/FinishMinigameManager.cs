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
            // ---------------------  Construir ----------------------

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

            // SuperPc 1
            case 7:
                switch (GetComponentInParent<WhatTeamIsCalling>().team)
                {
                    case true:
                        GameObject.Find("Obj_SuperPc1A").GetComponent<BoxProgression>().CallFinish();
                        break;
                    case false:
                        GameObject.Find("Obj_SuperPc1B").GetComponent<BoxProgression>().CallFinish();
                        break;
                }
                break;

            // SuperPc 2
            case 8:
                switch (GetComponentInParent<WhatTeamIsCalling>().team)
                {
                    case true:
                        GameObject.Find("Obj_SuperPc2A").GetComponent<BoxProgression>().CallFinish();
                        break;
                    case false:
                        GameObject.Find("Obj_SuperPc2B").GetComponent<BoxProgression>().CallFinish();
                        break;
                }
                break;

            // SuperPc 3
            case 9:
                switch (GetComponentInParent<WhatTeamIsCalling>().team)
                {
                    case true:
                        GameObject.Find("Obj_SuperPc3A").GetComponent<BoxProgression>().CallFinish();
                        break;
                    case false:
                        GameObject.Find("Obj_SuperPc3B").GetComponent<BoxProgression>().CallFinish();
                        break;
                }
                break;

            // -------------------------  Producir ----------------------------

            // Cartel - Impresora
            case 11:
                LocalPlayerManager.paperCounter++;
                Debug.Log(LocalPlayerManager.paperCounter);
                switch (GetComponentInParent<WhatTeamIsCalling>().team)
                {
                    case true:
                        GameObject.Find("ImpresoraAObj").GetComponent<BoxProgression>().FinishedMinigame();
                        break;
                    case false:
                        GameObject.Find("ImpresoraBObj").GetComponent<BoxProgression>().FinishedMinigame();
                        break;
                }
                break;

            // Lightstick - Impre3D
            case 12:
                LocalPlayerManager.stickCounter++;
                Debug.Log(LocalPlayerManager.stickCounter);
                switch (GetComponentInParent<WhatTeamIsCalling>().team)
                {
                    case true:
                        GameObject.Find("Obj_Imp3dA").GetComponent<BoxProgression>().FinishedMinigame();
                        break;
                    case false:
                        GameObject.Find("Obj_Imp3dB").GetComponent<BoxProgression>().FinishedMinigame();
                        break;
                }
                break;

            // Chapita
            case 13:
                Debug.Log("Chapa Finish");
                LocalPlayerManager.chapaCounter++;
                Debug.Log(LocalPlayerManager.chapaCounter);
                switch (GetComponentInParent<WhatTeamIsCalling>().team)
                {
                    case true:
                        GameObject.Find("Obj_ChapitasA").GetComponent<BoxProgression>().FinishedMinigame();
                        break;
                    case false:
                        GameObject.Find("Obj_ChapitasB").GetComponent<BoxProgression>().FinishedMinigame();
                        break;
                }
                break;

            // Plotter
            case 14:
                LocalPlayerManager.paperCounter++;
                Debug.Log(LocalPlayerManager.paperCounter);
                switch (GetComponentInParent<WhatTeamIsCalling>().team)
                {
                    case true:
                        GameObject.Find("Obj_PlotA").GetComponent<BoxProgression>().FinishedMinigame();
                        break;
                    case false:
                        GameObject.Find("Obj_PlotB").GetComponent<BoxProgression>().FinishedMinigame();
                        break;
                }
                break;

            // Merch
            case 15:
                LocalPlayerManager.chapaCounter++;
                Debug.Log(LocalPlayerManager.paperCounter);
                switch (GetComponentInParent<WhatTeamIsCalling>().team)
                {
                    case true:
                        GameObject.Find("Obj_MerchA").GetComponent<BoxProgression>().FinishedMinigame();
                        break;
                    case false:
                        GameObject.Find("Obj_MerchB").GetComponent<BoxProgression>().FinishedMinigame();
                        break;
                }
                break;

            // Impresora Indust
            case 16:
                LocalPlayerManager.stickCounter++;
                Debug.Log(LocalPlayerManager.paperCounter);
                switch (GetComponentInParent<WhatTeamIsCalling>().team)
                {
                    case true:
                        GameObject.Find("Obj_ImpIndusA").GetComponent<BoxProgression>().FinishedMinigame();
                        break;
                    case false:
                        GameObject.Find("Obj_ImpIndusB").GetComponent<BoxProgression>().FinishedMinigame();
                        break;
                }
                Debug.Log("finished");
                break;



            // SuperPc
            case 18:
                Debug.Log("Super Pc Procesando");
                break;
        }
    }
}
