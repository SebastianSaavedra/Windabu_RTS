using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;
using UnityEditor;
using System.Xml.Schema;

public class ManagerMinijuegos : MonoBehaviourPunCallbacks,IPunObservable
{
    //Lista que guarda los jugadores que están en la sala
    public List<Player> playersActuales = new List<Player>();

    //public TextMeshProUGUI textInfoMaster;
    //con respecto a si están o no disponibles para ser jugados
    //public Button[] botonesMinijuego;
    public GameObject[] parentObjetosMinijuegosUNPlayer;
    public GameObject[] parentObjetosMinijuegosDOSPlayers;
    public int player1_ID;
    public int player2_ID;
    public int wichMinigamePanel;
    public static bool start;
    [HideInInspector] public int idMinijuego;


    public List<Minijuego> minijuegos = new List<Minijuego>();

    [SerializeField] MiniJuegoCarteles minijuegoCarteles;
    [SerializeField] PanelData[] minigameData;
    

    //private void Awake()
    //{
    //    //esto en algún lugar debería estar, en el launcher probablemente
    //    PhotonNetwork.AutomaticallySyncScene = true;
    //}

    //bool isConnecting;

    // Start is called before the first frame update
    void Start()
    {
        start = true;
        foreach (var minijuego in minijuegos)
        {
            minijuego.ResetearValoresMinijuego();
        }

        if (PhotonNetwork.IsMasterClient)   //Tener en mente que este codigo puede producir errores a futuro.
        {
            //SetearUIMasterClient();
            playersActuales.Add(PhotonNetwork.LocalPlayer);
            Debug.Log("On joined room ha sido debugea2 (?");

        }

        //Connect();
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
            player1_ID = playerActorNumber;
        }
        else if (minijuegos[minijuegoAComenzar].numeroDeJugadores == 2)
        {
            minijuegos[minijuegoAComenzar].jugadorDos = playerActorNumber;
            player2_ID = playerActorNumber;
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
    public void ActualizarEstadoMinijuego1(int playerActor)
    {
        Debug.Log("Intentar actualizar");
        photonView.RPC("IntentarActualizarEstadoMiniJuego1", RpcTarget.MasterClient, playerActor);
    }
    //public void ActualizarEstadoMinijuego2()
    //{
    //    Debug.Log("Intentar actualizar");
    //    photonView.RPC("IntentarActualizarEstadoMiniJuego2", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.ActorNumber);
    //}


    [PunRPC] //ManagerMinijuegos del Cliente Maestro se dice a si mismo que envie el aviso de esta actualización
    void IntentarActualizarEstadoMiniJuego1(int playerActorNumber)
    {
        //determino a cuál de los dos jugadores le debo enviar este mensaje
        if (minijuegos[idMinijuego].jugadorUno == playerActorNumber)
        {
            photonView.RPC("EnviarRPCAlOtroJugadorMinijuego1", RpcTarget.MasterClient
                , minijuegos[idMinijuego].jugadorDos);
        }
        else if (minijuegos[idMinijuego].jugadorDos == playerActorNumber)
        {
            photonView.RPC("EnviarRPCAlOtroJugadorMinijuego1", RpcTarget.MasterClient
                , minijuegos[idMinijuego].jugadorUno);
        }
    }

    //[PunRPC] //ManagerMinijuegos del Cliente Maestro se dice a si mismo que envie el aviso de esta actualización
    //void IntentarActualizarEstadoMiniJuego2(int playerActorNumber)
    //{
    //    //determino a cuál de los dos jugadores le debo enviar este mensaje
    //    if (minijuegos[idMinijuego].jugadorUno == playerActorNumber)
    //    {
    //        photonView.RPC("EnviarRPCAlOtroJugadorMinijuego1", RpcTarget.MasterClient
    //            , minijuegos[idMinijuego].jugadorDos);
    //    }
    //    else if (minijuegos[idMinijuego].jugadorDos == playerActorNumber)
    //    {
    //        photonView.RPC("EnviarRPCAlOtroJugadorMinijuego1", RpcTarget.MasterClient
    //            , minijuegos[idMinijuego].jugadorUno);
    //    }
    //}

    [PunRPC]    //ManagerMinijuegos del Cliente Maestro intenta avisarle al ManagerMinijuegos local del otro jugador
                //que se realizó un cambio
    void EnviarRPCAlOtroJugadorMinijuego1(int playerActorNumber)
    {
        if(minijuegos[0].numeroDeJugadores == 2)        // Carteles
        {
            photonView.RPC("AvisarActualizacionMinijuego1", TargetPlayerByActorNumber(playerActorNumber));
        }
        else if (minijuegos[1].numeroDeJugadores == 2)        // Carteles
        {
            photonView.RPC("AvisarActualizacionMinijuego1", TargetPlayerByActorNumber(playerActorNumber));
        }
        else if (minijuegos[2].numeroDeJugadores == 2)        // Lightstick
        {
            photonView.RPC("AvisarActualizacionMinijuego2", TargetPlayerByActorNumber(playerActorNumber));
        }
        else if (minijuegos[3].numeroDeJugadores == 2)        // Lightstick
        {
            photonView.RPC("AvisarActualizacionMinijuego2", TargetPlayerByActorNumber(playerActorNumber));
        }
        else
        {
            Debug.Log("solamente hay un jugador");
        }
        
        
    }

    //[PunRPC]    //ManagerMinijuegos del Cliente Maestro intenta avisarle al ManagerMinijuegos local del otro jugador
    //            //que se realizó un cambio
    //void EnviarRPCAlOtroJugadorMinijuego2(int playerActorNumber)
    //{
    //    if (minijuegos[2].numeroDeJugadores == 2)        //Lightstick
    //    {
    //        photonView.RPC("AvisarActualizacionMinijuego2", TargetPlayerByActorNumber(playerActorNumber));
    //    }
    //    else
    //    {
    //        Debug.Log("solamente hay un jugador");
    //    }
    //}

    [PunRPC] //ManagerMinijuegos local le avisa al manager del minijuego 1 que recibió un cambio
    void AvisarActualizacionMinijuego1()
    {
        Debug.Log("Intento actualizar minijuego 1");
        GameObject.Find("Rodillo").GetComponent<MiniJuegoCarteles>().ReciboActualizacionDeOtroJugador();
    }

    [PunRPC] //ManagerMinijuegos local le avisa al manager del minijuego 1 que recibió un cambio
    void AvisarActualizacionMinijuego2()
    {
        Debug.Log("Intengo actualizar minijuego 2");
        GameObject.Find("Rodillo").GetComponent<MiniJuegoCarteles>().ReciboActualizacionDeOtroJugador();
    }
    #endregion
    //-------------

    //[PunRPC]
    //void ActualizarUIMasterClient()
    //{
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        SetearUIMasterClient();
    //    }
    //}



    //Cambia el estado visual del cliente
    [PunRPC]
    void MiniJuegoComenzadoUnJugador(int indexMiniJuego)
    {        
        //de no estar activado el minijuego, lo activa para un jugador 
        EncenderUIMinijuegoUnJugador(indexMiniJuego);

        
        //photonView.RPC("ActualizarUIMasterClient", RpcTarget.MasterClient);     
    }

    [PunRPC]
    void MiniJuegoComenzadoDosJugadores(int indexMiniJuego)
    {
        
        //de no estar activado el minijuego, lo activa para un jugador 
        EncenderUIMinijuegoDOSJugadores(indexMiniJuego);


        //photonView.RPC("ActualizarUIMasterClient", RpcTarget.MasterClient);
    }

    public void CambiarInteractable(int playerId, int minijuego) 
    {
        Debug.Log("Llego al CambiarInteractable" + playerId);
        Destroy(parentObjetosMinijuegosUNPlayer[0].transform.GetChild(0).gameObject);
        GameObject panel;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Debug.Log("Debugeando el foreach");
            if (player.GetComponentInParent<PlayerId>().id == playerId)
            {
                Debug.Log("Entro al IF del PlayerID");
                if (player.GetComponentInParent<PlayerTeam>().TeamA) 
                {
                    Debug.Log("Reconocio TEAM A");
                    panel = Instantiate(minigameData[minijuego].PanelVSA, parentObjetosMinijuegosUNPlayer[0].transform.position, Quaternion.identity);
                    panel.transform.parent = parentObjetosMinijuegosUNPlayer[0].transform;
                    panel.transform.localScale = new Vector3(1, 1, 1);
                  //  player.GetComponentInParent<TEST_Interact>().objectToInteract = panel;
                }

                if (player.GetComponentInParent<PlayerTeam>().TeamB)
                {
                    Debug.Log("Reconocio TEAM B");
                    panel = Instantiate(minigameData[minijuego].PanelVSB, parentObjetosMinijuegosUNPlayer[0].transform.position, Quaternion.identity);
                    panel.transform.parent = parentObjetosMinijuegosUNPlayer[0].transform;
                    panel.transform.localScale = new Vector3(1, 1, 1);
                   // player.GetComponentInParent<TEST_Interact>().objectToInteract = panel;
                }
            }
        }
    }
    public void ResetearMuchosValores(int minigame)
    {
        photonView.RPC("RPCResetearMuchosValores", RpcTarget.MasterClient, minigame);
    }

    [PunRPC]
    public void RPCResetearMuchosValores(int minigame) 
    {
        minijuegos[minigame].ResetearValoresMinijuego();
        player1_ID = 0;
        player2_ID = 0;
    }

    public void ReseteoDeCarteles(int minigame)   // HAY QUE REINICIAR LOS CARTELES
    {
        if (minijuegos[minigame].numeroDeJugadores == 1) 
        {
        minijuegoCarteles.photonView.RPC("ResetCarteles", TargetPlayerByActorNumber(player1_ID),minigame);
        }
        else if (minijuegos[minigame].numeroDeJugadores == 2)
        {
            minijuegoCarteles.photonView.RPC("ResetCarteles", TargetPlayerByActorNumber(player1_ID),minigame);
            minijuegoCarteles.photonView.RPC("ResetCarteles", TargetPlayerByActorNumber(player2_ID),minigame);
        }
        // photonView.RPC("ResetCarteles",TargetPlayerByActorNumber());
    }

    //AQUÍ YA DEBERÍA VERSE TODA LA LÓGICA DEL MINIJUEGO,
    //ir enviando actualizaciones para el Cliente Maestro
    //e ir enviando actualizaciones desde el cliente maestro al/ a los cliente/s
    //generalizar a cualquier juego
    void EncenderUIMinijuegoUnJugador(int indexMinijuego)
    {
     //   parentObjetosMinijuegosUNPlayer[indexMinijuego].SetActive(true);
        Debug.Log("Se activa UI Minijuego 1");
    }

    void EncenderUIMinijuegoDOSJugadores(int indexMinijuego)
    {
        //Si se hace algo como esto, apagar el ui que ya estaba encendida en el jugador inicial
        Debug.Log("Se activa UI Minijuego 2");
        //parentObjetosMinijuegosUNPlayer[indexMinijuego].SetActive(false);
        //parentObjetosMinijuegosDOSPlayers[indexMinijuego].SetActive(true);
        //photonView.RPC("CambiarInteractable", TargetPlayerByActorNumber(minijuegos[indexMinijuego].jugadorUno), minijuegos[indexMinijuego].jugadorUno, indexMinijuego);
        //photonView.RPC("CambiarInteractable", TargetPlayerByActorNumber(minijuegos[indexMinijuego].jugadorDos), minijuegos[indexMinijuego].jugadorDos, indexMinijuego);      
    }
    public void StartGame() 
    {
        start = true;
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
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            for (int x=0;x<minijuegos.Count;x++) 
            {
                stream.SendNext(minijuegos[x].jugadorUno);
                stream.SendNext(minijuegos[x].jugadorDos);
                stream.SendNext(minijuegos[x].numeroDeJugadores);
                stream.SendNext(minijuegos[x].completado);
                stream.SendNext(minijuegos[x].barraVersusA);
                stream.SendNext(minijuegos[x].barraVersusB);
                stream.SendNext(minijuegos[x].rondaA);
                stream.SendNext(minijuegos[x].rondaB);
                stream.SendNext(minijuegos[x].cantidadDeFallosLightstickA);
                stream.SendNext(minijuegos[x].cantidadDeFallosLightstickB);
                stream.SendNext(minijuegos[x].falloLighstickA);
                stream.SendNext(minijuegos[x].falloLighstickB);
                stream.SendNext(minijuegos[x].intentosA);
                stream.SendNext(minijuegos[x].intentosB);
            }
            stream.SendNext(start);
        }
        else if (stream.IsReading)
        {
            for (int x = 0; x < minijuegos.Count; x++)
            {
                minijuegos[x].jugadorUno        = (int)stream.ReceiveNext();
                minijuegos[x].jugadorDos        = (int)stream.ReceiveNext();
                minijuegos[x].numeroDeJugadores = (int)stream.ReceiveNext();
                minijuegos[x].completado        = (bool)stream.ReceiveNext();
                minijuegos[x].barraVersusA = (int)stream.ReceiveNext();
                minijuegos[x].barraVersusB = (int)stream.ReceiveNext();
                minijuegos[x].rondaA = (int)stream.ReceiveNext();
                minijuegos[x].rondaB = (int)stream.ReceiveNext();
                minijuegos[x].cantidadDeFallosLightstickA = (int)stream.ReceiveNext();
                minijuegos[x].cantidadDeFallosLightstickB = (int)stream.ReceiveNext();
                minijuegos[x].falloLighstickA = (bool)stream.ReceiveNext();
                minijuegos[x].falloLighstickB = (bool)stream.ReceiveNext();
                minijuegos[x].intentosA = (int)stream.ReceiveNext();
                minijuegos[x].intentosB = (int)stream.ReceiveNext();
            }
            start = (bool)stream.ReceiveNext();
        }
    }

    //void ApagarBotonMinijuego(int indexMinijuego)
    //{
    //    botonesMinijuego[indexMinijuego].interactable = false;
    //}


    #region No importa

    //public void Connect()
    //{
    //    Debug.Log("conectando");
    //    // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
    //    if (PhotonNetwork.IsConnected)
    //    {
    //        // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
    //        PhotonNetwork.JoinRandomRoom();
    //    }
    //    else
    //    {
    //        // #Critical, we must first and foremost connect to Photon Online Server.
    //        // keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
    //        isConnecting = PhotonNetwork.ConnectUsingSettings();

    //    }
    //}

    //public override void OnConnectedToMaster()
    //{
    //    Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
    //    // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
    //    // we don't want to do anything if we are not attempting to join a room.
    //    // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
    //    // we don't want to do anything.
    //    if (isConnecting)
    //    {
    //        // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
    //        PhotonNetwork.JoinRandomRoom();
    //        isConnecting = false;
    //    }
    //}

    #endregion

    //---------Actualización visual, no es importante--------
    //Get info si soy cliente maestro o no
    //public override void OnJoinedRoom()
    //{
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        //SetearUIMasterClient();
    //        playersActuales.Add(PhotonNetwork.LocalPlayer);
    //        Debug.Log("On joined room ha sido debugea2 (?");

    //    }
    //}
    //else
    //{
    //    SeterUIClienteNormal();
    //}

    //}

    #region No importa

    //public override void OnJoinRandomFailed(short returnCode, string message)
    //{
    //    Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

    //    // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
    //    PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 6 });
    //}

    #endregion


    //Esto solamente hace el cambio visual en el ui de la pantalla
    //void SetearUIMasterClient()
    //{
    //    textInfoMaster.text = "CLIENTE MAESTRO " + "Minijuego 1"+ minijuegos[0].estadoDelMinijuego + " Minijuego 2:" + minijuegos[1].estadoDelMinijuego;
    //}

    //void SeterUIClienteNormal()
    //{
    //    textInfoMaster.text = "Cliente normal";
    //}

}