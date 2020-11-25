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
    [SerializeField] GameObject originPanel;

    Coroutine cor;
    float initialSpeed;
    //Refs
    public GameObject punto0, punto1;

    public bool scored;
    int intentos;   // para la lista
    [SerializeField] GameObject[] aciertos;
    [SerializeField] GameObject[] fallos;

    int intentosJugador2;
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
    }

    private new void OnEnable()
    {
        intentos = 0;
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
        //If player 1 acerto 
        if (originPanel.GetComponent<WhatTeamIsCalling>().team == true)
        {
            if (intentos != aciertos.Length)
            {
                Debug.Log("Player A acerto una vez");
                aciertos[intentos].SetActive(true);
            }
        }
        //If player 2 acerto
        else if (originPanel.GetComponent<WhatTeamIsCalling>().team == false)
        {
            if (intentos != aciertosP2.Length)
            {
                Debug.Log("Player B acerto una vez");
                aciertosP2[intentos].SetActive(true);
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
        //If player 1 acerto 
        if (originPanel.GetComponent<WhatTeamIsCalling>().team == true)
        {
            if (intentos != fallos.Length)
            {
                Debug.Log("Player A fallo una vez");
                fallos[intentos].SetActive(true);
            }
        }
        //If player 2 acerto
        else if (originPanel.GetComponent<WhatTeamIsCalling>().team == false)
        {
            if (intentos != fallosP2.Length)
            {
                Debug.Log("Player B fallo una vez");
                fallosP2[intentos].SetActive(true);
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
        if (Input.GetKeyDown(KeyCode.Space) && moving)
        {
            cor = StartCoroutine("Stopped");
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

    IEnumerator Stopped()
    {
        moving = false;
        yield return new WaitForSeconds(0.1f);

        if (scored)
        {
            Debug.Log("Wena");

            yield return new WaitForSeconds(.75f);

            DecirleAMasterClienteQueHiceUnCambio();
            Debug.Log("Intentos: " + intentos);

            if (intentos >= 5)
            {
                intentos = 0;
                managerMinigame.FinishTask();
            }
            else
            {
                ReiniciarActividadAcerto();
            }
        }
        else
        {
            Debug.Log("Ksi");

            yield return new WaitForSeconds(.75f);

            DecirleAMasterClienteQueHiceUnCambio();

            if (intentos >= 5)
            {
                intentos = 0;
                managerMinigame.FinishTask();
            }
            else
            {
                ReiniciarActividadFallo();
            }
        }
    }
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