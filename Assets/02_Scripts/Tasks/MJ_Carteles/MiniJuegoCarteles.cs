using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


using Photon.Realtime;
using Photon.Pun;

public class MiniJuegoCarteles : MonoBehaviourPunCallbacks
{
    //No es necesariamente el cliente maestro;      // bool Team: True = A ó False = B
    ManagerMinijuegos managerLocal;
    MinigameManager managerMinigame;
    [SerializeField] RPCManager RPCManager;
    [SerializeField] GameObject originPanel;

    //la cantidad de carteles que yo he colocado
    [HideInInspector] public int cartelesJugadorA = 0;
    public Image cartelesBarraA;

    [HideInInspector] public int cartelesJugadorB = 0;
    public Image cartelesBarraB;

    [SerializeField] GameObject cartelesSpot;
    [SerializeField] List<Transform> posicionesDePegado = new List<Transform>();
    List<Transform> posicionesRestantes;

    //Audio
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] sonidos;
    [SerializeField] AudioClip winSFX;

    Coroutine sgteCor;
    [SerializeField] Cartel cartel;
    int esquinas;
    void Start()
    {
        esquinas = 0;
        managerMinigame = GameObject.Find("MinijuegosManager").GetComponent<MinigameManager>();      
        managerLocal = GameObject.Find("MinijuegosManager").GetComponent<ManagerMinijuegos>();
        posicionesRestantes = new List<Transform>(posicionesDePegado);
    }

    public new void OnEnable()
    {
        ResetCarteles(0);
        cartelesJugadorB = 0;
        esquinas = 0;
        originPanel = GameObject.Find("OriginPanel");
        RPCManager = GameObject.Find("MiniJuego1_1(cartel)").GetComponent<RPCManager>();
        foreach (Transform child in cartelesSpot.transform)
        {
            Destroy(child.gameObject);
        }
    }
    public new void OnDisable()
    {
        ResetCarteles(0);
        cartelesJugadorB = 0;
        esquinas = 0;
    }

    //Coloco un cartel
    //Le aviso al Master Client que hice un cambio
    public void ColocarCartel()
    {      
        sgteCor = StartCoroutine("SgteCartel");
        Carteles(1);
        esquinas = 0;

        DecirleAMasterClienteQueHiceUnCambio();
    }

    private void Update()
    {
        ActualizarBarras();
    }

    void Carteles(int valor)
    {
        if (originPanel.GetComponent<WhatTeamIsCalling>().team == true)
        {
            cartelesJugadorA += valor;
            RPCManager.RPCActualizarDatosA(originPanel.GetComponent<WhatTeamIsCalling>().id,cartelesJugadorA);
            cartelesBarraA.fillAmount = (float)managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA / 10;
            Debug.Log("El player A ha pegado :" + cartelesJugadorA + " carteles");

        }

        else if (originPanel.GetComponent<WhatTeamIsCalling>().team == false)
        {
            cartelesJugadorB += valor;
            RPCManager.RPCActualizarDatosB(originPanel.GetComponent<WhatTeamIsCalling>().id, cartelesJugadorB);
            cartelesBarraB.fillAmount = (float)managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB / 10;
            Debug.Log("El player B ha pegado :" + cartelesJugadorB + " carteles");
        }

        if (cartelesJugadorA >= 10 || cartelesJugadorB >= 10)
        {
            managerMinigame.FinishTask();
        }
    }

    void ActualizarBarras()
    {
        if (managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].numeroDeJugadores == 2)
        {
            cartelesBarraA.fillAmount = (float)managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusA / 10;
            cartelesBarraB.fillAmount = (float)managerLocal.minijuegos[originPanel.GetComponent<WhatTeamIsCalling>().id].barraVersusB / 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Esquina"))
        {
            // Esquina es pegada;
            audioSource.PlayOneShot(sonidos[Random.Range(0,2)]);
            
            esquinas++;
            collision.GetComponent<BoxCollider2D>().enabled = false;

            this.transform.parent.GetComponentInChildren<Cartel>().Iterar();
            Debug.Log("Cuantas esquinas hay: " + esquinas);


            if (esquinas >= 3)
            {
                ColocarCartel();
            }
        }
    }

    void DecirleAMasterClienteQueHiceUnCambio()     // No se si aun se usa??
    {
        managerLocal.ActualizarEstadoMinijuego1(PhotonNetwork.LocalPlayer.ActorNumber);
    }

    //Master Client me avisa que el otro jugador hizo un cambio
    public void ReciboActualizacionDeOtroJugador()      // Ya no se usa creo????
    {
        //cartelesJugadorB++;
        //cartelesBarraB.fillAmount = (float)cartelesJugadorB / 10;

        //Debug.Log("Cantidad de carteles que ha pegado el jogador 2" + cartelesJugadorB);
        ////contadorJugador2.text = cartelesJugador2.ToString();
        ////Actualizar ui
    }

    [PunRPC]
    public void ResetCarteles(int cartelesBaseValue)
    {
        Debug.Log("Se Reseteo o.0");
        cartelesJugadorA = cartelesBaseValue;
        Debug.Log(cartelesJugadorA);
    }

    IEnumerator SgteCartel()
    {
        cartel.hayCartel = false;
        yield return new WaitForSeconds(.25f);
        audioSource.PlayOneShot(winSFX);
        RandomPos();
        this.transform.parent.GetComponentInChildren<Cartel>().SpawnCartel();
        yield break;
    }

    void RandomPos()
    {
        int posIndex = Random.Range(0,posicionesRestantes.Count);

        cartel.cartelito.transform.localScale = new Vector3(.4f, .4f, 1f);
        cartel.cartelito.transform.position = posicionesRestantes[posIndex].position;
        posicionesRestantes.RemoveAt(posIndex);
    }
}