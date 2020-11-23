using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class Sabotage : MonoBehaviourPunCallbacks
{
    [SerializeField] Com.MaluCompany.TestGame.TEST_Movement movement;
    [SerializeField] PlayerTeam playerTeam;
    //Coroutine cor;
    public List<Player> playersActuales = new List<Player>();

    [SerializeField] BoxCollider2D colliderAct;

    //private new void OnEnable()
    //{
    //    playerTeam.GetComponent<PlayerTeam>();
    //    Debug.Log(playerTeam);
    //}

    private void Awake()
    {
        //movement.GetComponent<Com.MaluCompany.TestGame.TEST_Movement>();
        if (PhotonNetwork.IsMasterClient)   //Tener en mente que este codigo puede producir errores a futuro.
        {
            //SetearUIMasterClient();
            playersActuales.Add(PhotonNetwork.LocalPlayer);
            //Debug.Log("On joined room ha sido debugea2 (?");
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
        Debug.Log("Entro al collider de la wea");
        if (this.playerTeam.TeamA && collision.CompareTag("Actividad B"))
        {
            Debug.Log("El man es del Team A");
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Presiono la F y es del team A");
                photonView.RPC("LlamarCorutinaSabotaje", RpcTarget.AllViaServer);
            }
        }

        if (this.playerTeam.TeamB && collision.CompareTag("Actividad A"))
        {
            Debug.Log("El man es del Team B");
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Presiono la F y es del team B");
                photonView.RPC("LlamarCorutinaSabotaje", RpcTarget.AllViaServer);
            }
        }
    }

    [PunRPC]
    IEnumerator Sabotaje()
    {
        Debug.Log("Llego a la corutina del sab");
        movement.enabled = false;
        yield return new WaitForSeconds(15f);
        colliderAct.enabled = false;
        movement.enabled = true;
        if (colliderAct != null)
        {
            colliderAct = null;
            Debug.Log("Perdio el colliderAct");
        }
        yield break;
    }

    [PunRPC]
    void LlamarCorutinaSabotaje()
    {
        StartCoroutine("Sabotaje");
    }
    //[PunRPC]
    //public void InterrumpirSabotaje()
    //{
    //    StopCoroutine("Sabotaje");
    //    movement.enabled = true;
    //    colliderAct = null;
    //    Debug.Log("Me han detenido el sabotaje chavales o.0!");
    //}

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

        if (this.playerTeam.TeamB && collision.CompareTag("Actividad A"))
        {
            colliderAct = collision.GetComponent<BoxCollider2D>();
            Debug.Log("Agarro el colliderAct: " + colliderAct);
        }
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