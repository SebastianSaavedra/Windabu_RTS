using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


using Photon.Realtime;
using Photon.Pun;

public class MiniJuegoCarteles : MonoBehaviourPunCallbacks
{
    //No es necesariamente el cliente maestro;
    ManagerMinijuegos managerLocal;
    MinigameManager managerMinigame;

    const float tiempoParaRealizarMinijuego = 30;

    [HideInInspector] public int carteles;
    public Image cartelesBarra;

    int cartelesJugador2 = 0;
    public Image contadorJugador2;

    //la cantidad de carteles que yo he colocado
    [SerializeField] GameObject cartelesSpot;
    
    [SerializeField] BoxCollider2D col2d;
    Coroutine sgteCor;
    [SerializeField] Cartel cartel;
    [SerializeField] int esquinas;
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

    public override void OnEnable()
    {
        ResetCarteles(0);
        cartelesJugador2 = 0;
        esquinas = 0;
        //contadorJugador2.text = cartelesJugador2.ToString();
        foreach (Transform child in cartelesSpot.transform)
        {
            Destroy(child.gameObject);
        }
        //owo++;
        //Debug.Log("Se activo el minijuego de carteles! o.0!! " + owo);
        //if (cartelesSpot.transform.parent != null)
        //{
        //    Destroy(cartelesSpot.transform.parent);
        //    Debug.Log("llego acá??");
        //}
    }
    public override void OnDisable()
    {
        ResetCarteles(0);
        cartelesJugador2 = 0;
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

    void Carteles(int valor)
    {
        carteles += valor;
        cartelesBarra.fillAmount += (float)carteles/10;
        Debug.Log("Cantidad de carteles: " + carteles);

        if (carteles >= 3)
        {
            //carteles = 0;
            managerMinigame.FinishTask();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Esquina"))
        {
            // Esquina es pegada;
            
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
        contadorJugador2.fillAmount += (float)cartelesJugador2 / 10;
        Debug.Log("Cantidad de carteles del player2: " + contadorJugador2);
        //contadorJugador2.text = cartelesJugador2.ToString();
        //Actualizar ui
    }

    [PunRPC]
    public void ResetCarteles(int cartelesBaseValue)
    {
        Debug.Log("Se Reseteo o.0");
        carteles = cartelesBaseValue;
        Debug.Log(carteles);
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
