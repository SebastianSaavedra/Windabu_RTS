﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;
using UnityEditor;

public class ManagerMinijuegos : MonoBehaviourPunCallbacks
{
    //Lista que guarda los jugadores que están en la sala
    public List<Player> playersActuales = new List<Player>();

    public TextMeshProUGUI textInfoMaster;
    //con respecto a si están o no disponibles para ser jugados
    public Button[] botonesMinijuego;
    public GameObject[] parentObjetosMinijuegosUNPlayer;
    public GameObject[] parentObjetosMinijuegosDOSPlayers;

    public List<Minijuego> minijuegos = new List<Minijuego>();
    

    private void Awake()
    {
        //esto en algún lugar debería estar, en el launcher probablemente
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    bool isConnecting;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var minijuego in minijuegos)
        {
            minijuego.ResetearValoresMinijuego();
        }

        Connect();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    //Player al que enviarle el rpc
    Player TargetPlayerByActorNumber(int playerActorNumber)
    {
        //Solamente se tiene para mostrar el número de jugadores y su actorNumber
        foreach (var item in playersActuales)
        {
            Debug.Log("Players: " + item.ActorNumber);
        }

        //Determina a qué jugador debemos enviarle el RPC según su actor number 
        //(identificador de cada jugador en esta sala de juego)
        Player playerToReturn = null;
        for (int i = 0; i < playersActuales.Count; i++)
        {
            if (playersActuales[i].ActorNumber == playerActorNumber)
            {
                playerToReturn = playersActuales[i];
                break;
            }
        }
        return playerToReturn;
    }


    //---------Minijuegos------
    //Este método solamente los debe ejecutar el ciente maestro
    public void ComenzarUnMinijuego(int minijuegoAComenzar, int playerActorNumber)
    {
        minijuegos[minijuegoAComenzar].ComenzarMinijuego();
        minijuegos[minijuegoAComenzar].numeroDeJugadores++; //agrego un jugador

        //Setea el jugador que está actualmente en el minijuego
        //con esto ahora tengo guardados los dos jugadores que están en el minijuego
        //puedo dedicarme a recibir y realizar RPC para ellos (actualizar lo que pasa en pantalla)
        if (minijuegos[minijuegoAComenzar].numeroDeJugadores == 1)
        {
            minijuegos[minijuegoAComenzar].jugadorUno = playerActorNumber;
        }
        else if (minijuegos[minijuegoAComenzar].numeroDeJugadores == 2)
        {
            minijuegos[minijuegoAComenzar].jugadorDos = playerActorNumber;
        }
        
        
        Debug.Log("jugador que está llamando este RPC: " + playerActorNumber);


        //autoriza al jugador a jugar
        if (minijuegos[minijuegoAComenzar].numeroDeJugadores == 1)
        {
            //Activar versión para un jugador  
            photonView.RPC("MiniJuegoComenzadoUnJugador", TargetPlayerByActorNumber(playerActorNumber), minijuegoAComenzar);
        }
        else if(minijuegos[minijuegoAComenzar].numeroDeJugadores == 2)
        {
            //Activar versión para dos jugadores
            //Activar en el jugador que ya estaba jugando, la versión dos jugadores
            photonView.RPC("MiniJuegoComenzadoDosJugadores",
                TargetPlayerByActorNumber(minijuegos[minijuegoAComenzar].jugadorUno),
                minijuegoAComenzar);
            //Actiavr en el jugador que acaba  dellegar al minijuego la versión de dos jugadores
            photonView.RPC("MiniJuegoComenzadoDosJugadores", TargetPlayerByActorNumber(playerActorNumber), minijuegoAComenzar);
            
        }
    }

    //-----Minijuego 1 esto es un ejemplo, por temas de orden no debería estar todo en la misma clase
    //Esta actualizaci´´on es particularmente cuando son dos los jugadores, no 1
    #region Minijuego 1
    //ManagerMinijuegos local le avisa al ManagerMinijuegos del Cliente Maestro que debe realizar una actualización
    //del minijuego 1
    public void ActualizarEstadoMinijuego1()
    {
        Debug.Log("Intentar actualizar");
        photonView.RPC("IntentarActualizarEstadoMiniJuego1", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.ActorNumber);    
    }


    [PunRPC] //ManagerMinijuegos del Cliente Maestro se dice a si mismo que envie el aviso de esta actualización
    void IntentarActualizarEstadoMiniJuego1(int playerActorNumber)
    {
        //determino a cuál de los dos jugadores le debo enviar este mensaje
        if (minijuegos[0].jugadorUno == playerActorNumber)
        {
            photonView.RPC("EnviarRPCAlOtroJugadorMinijuego1", RpcTarget.MasterClient
                , minijuegos[0].jugadorDos);
        }
        else if (minijuegos[0].jugadorDos == playerActorNumber)
        {
            photonView.RPC("EnviarRPCAlOtroJugadorMinijuego1", RpcTarget.MasterClient
                , minijuegos[0].jugadorUno);
        }
    }

    [PunRPC]    //ManagerMinijuegos del Cliente Maestro intenta avisarle al ManagerMinijuegos local del otro jugador
                //que se realizó un cambio
    void EnviarRPCAlOtroJugadorMinijuego1(int playerActorNumber)
    {
        if(minijuegos[0].numeroDeJugadores == 2)
        {
            photonView.RPC("AvisarActualizacionMinijuego1", TargetPlayerByActorNumber(playerActorNumber));
        }
        else
        {
            Debug.Log("solamente hay un jugador");
        }
        
        
    }

    [PunRPC] //ManagerMinijuegos local le avisa al manager del minijuego 1 que recibió un cambio
    void AvisarActualizacionMinijuego1()
    {
        Debug.Log("Intengo actualizar minijuego 1");
        GameObject.Find("ManagerMinijuego1").GetComponent<MiniJuego1>().ReciboActualizacionDeOtroJugador();
    }
    #endregion
    //-------------




    [PunRPC]
    void ActualizarUIMasterClient()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SetearUIMasterClient();
        }
    }



    //Cambia el estado visual del cliente
    [PunRPC]
    void MiniJuegoComenzadoUnJugador(int indexMiniJuego)
    {
        Debug.Log("Minijuego iniciado: " + indexMiniJuego);
        //de no estar activado el minijuego, lo activa para un jugador 
        EncenderUIMinijuegoUnJugador(indexMiniJuego);

        
        photonView.RPC("ActualizarUIMasterClient", RpcTarget.MasterClient);     
    }

    [PunRPC]
    void MiniJuegoComenzadoDosJugadores(int indexMiniJuego)
    {
        Debug.Log("Minijuego iniciado: " + indexMiniJuego);
        //de no estar activado el minijuego, lo activa para un jugador 
        EncenderUIMinijuegoDOSJugadores(indexMiniJuego);


        photonView.RPC("ActualizarUIMasterClient", RpcTarget.MasterClient);
    }



    


    //AQUÍ YA DEBERÍA VERSE TODA LA LÓGICA DEL MINIJUEGO,
    //ir enviando actualizaciones para el Cliente Maestro
    //e ir enviando actualizaciones desde el cliente maestro al/ a los cliente/s
    //generalizar a cualquier juego
    void EncenderUIMinijuegoUnJugador(int indexMinijuego)
    {
        parentObjetosMinijuegosUNPlayer[indexMinijuego].SetActive(true);
    }

    void EncenderUIMinijuegoDOSJugadores(int indexMinijuego)
    {
        //Si se hace algo como esto, apagar el ui que ya estaba encendida en el jugador inicial
        parentObjetosMinijuegosDOSPlayers[indexMinijuego].SetActive(true);
    }




    //----player join/quit
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Jugador entra a room");
        playersActuales.Add(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        playersActuales.Remove(otherPlayer);
    }

    void ApagarBotonMinijuego(int indexMinijuego)
    {
        botonesMinijuego[indexMinijuego].interactable = false;
    }


    #region No importa

    public void Connect()
    {
        Debug.Log("conectando");
        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {
            // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
            // keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
            isConnecting = PhotonNetwork.ConnectUsingSettings();

        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
        // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
        // we don't want to do anything if we are not attempting to join a room.
        // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
        // we don't want to do anything.
        if (isConnecting)
        {
            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;
        }
    }

    #endregion

    //---------Actualización visual, no es importante--------
    //Get info si soy cliente maestro o no
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {            
            SetearUIMasterClient();
            playersActuales.Add(PhotonNetwork.LocalPlayer);
        }
        else
        {
            SeterUIClienteNormal();
        }
        
    }

    #region No importa

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 6 });
    }

    #endregion


    //Esto solamente hace el cambio visual en el ui de la pantalla
    void SetearUIMasterClient()
    {
        textInfoMaster.text = "CLIENTE MAESTRO " + "Minijuego 1"+ minijuegos[0].estadoDelJuego + " Minijuego 2:" + minijuegos[1].estadoDelJuego;
    }

    void SeterUIClienteNormal()
    {
        textInfoMaster.text = "Cliente normal";
    }

}