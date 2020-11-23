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
    [SerializeField] Coroutine cor;
    [SerializeField] BoxCollider2D colliderAct;

    //private new void OnEnable()
    //{
    //    playerTeam.GetComponent<PlayerTeam>();
    //    Debug.Log(playerTeam);
    //}

    private void Start()
    {
        
        if (PhotonNetwork.IsMasterClient) 
        {
                playersActuales.Add(PhotonNetwork.LocalPlayer);

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
        if (this.playerTeam.TeamA && collision.CompareTag("Actividad B"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Presiono la F y es del team A");
                photonView.RPC("LlamarCorutinaSabotaje", TargetPlayerByActorNumber(GetComponent<PlayerId>().id));
            }
        }

        if (this.playerTeam.TeamB && collision.CompareTag("Actividad A"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Presiono la F y es del team B");
                photonView.RPC("LlamarCorutinaSabotaje", TargetPlayerByActorNumber(GetComponent<PlayerId>().id));             
            }
        }
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
            }
    }
    
        [PunRPC]
     public void EnableMovement(bool enable) 
        {
            movement.enabled = enable;
            Debug.Log(movement.enabled);
            Debug.Log(this.transform.GetComponent<TEST_Movement>().enabled);
            Debug.Log(transform.name + " Movimiento recuperado");
        }

        [PunRPC]
        public void DisableMovement() 
        {
          GetComponent<TEST_Movement>().enabled = false;
        }

    [PunRPC]
    IEnumerator Sabotaje()
    {
        movement.enabled = false;
        yield return new WaitForSeconds(15f);
        colliderAct.GetComponent<ColSaver>().RPCDisable();
        movement.enabled = true;
        if (colliderAct != null)
        {
            colliderAct = null;
        }
        yield break;
    }

    [PunRPC]
    void LlamarCorutinaSabotaje()
    {
            StartCoroutine(Sabotaje()); ;
            Debug.Log("Cor llamada");
    }

    [PunRPC]
    public void InterrumpirSabotaje()
    {
        StopCoroutine(Sabotaje());
        Debug.Log("Paro sabotaje");
        colliderAct = null;
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
            colliderAct = collision.GetComponent<BoxCollider2D>();
            Debug.Log("Agarro el colliderAct: " + colliderAct);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<TEST_Interact>())
        {
            playerToInterrupt = null;
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