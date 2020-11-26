using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class BarMovement : MonoBehaviourPunCallbacks
{
    //Privadas
    ManagerMinijuegos managerLocal;
    MinigameManager managerMinigame;
    [SerializeField] RPCManager RPCManager;
    [SerializeField] GameObject originPanel;

    Coroutine cor;
    float initialSpeed;
    //Refs
    public GameObject punto0, punto1;

    public bool scored;
    public bool fallo;
    int intentos;   // para la lista
    int cantidadDeAciertos;
    int cantidadDeFallos;

    //Listas
    [SerializeField] GameObject[] aciertos;
    [SerializeField] GameObject[] fallos;

    [SerializeField] GameObject[] aciertosP2;
    [SerializeField] GameObject[] fallosP2;


    [Space(10)]
    public float speed;
    public bool moving;
    [SerializeField] int maxSpeed;

    private void Awake()
    {
        initialSpeed = speed;
    }
    private void Start()
    {
        managerMinigame = GameObject.Find("MinijuegosManager").GetComponent<MinigameManager>();
        managerLocal = GameObject.Find("MinijuegosManager").GetComponent<ManagerMinijuegos>();
        RPCManager = GameObject.Find("MiniJuego1_1(cartel)").GetComponent<RPCManager>();
    }

    private new void OnEnable()
    {
        intentos = 0;
        cantidadDeFallos = 0;
        cantidadDeAciertos = 0;
        originPanel = GameObject.Find("OriginPanel");
        StartCoroutine("IniciarAct");
    }

    IEnumerator IniciarAct()
    {
        yield return new WaitForSeconds(.5f);
        speed = initialSpeed;
        moving = true;
        yield break;
    }

    void ReiniciarActividadAcerto()             //Serializar los intentos/Gameobjects????
    {
        fallo = false;
        //If player 1 acerto 
        if (originPanel.GetComponent<WhatTeamIsCalling>().team == true)
        {
            if (intentos != aciertos.Length)
            {
                aciertos[intentos].SetActive(true);
                Debug.Log("Player A acerto una vez");
                cantidadDeAciertos++;
                RPCManager.RPCActualizarDatosA(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeAciertos);
                RPCManager.RPCActualizarFalloLighstickA(originPanel.GetComponent<WhatTeamIsCalling>().id, fallo);
                //managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA++;
            }
        }
        //If player 2 acerto
        else if (originPanel.GetComponent<WhatTeamIsCalling>().team == false)
        {
            if (intentos != aciertosP2.Length)
            {
                aciertosP2[intentos].SetActive(true);
                Debug.Log("Player B acerto una vez");
                cantidadDeAciertos++;
                RPCManager.RPCActualizarDatosB(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeAciertos);
                RPCManager.RPCActualizarFalloLighstickB(originPanel.GetComponent<WhatTeamIsCalling>().id, fallo);
                //managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB++;
            }
        }

        intentos++;
        Debug.Log("Intentos: " + intentos);

        transform.position = punto0.transform.position;
        if (speed <= maxSpeed)
        {
            speed += speed / Random.Range(1.8f, 2);
            if (speed >= maxSpeed)
            {
                speed = maxSpeed;
            }
        }
        Debug.Log("La velocidad del lighstick es: " + speed);
        moving = true;
    }

    void ReiniciarActividadFallo()
    {
        fallo = true;
        //If player 1 acerto 
        if (originPanel.GetComponent<WhatTeamIsCalling>().team == true)
        {
            if (intentos != fallos.Length)
            {
                fallos[intentos].SetActive(true);
                Debug.Log("Player A fallo una vez");
                cantidadDeFallos++;
                RPCManager.RPCActualizarDatosFallosLightstickA(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeFallos);
                RPCManager.RPCActualizarFalloLighstickA(originPanel.GetComponent<WhatTeamIsCalling>().id, fallo);
            }
        }
        //If player 2 acerto
        else if (originPanel.GetComponent<WhatTeamIsCalling>().team == false)
        {
            if (intentos != fallosP2.Length)
            {
                fallosP2[intentos].SetActive(true);
                Debug.Log("Player B fallo una vez");
                cantidadDeFallos++;
                RPCManager.RPCActualizarDatosFallosLightstickB(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeFallos);
                RPCManager.RPCActualizarFalloLighstickB(originPanel.GetComponent<WhatTeamIsCalling>().id, fallo);
            }
        }

        intentos++;
        Debug.Log("Intentos: " + intentos);

        transform.position = punto0.transform.position;
        moving = true;
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, punto1.transform.position, speed * Time.deltaTime);

            if(transform.position == punto1.transform.position)
            {
                transform.position = punto0.transform.position;
            }
        }
    }

    private void Update()
    {
        if (managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].numeroDeJugadores == 2)
        {
            if (originPanel.GetComponent<WhatTeamIsCalling>().team == true)
            {
                if (managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].falloLighstickB == true)
                {
                    fallosP2[managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].cantidadDeFallosLightstickB].SetActive(true);
                }

                else
                {
                    aciertosP2[managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB].SetActive(true);
                }
            }
            else if (originPanel.GetComponent<WhatTeamIsCalling>().team == false)
            {
                if (managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].falloLighstickA == true)
                {
                    fallos[managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].cantidadDeFallosLightstickA].SetActive(true);
                }

                else
                {
                    aciertos[managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA].SetActive(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && moving && managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].numeroDeJugadores < 2)
        {
            cor = StartCoroutine("StoppedLocal");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && moving && managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].numeroDeJugadores == 2)
        {
            StopCoroutine("StoppedLocal");
            cor = StartCoroutine("StoppedVS");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        scored = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        scored = false;
    }


    IEnumerator StoppedLocal()
    {
        moving = false;
        yield return new WaitForSeconds(0.1f);

        if (scored)         // Si esque logro achuntar al collider
        {
            Debug.Log("Wena");

            yield return new WaitForSeconds(.75f);

            DecirleAMasterClienteQueHiceUnCambio();
            Debug.Log("Intentos: " + intentos);

            if (intentos > 4)
            {
                if (cantidadDeAciertos > cantidadDeFallos)
                {
                    intentos = 0;
                    cantidadDeFallos = 0;
                    cantidadDeAciertos = 0;
                    managerMinigame.FinishTask();
                }
                else
                {
                    intentos = 0;
                    cantidadDeFallos = 0;
                    cantidadDeAciertos = 0;
                    Debug.Log("Perdio");
                    //OnLosePanel;
                }

            }
            else
            {
                ReiniciarActividadAcerto();
            }
        }

        else            // Si esque fallo al achuntarle al collider
        {
            Debug.Log("Ksi");

            yield return new WaitForSeconds(.75f);

            DecirleAMasterClienteQueHiceUnCambio();
            if (intentos > 4)
            {
                if (cantidadDeAciertos > cantidadDeFallos)
                {
                    intentos = 0;
                    cantidadDeFallos = 0;
                    cantidadDeAciertos = 0;
                    managerMinigame.FinishTask();
                }
                else
                {
                    intentos = 0;
                    cantidadDeFallos = 0;
                    cantidadDeAciertos = 0;
                    Debug.Log("Perdio");
                    //OnLosePanel;

                }
            }
            else
            {
                ReiniciarActividadFallo();
            }
        }
    }


    IEnumerator StoppedVS()
    {
        moving = false;
        yield return new WaitForSeconds(0.1f);

        if (scored)              // Si esque logro achuntar al collider
        {
            Debug.Log("Wena");

            yield return new WaitForSeconds(.75f);

            DecirleAMasterClienteQueHiceUnCambio();
            Debug.Log("Intentos: " + intentos);

            if (intentos > 4)
            {
                if (originPanel.GetComponent<WhatTeamIsCalling>().team == true)
                {
                    //managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].rondaA++;
                    RPCManager.RPCActualizarDatosRondaA(originPanel.GetComponent<WhatTeamIsCalling>().id, 1);
                    moving = false;
                    transform.position = punto0.transform.position;

                    yield return new WaitUntil(() => managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].rondaA ==
                    managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].rondaB);

                    if (managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA > 
                        managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB)
                    {
                        //Victoria Team A
                        intentos = 0;
                        cantidadDeFallos = 0;
                        cantidadDeAciertos = 0;
                        managerMinigame.FinishTask();
                    }

                    else if (managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA ==
                            managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB)
                    {
                        // Empate
                        //managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA = 0;
                        //managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB = 0;
                        cantidadDeAciertos = 0;
                        cantidadDeFallos = 0;
                        intentos = 0;
                        fallo = false;
                        RPCManager.RPCActualizarDatosA(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeAciertos);
                        RPCManager.RPCActualizarDatosB(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeAciertos);
                        RPCManager.RPCActualizarDatosFallosLightstickA(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeFallos);
                        RPCManager.RPCActualizarDatosFallosLightstickB(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeFallos);
                        RPCManager.RPCActualizarFalloLighstickA(originPanel.GetComponent<WhatTeamIsCalling>().id, fallo);
                        RPCManager.RPCActualizarFalloLighstickB(originPanel.GetComponent<WhatTeamIsCalling>().id, fallo);
                        speed = initialSpeed;

                    }

                    else
                    {
                        //Derrota Team A
                        intentos = 0;
                        cantidadDeFallos = 0;
                        cantidadDeAciertos = 0;
                        Debug.Log("Perdio");
                        //OnLosePanel;
                    }
                }

                else if (originPanel.GetComponent<WhatTeamIsCalling>().team == false)
                {
                    //managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].rondaB++;
                    RPCManager.RPCActualizarDatosRondaB(originPanel.GetComponent<WhatTeamIsCalling>().id, 1);
                    moving = false;
                    transform.position = punto0.transform.position;

                    yield return new WaitUntil(() => managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].rondaA ==
                    managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].rondaB);

                    if (managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB >
                        managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA)
                    {
                        //Victoria Team B
                        intentos = 0;
                        cantidadDeFallos = 0;
                        cantidadDeAciertos = 0;
                        managerMinigame.FinishTask();
                    }
                    else if (managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB ==
                        managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA)
                    {
                        // Empate
                        cantidadDeAciertos = 0;
                        cantidadDeFallos = 0;
                        intentos = 0;
                        fallo = false;
                        RPCManager.RPCActualizarDatosA(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeAciertos);
                        RPCManager.RPCActualizarDatosB(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeAciertos);
                        RPCManager.RPCActualizarDatosFallosLightstickA(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeFallos);
                        RPCManager.RPCActualizarDatosFallosLightstickB(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeFallos);
                        RPCManager.RPCActualizarFalloLighstickA(originPanel.GetComponent<WhatTeamIsCalling>().id, fallo);
                        RPCManager.RPCActualizarFalloLighstickB(originPanel.GetComponent<WhatTeamIsCalling>().id, fallo);
                        speed = initialSpeed;

                    }
                    else
                    {
                        //Derrota Team B
                        intentos = 0;
                        cantidadDeFallos = 0;
                        cantidadDeAciertos = 0;
                        Debug.Log("Perdio");
                        //OnLosePanel;
                    }
                }
            }
            else
            {
                ReiniciarActividadAcerto();
            }
        }


        else          // Si esque fallo al achuntarle al collider
        {
            Debug.Log("Ksi");

            yield return new WaitForSeconds(.75f);

            DecirleAMasterClienteQueHiceUnCambio();
            if (intentos > 4)
            {
                if (originPanel.GetComponent<WhatTeamIsCalling>().team == true)
                {
                    //managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].rondaA++;
                    RPCManager.RPCActualizarDatosRondaA(originPanel.GetComponent<WhatTeamIsCalling>().id, 1);
                    moving = false;
                    transform.position = punto0.transform.position;

                    yield return new WaitUntil(() => managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].rondaA ==
                    managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].rondaB);

                    if (managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA >
                        managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB)
                    {
                        //Victoria Team A
                        intentos = 0;
                        cantidadDeFallos = 0;
                        cantidadDeAciertos = 0;
                        managerMinigame.FinishTask();
                    }

                    else if (managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA ==
                            managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB)
                    {
                        // Empate
                        //managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA = 0;
                        //managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB = 0;
                        cantidadDeAciertos = 0;
                        cantidadDeFallos = 0;
                        intentos = 0;
                        fallo = false;
                        RPCManager.RPCActualizarDatosA(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeAciertos);
                        RPCManager.RPCActualizarDatosB(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeAciertos);
                        RPCManager.RPCActualizarDatosFallosLightstickA(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeFallos);
                        RPCManager.RPCActualizarDatosFallosLightstickB(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeFallos);
                        RPCManager.RPCActualizarFalloLighstickA(originPanel.GetComponent<WhatTeamIsCalling>().id, fallo);
                        RPCManager.RPCActualizarFalloLighstickB(originPanel.GetComponent<WhatTeamIsCalling>().id, fallo);
                        speed = initialSpeed;

                    }

                    else
                    {
                        //Derrota Team A
                        intentos = 0;
                        cantidadDeFallos = 0;
                        cantidadDeAciertos = 0;
                        Debug.Log("Perdio");
                        //OnLosePanel;
                    }
                }

                else if (originPanel.GetComponent<WhatTeamIsCalling>().team == false)
                {
                    //managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].rondaB++;
                    RPCManager.RPCActualizarDatosRondaB(originPanel.GetComponent<WhatTeamIsCalling>().id, 1);
                    moving = false;
                    transform.position = punto0.transform.position;

                    yield return new WaitUntil(() => managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].rondaA ==
                    managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].rondaB);

                    if (managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB >
                        managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA)
                    {
                        //Victoria Team B
                        intentos = 0;
                        cantidadDeFallos = 0;
                        cantidadDeAciertos = 0;
                        managerMinigame.FinishTask();
                    }
                    else if (managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB ==
                        managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA)
                    {
                        // Empate
                        //managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA = 0;
                        //managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB = 0;
                        cantidadDeAciertos = 0;
                        cantidadDeFallos = 0;
                        intentos = 0;
                        fallo = false;
                        RPCManager.RPCActualizarDatosA(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeAciertos);
                        RPCManager.RPCActualizarDatosB(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeAciertos);
                        RPCManager.RPCActualizarDatosFallosLightstickA(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeFallos);
                        RPCManager.RPCActualizarDatosFallosLightstickB(originPanel.GetComponent<WhatTeamIsCalling>().id, cantidadDeFallos);
                        RPCManager.RPCActualizarFalloLighstickA(originPanel.GetComponent<WhatTeamIsCalling>().id, fallo);
                        RPCManager.RPCActualizarFalloLighstickB(originPanel.GetComponent<WhatTeamIsCalling>().id, fallo);
                        speed = initialSpeed;

                    }
                    else
                    {
                        //Derrota Team B
                        intentos = 0;
                        cantidadDeFallos = 0;
                        cantidadDeAciertos = 0;
                        Debug.Log("Perdio");
                        //OnLosePanel;
                    }
                }
            }
            else
            {
                ReiniciarActividadFallo();
            }
        }
    }

    // Comparar valores, se puede hacer la wea local creo, total al que le llega el finish task gana
    void DecirleAMasterClienteQueHiceUnCambio()
    {
        managerLocal.ActualizarEstadoMinijuego1(PhotonNetwork.LocalPlayer.ActorNumber);
    }

    //Master Client me avisa que el otro jugador hizo un cambio
    public void ReciboActualizacionDeOtroJugador()
    {
        //cartelesJugador2++;
        //contadorJugador2.fillAmount = (float)cartelesJugador2 / 10;
        //contadorJugador2.text = cartelesJugador2.ToString();
        //Actualizar ui
    }

    private new void OnDisable()
    {
        speed = initialSpeed;
        moving = false;
    }
}