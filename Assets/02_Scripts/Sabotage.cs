using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

namespace Com.MaluCompany.TestGame
{
    public class Sabotage : MonoBehaviourPunCallbacks
{
    [SerializeField] Com.MaluCompany.TestGame.TEST_Movement movement;
    [SerializeField] PlayerTeam playerTeam;
    //Coroutine cor;
    public List<Player> playersActuales = new List<Player>();
    public GameObject playerToInterrupt;
        [SerializeField] float timer;
        float backTimer;
    [SerializeField] Coroutine cor;
    [SerializeField] BoxCollider2D colliderAct;
        public bool saboteando;
        [SerializeField] bool puedeSabotear;

    //private new void OnEnable()
    //{
    //    playerTeam.GetComponent<PlayerTeam>();
    //    Debug.Log(playerTeam);
    //}

    private void Start()
    {
            backTimer = timer;
        if (PhotonNetwork.IsMasterClient) 
        {
                playersActuales.Add(PhotonNetwork.LocalPlayer);

            }
    }

        private void Update()
        {
            if (saboteando) 
            {
                Sabotaje();
            }
            if (puedeSabotear) 
            {
                if (Input.GetKeyDown(KeyCode.F)) 
                {
                    Debug.Log("Un loco del" + GetComponent<PlayerTeam>().team);
                    photonView.RPC("LlamarCorutinaSabotaje", TargetPlayerByActorNumber(GetComponent<PlayerId>().id));
                    puedeSabotear = false;
                }
            }
        }

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

    private void OnTriggerStay2D(Collider2D collision)
    {
            if (photonView.IsMine) {
                #region Sabotaje
                if (this.playerTeam.TeamA && collision.CompareTag("Actividad B"))
        {
            //if (Input.GetKeyDown(KeyCode.F))
            //{
            //    Debug.Log("Presiono la F y es del team A");
            //    photonView.RPC("LlamarCorutinaSabotaje", TargetPlayerByActorNumber(GetComponent<PlayerId>().id));
            //}
        }

        if (this.playerTeam.TeamB && collision.CompareTag("Actividad A"))
        {
            //if (Input.GetKeyDown(KeyCode.F))
            //{
            //    Debug.Log("Presiono la F y es del team B");
            //    photonView.RPC("LlamarCorutinaSabotaje", TargetPlayerByActorNumber(GetComponent<PlayerId>().id));             
            //}
        }
                #endregion

                #region Interrupcion
                if (Input.GetKeyDown(KeyCode.T)) 
        {
        if (this.playerTeam.TeamA && collision.CompareTag("Actividad A"))
        {           
            photonView.RPC("InterrumpirSabotaje", TargetPlayerByActorNumber(colliderAct.GetComponent<ColSaver>().room.jugadorDos));
            photonView.RPC("EnableMovement", TargetPlayerByActorNumber(colliderAct.GetComponent<ColSaver>().room.jugadorDos), true);
        }
        if (this.playerTeam.TeamB && collision.CompareTag("Actividad B"))
        {
            photonView.RPC("InterrumpirSabotaje", TargetPlayerByActorNumber(colliderAct.GetComponent<ColSaver>().room.jugadorUno));
            photonView.RPC("EnableMovement", TargetPlayerByActorNumber(colliderAct.GetComponent<ColSaver>().room.jugadorUno), true);
        }
        }
                #endregion
            }
        }
    
        [PunRPC]
     public void EnableMovement(bool enable) 
        {
            playerToInterrupt.GetComponent<TEST_Movement>().enabled = enable;
            Debug.Log(playerToInterrupt+ "Movimiento Recuperado");
        }

        [PunRPC]
        public void DisableMovement() 
        {
          GetComponent<TEST_Movement>().enabled = false;
        }

    [PunRPC]
    public void Sabotaje()
    {
        movement.enabled = false;
            // yield return new WaitForSeconds(15f);
            timer -= Time.deltaTime;
            if (timer <= 0) 
            {
        colliderAct.GetComponent<ColSaver>().RPCDisable();
        movement.enabled = true;
                if (colliderAct != null)
                {
                    colliderAct = null;
                }
                ResetearValoresSabotaje();
            }
    }
        [PunRPC]
        public void ResetearValoresSabotaje() 
        {
            timer      = backTimer;
            saboteando = false;
        }

    [PunRPC]
    void LlamarCorutinaSabotaje()
    {
            saboteando = true;
            //StartCoroutine(Sabotaje()); ;
            Debug.Log("Cor llamada");
    }
        [PunRPC]
      public void PararCorrutinaSabotaje()
        {
          //  StopCoroutine("Sabotaje");
        }

    [PunRPC]
    public void InterrumpirSabotaje()
    {
        playerToInterrupt.GetComponent<Sabotage>().ResetearValoresSabotaje();
            playerToInterrupt.transform.position = playerToInterrupt.GetComponent<Sabotage>().colliderAct.GetComponent<ColSaver>().goToOnInterrupt.position;
        Debug.Log("Paro sabotaje");
        colliderAct = null;
        Debug.Log(playerToInterrupt.name + " Fue interrumpido");
        Debug.Log("Me han detenido el sabotaje chavales o.0!");
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (playerTeam.TeamA && collision.gameObject.GetComponent<PlayerTeam>().TeamB)
    //    {
    //        Debug.Log("Toco al player enemigo");
    //        if (Input.GetKeyDown(KeyCode.R))
    //        {
    //            photonView.RPC("InterrumpirSabotaje", TargetPlayerByActorNumber(collision.gameObject.GetComponent<PlayerId>().id));
    //            Debug.Log("El Id del player que toco es: " + collision.gameObject.GetComponent<PlayerId>().id);
    //            //collision.GetComponent<Sabotage>().InterrumpirSabotaje();
    //        }
    //    }

    //    if (playerTeam.TeamB && collision.gameObject.GetComponent<PlayerTeam>().TeamA)
    //    {
    //        Debug.Log("Toco al player enemigo");
    //        if (Input.GetKeyDown(KeyCode.R))
    //        {
    //            photonView.RPC("InterrumpirSabotaje", TargetPlayerByActorNumber(collision.gameObject.GetComponent<PlayerId>().id));
    //            Debug.Log("El Id del player que toco es: " + collision.gameObject.GetComponent<PlayerId>().id);
    //            //collision.GetComponent<Sabotage>().InterrumpirSabotaje();
    //        }
    //    }

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (this.playerTeam.TeamA && collision.CompareTag("Actividad B"))
            {
                puedeSabotear = true;
                colliderAct = collision.GetComponent<BoxCollider2D>();
                Debug.Log("Agarro el colliderAct: " + colliderAct);
            }
            if (this.playerTeam.TeamA && collision.CompareTag("Actividad A"))
            {
                colliderAct = collision.GetComponent<BoxCollider2D>();
                Debug.Log("Agarro el colliderAct: " + colliderAct);
            }
            if (this.playerTeam.TeamB && collision.CompareTag("Actividad B"))
            {
                colliderAct = collision.GetComponent<BoxCollider2D>();
                Debug.Log("Agarro el colliderAct: " + colliderAct);
            }
            if (collision.GetComponent<TEST_Interact>())
        {
            playerToInterrupt = collision.gameObject;
        }

        if (this.playerTeam.TeamB && collision.CompareTag("Actividad A"))
        {
                puedeSabotear = true;
            colliderAct = collision.GetComponent<BoxCollider2D>();
            Debug.Log("Agarro el colliderAct: " + colliderAct);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            if (photonView.IsMine) 
            {
        if (collision.GetComponent<TEST_Interact>())
        {
            playerToInterrupt = null;
        }

                if (this.playerTeam.TeamA && collision.CompareTag("Actividad B"))
                {
                    puedeSabotear = false;
                }

                if (this.playerTeam.TeamB && collision.CompareTag("Actividad A"))
                {
                    puedeSabotear = false;
                }

            }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Jugador entra a room");
        playersActuales.Add(newPlayer);
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (colliderAct != null)
    //    {
    //        colliderAct = null;
    //        Debug.Log("Perdio el colliderAct");
    //    }
    //}
}
}