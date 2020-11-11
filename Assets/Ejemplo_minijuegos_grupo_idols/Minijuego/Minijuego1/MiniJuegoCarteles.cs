using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


using Photon.Realtime;
using Photon.Pun;

public class MiniJuegoCarteles : MonoBehaviour
{
    //No es necesariamente el cliente maestro;
    ManagerMinijuegos managerLocal;
    MinigameManager managerMinigame;

    const float tiempoParaRealizarMinijuego = 30;

    int cartelesJugador2 = 0;
    public TextMeshProUGUI contadorJugador2;

    //la cantidad de carteles que yo he colocado
    //public int cartelesColocados = 0;
    
    [SerializeField] BoxCollider2D col2d;
    Coroutine sgteCor;
    [SerializeField] Cartel cartel;
    int esquinas;
    private void Awake()
    {
        col2d.GetComponent<BoxCollider2D>();
        //contadorJugador2 = GameObject.Find("Contador Carteles").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        esquinas = 0;
        managerMinigame = GameObject.Find("MinijuegosManager").GetComponent<MinigameManager>();
        
        managerLocal = GameObject.Find("MinijuegosManager").GetComponent<ManagerMinijuegos>();
    }

    private void OnEnable()
    {
        managerMinigame.ResetCarteles();
    }

    //Coloco un cartel
    //Le aviso al Master Client que hice un cambio
    public void ColocarCartel()
    {
        Debug.Log("Pego 1 cartel");
        Minijuegos.m_cartel(1);
        DecirleAMasterClienteQueHiceUnCambio();
        sgteCor = StartCoroutine("SgteCartel");
        esquinas = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Esquina"))
        {
            // Esquina es pegada;
            Debug.Log("Esquina Pegada");
            esquinas++;
            collision.GetComponent<BoxCollider2D>().enabled = false;

            if (esquinas >= 4)
            {
                ColocarCartel();
            }
        }
    }

    void DecirleAMasterClienteQueHiceUnCambio()
    {
        managerLocal.ActualizarEstadoMinijuego1();
    }

    //Master Client me avisa que el otro jugador hizo un cambio
    public void ReciboActualizacionDeOtroJugador()
    {
        cartelesJugador2++;
        contadorJugador2.text = cartelesJugador2.ToString();
        //Actualizar ui
    }

    IEnumerator SgteCartel()
    {
        //Debug.Log("Entro a la corutina");
        cartel.hayCartel = false;
        yield return new WaitForSeconds(.25f);
        // añadir animación pal lado
        // wait for the animation
        Destroy(cartel.cartelito); 
        this.transform.parent.GetComponentInChildren<Cartel>().SpawnCartel();
        yield break;
    }
}
