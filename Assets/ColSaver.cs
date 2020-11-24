using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class ColSaver : MonoBehaviourPunCallbacks,IPunObservable
{
    public Collider2D[] cols;
    public bool team;
    public Minijuego room;
    [SerializeField] Text text;
    [SerializeField] Text text1;
    int queJugador;

    private void Start()
    {
        photonView.RPC("ResetValueA", RpcTarget.AllViaServer);
        photonView.RPC("ResetValueB", RpcTarget.AllViaServer);
    }
    private void Update()
    {
        if (text == null || text1 == null)
            return;
        text.text  = "" + room.jugadorUno;
        text1.text = "" + room.jugadorDos;
    }

    public void RPCDisable() 
    {
        photonView.RPC("DisableCols", RpcTarget.AllViaServer);
    }
    public void RPCEnable()
    {
        photonView.RPC("EnableCols", RpcTarget.AllViaServer);
    }
    public void RPCAddPlayer(int playerActor, bool playerTeam) 
    {
        photonView.RPC("AddPlayer", RpcTarget.MasterClient, playerActor,playerTeam);
    }

    [PunRPC]
    public void AddPlayer(int id,bool team) 
    {
        if (team) 
        {
        room.jugadorUno = id;
        }
        else 
        {
            room.jugadorDos = id;
        }

    }

    [PunRPC]
    public void ResetValueA()
    {
        room.jugadorUno = 0;
    }
    [PunRPC]
    public void ResetValueB()
    {
        room.jugadorDos = 0;
    }

    [PunRPC]
    public void DisableCols() 
    {
        foreach(Collider2D col in cols) 
        {
            col.enabled = false;
        }
    }
    [PunRPC]
    public void EnableCols()
    {
        foreach (Collider2D col in cols)
        {
            col.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerTeam>().TeamA)
        {
            RPCAddPlayer(collision.GetComponent<PlayerId>().id, true);
        }
        if (collision.GetComponent<PlayerTeam>().TeamB)
        {
            RPCAddPlayer(collision.GetComponent<PlayerId>().id, false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerTeam>().TeamA)
        {
            photonView.RPC("ResetValueA", RpcTarget.AllViaServer);
        }
        if (collision.GetComponent<PlayerTeam>().TeamB)
        {
            photonView.RPC("ResetValueB", RpcTarget.AllViaServer);           
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(room.jugadorUno);
            stream.SendNext(room.jugadorDos);
        }
        else if (stream.IsReading)
        {
            room.jugadorUno = (int)stream.ReceiveNext();
            room.jugadorDos = (int)stream.ReceiveNext();
        }
    }

}
