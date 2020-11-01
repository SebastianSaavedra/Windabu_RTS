using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class CPManager :MonoBehaviourPunCallbacks,IPunObservable
{
    [SerializeField]
    GameObject teamManager1, teamManager2;

    [SerializeField]
    float fameAdded;
    [SerializeField]
    Slider slider,slider2;

    bool blueControlling;
    PhotonView PV;
    #region Testing Code
    Color colorNeutral, colorTeam1, colorTeam2;

    public void Awake()
    {
        PV = GetComponent<PhotonView>();
        colorNeutral = gameObject.GetComponent<SpriteRenderer>().color;

        colorTeam1 = Color.blue;
        colorTeam2 = Color.red;
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 10;
    }
    #endregion

    //Change to team1
    public void Team1()
    {
        blueControlling = true;
        photonView.RPC("ChangeTeamColor", RpcTarget.AllBuffered, blueControlling);
        photonView.RPC("RPCGainPoints", RpcTarget.MasterClient);
    }
    [PunRPC]
    public void ChangeTeamColor(bool whatTeam) 
    {
        if(whatTeam)
        {
        gameObject.GetComponent<SpriteRenderer>().color = colorTeam1;
        }
        else 
        {
            gameObject.GetComponent<SpriteRenderer>().color = colorTeam2;
        }
    }


    //Change to team2
    public void Team2()
    {
        blueControlling = false;
        photonView.RPC("ChangeTeamColor", RpcTarget.AllBuffered, blueControlling);
        photonView.RPC("RPCGainPoints", RpcTarget.MasterClient);
    }

    [PunRPC]
    public void GainPoints(bool whatTeam)
    {
        Debug.Log("Llamado");
        if(whatTeam)
        {
            teamManager1.GetComponent<TeamManager>().fameA += fameAdded;
            slider.value += fameAdded;
        }
        else 
        {
            teamManager1.GetComponent<TeamManager>().fameB += fameAdded;
            slider2.value += fameAdded;
        }
    }

    [PunRPC]
    public void RPCGainPoints()
    {
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(10f);
        photonView.RPC("GainPoints", RpcTarget.MasterClient,blueControlling);
        yield return new WaitForSeconds(.5f);
        photonView.RPC("RPCGainPoints", RpcTarget.MasterClient);
        yield break;
    }

    void ChangeMat(GameObject sprite) 
    {
        sprite = sprite.GetComponent<SpriteRenderer>().gameObject;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
    {
       if (stream.IsWriting) 
       {
            stream.SendNext(slider.value);
            stream.SendNext(slider2.value);
      }
      else if (stream.IsReading) 
      {
           slider.value =(float)stream.ReceiveNext();
           slider2.value =(float)stream.ReceiveNext();
      }
      }
    
}