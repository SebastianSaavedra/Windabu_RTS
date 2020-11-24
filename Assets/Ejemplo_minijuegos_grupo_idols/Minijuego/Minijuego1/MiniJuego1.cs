using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using Photon.Realtime;
using Photon.Pun;

public class MiniJuego1 : MonoBehaviour
{
    //No es necesariamente el cleitne maestro;
    ManagerMinijuegos managerLocal;

    const float tiempoParaRealizarMinijuego = 30;
    [SerializeField] GameObject parentHolderBotones1Jugador;

    [Space]
    [SerializeField] GameObject parentHolderBotones2Jugadores_Jugador1;
    [SerializeField] GameObject parentHolderBotones2Jugadores_Jugador2;


    List<Button> hijosParentHolderBotones2Jugadores_Jugador2 = new List<Button>();


    //la cantidad de carteles que yo he colocado
    public int cartelesColocados = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        cartelesColocados = 0;
        
        managerLocal = GameObject.Find("ManagerMinijuego_ClientMasterOnly").GetComponent<ManagerMinijuegos>();

        int cantidadDeBotones = parentHolderBotones2Jugadores_Jugador2.transform.childCount;
        Debug.Log("hijos: " + cantidadDeBotones);

        for (int i = 0; i < cantidadDeBotones; i++)
        {
            Button boton = parentHolderBotones2Jugadores_Jugador2.transform.GetChild(i).GetComponent<Button>();

            hijosParentHolderBotones2Jugadores_Jugador2.Add(boton);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Coloco un cartel
    //Le aviso al Master Client que hice un cambio
    public void ColocarCartel(Button thisButton)
    {
        cartelesColocados++;
        DecirleAMasterClienteQueHiceUnCambio();
        StartCoroutine(ApagarBoton(thisButton));
    }


    void DecirleAMasterClienteQueHiceUnCambio()
    {
        managerLocal.ActualizarEstadoMinijuego1(PhotonNetwork.LocalPlayer.ActorNumber);
    }




    //Master Client me avisa que el otro jugador hizo un cambio
    public void ReciboActualizacionDeOtroJugador()
    {
        //ojo que como ejemplo los voy a eliminar de la lista
        //pero no debería perder esa referencia nunca
        if (hijosParentHolderBotones2Jugadores_Jugador2.Count > 0)
        {
            hijosParentHolderBotones2Jugadores_Jugador2[0].interactable = false;
            hijosParentHolderBotones2Jugadores_Jugador2.RemoveAt(0);
        }
            

    }


    IEnumerator ApagarBoton(Button thisButton)
    {
        yield return new WaitForSeconds(0.1f);
        thisButton.interactable = false;
    }
    
}
