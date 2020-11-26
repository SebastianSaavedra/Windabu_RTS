using Com.MaluCompany.TestGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using Photon.Pun;
using Photon.Realtime;

public class ArcadeMinigame : MonoBehaviourPunCallbacks
{
    // Refs
    MinigameManager managerMinigame;
    ManagerMinijuegos managerMinijuegos;
    GameObject originPanel;
    RPCManager RPCManager;
    //[SerializeField] Animator animVs;
    //bool vs;
    bool inicio;

    //Barras
    public Image barraA;
    public Image barraB;

    //Valores del minijuego
    public int mashLimit = 32;
    [SerializeField] int mashScore;
    private bool leftActive;

    // Pantallas que se muestran
    public Text uiMashCounter;
    public GameObject p1, p1z, p1x, p2;
    public GameObject[] screens;
    int screenCounter;

    private void Start()
    {
        uiMashCounter = GetComponent<Text>();
        leftActive = true;
        managerMinijuegos = GameObject.Find("MinijuegosManager").GetComponent<ManagerMinijuegos>();
        managerMinigame = GameObject.Find("MinijuegosManager").GetComponent<MinigameManager>();
    }
    private new void OnEnable()
    {
        originPanel = GameObject.Find("OriginPanel");
        RPCManager = GameObject.Find("MiniJuego1_1(cartel)").GetComponent<RPCManager>();
        uiMashCounter = GetComponent<Text>();
        leftActive = true;
        mashScore = 0;
        StartCoroutine("IniciarAct");
    }

    IEnumerator IniciarAct()
    {
        yield return new WaitForSeconds(.5f);
        inicio = true;
        yield break;
    }

    void ActualizarBarra()
    {
        if (managerMinijuegos.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].numeroDeJugadores == 2)
        {
            //if (!vs)
            //{
            //    //animVs.SetTrigger("");
            //    //vs = true;
            //}
            barraA.fillAmount = (float)managerMinijuegos.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA / mashLimit;
            barraB.fillAmount = (float)managerMinijuegos.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB / mashLimit;
        }
    }

    private void Update()
    {
        if (originPanel.GetComponent<WhatTeamIsCalling>().team == true)     //TEAM A
        {
            if (inicio)
            {
                if (Input.GetKeyDown(KeyCode.Z) && leftActive)
                {
                    leftActive = false;
                    mashScore++;
                    RPCManager.RPCActualizarDatosA(originPanel.GetComponent<WhatTeamIsCalling>().id, mashScore);
                    barraA.fillAmount = (float)managerMinijuegos.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA / mashLimit;
                    uiMashCounter.text = mashScore.ToString("0");

                    // UI
                    p1.SetActive(false);
                    p1z.SetActive(true);
                    p1x.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.X) && !leftActive)
                {
                    leftActive = true;
                    mashScore++;
                    RPCManager.RPCActualizarDatosA(originPanel.GetComponent<WhatTeamIsCalling>().id, mashScore);
                    barraA.fillAmount = (float)managerMinijuegos.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA / mashLimit;
                    uiMashCounter.text = mashScore.ToString("0");

                    #region UI
                    p1z.SetActive(false);
                    p1x.SetActive(true);

                    if (screenCounter >= screens.Length - 1)
                    {
                        screenCounter = 0;
                    }
                    else
                    {
                        screens[screenCounter].gameObject.SetActive(false);
                        screenCounter += 1;
                        screens[screenCounter].gameObject.SetActive(true);
                    }
                    #endregion
                }

                ActualizarBarra();
                // Win con //managerMinijuegos.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA
                if (mashScore >= mashLimit)
                {
                    // UI
                    p1.SetActive(true);
                    p1z.SetActive(false);
                    p1x.SetActive(false);

                    Debug.Log("Win");
                    mashScore = 0;
                    uiMashCounter.text = mashScore.ToString("0");
                    gameObject.SetActive(false);
                    managerMinigame.FinishTask();
                }

            }
        }

        else if (originPanel.GetComponent<WhatTeamIsCalling>().team == false)       //TEAM B
        {
            if (inicio)
            {
                if (Input.GetKeyDown(KeyCode.Z) && leftActive)
                {
                    leftActive = false;
                    mashScore++;
                    RPCManager.RPCActualizarDatosB(originPanel.GetComponent<WhatTeamIsCalling>().id, mashScore);
                    barraB.fillAmount = (float)managerMinijuegos.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB / mashLimit;
                    uiMashCounter.text = mashScore.ToString("0");

                    // UI
                    p1.SetActive(false);
                    p1z.SetActive(true);
                    p1x.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.X) && !leftActive)
                {
                    leftActive = true;
                    mashScore++;
                    RPCManager.RPCActualizarDatosB(originPanel.GetComponent<WhatTeamIsCalling>().id, mashScore);
                    barraB.fillAmount = (float)managerMinijuegos.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB / mashLimit;
                    uiMashCounter.text = mashScore.ToString("0");

                    #region UI
                    p1z.SetActive(false);
                    p1x.SetActive(true);

                    if (screenCounter >= screens.Length - 1)
                    {
                        screenCounter = 0;
                    }
                    else
                    {
                        screens[screenCounter].gameObject.SetActive(false);
                        screenCounter += 1;
                        screens[screenCounter].gameObject.SetActive(true);
                    }
                    #endregion
                }

                ActualizarBarra();
                // Win con //managerMinijuegos.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB
                if (mashScore >= mashLimit)
                {
                    // UI
                    p1.SetActive(true);
                    p1z.SetActive(false);
                    p1x.SetActive(false);

                    Debug.Log("Win");
                    mashScore = 0;
                    uiMashCounter.text = mashScore.ToString("0");
                    gameObject.SetActive(false);
                    managerMinigame.FinishTask();
                }

            }
        }
    }
}
