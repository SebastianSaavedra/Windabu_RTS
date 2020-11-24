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
        IniciarAct();
    }

    void IniciarAct()
    {
        speed = initialSpeed;
        moving = true;
    }

    void ReiniciarActividadMasRapido()
    {
        if (intentos >= 6)
        {
            managerMinigame.FinishTask();
        }
        else
        {
            transform.position = punto0.transform.position;
            if (speed <= maxSpeed)
            {
                speed += speed / Random.Range(1.8f, 2);
                if (speed >= maxSpeed)
                {
                    speed = maxSpeed;
                }
            }
            Debug.Log("La velocidad del dildo es: " + speed);
            moving = true;
        }
    }

    void ReiniciarActividadPerdio()
    {
        if (intentos >= 6)
        {
            managerMinigame.FinishTask();
        }
        else
        {
            transform.position = punto0.transform.position;
            moving = true;
        }
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
            ReiniciarActividadMasRapido();
        }
        else
        {
            Debug.Log("Ksi");

            yield return new WaitForSeconds(.75f);

            DecirleAMasterClienteQueHiceUnCambio();
            ReiniciarActividadPerdio();
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