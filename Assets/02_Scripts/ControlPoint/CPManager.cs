using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class CPManager :MonoBehaviourPunCallbacks,IPunObservable
{
    [SerializeField]
    GameObject teamManager1;

    [SerializeField]
    float fameAdded;
    [SerializeField]
    Slider slider,slider2;
    public
    bool whatTeamInControl,alreadyContested;
    bool contested;
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
        if (!contested) { 
        blueControlling = true;
        photonView.RPC("ChangeTeamColor", RpcTarget.AllBuffered, blueControlling);
        photonView.RPC("RPCGainPoints", RpcTarget.MasterClient);
        contested = true;
            photonView.RPC("ChangeControlledBy", RpcTarget.MasterClient, contested, blueControlling);
        }
        else if (contested)
        {
            blueControlling = true;
            photonView.RPC("ChangeTeamColor", RpcTarget.AllBuffered, blueControlling);
            photonView.RPC("ChangeControlledBy", RpcTarget.MasterClient, contested, blueControlling);
        }
    }
    //Change to team2
    public void Team2()
    {
        if (!contested)
        {
            blueControlling = false;
            photonView.RPC("ChangeTeamColor", RpcTarget.AllBuffered, blueControlling);
            photonView.RPC("RPCGainPoints", RpcTarget.MasterClient);
            contested = true;
            photonView.RPC("ChangeControlledBy", RpcTarget.MasterClient, contested, blueControlling);
        }
       else if(contested)
        {
            blueControlling = false;
            photonView.RPC("ChangeTeamColor", RpcTarget.AllBuffered, blueControlling);
            photonView.RPC("ChangeControlledBy", RpcTarget.MasterClient,contested,blueControlling);
        }
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
    [PunRPC]
    public void ChangeControlledBy(bool isContested,bool controlledBy) 
    {
        alreadyContested = isContested;
        whatTeamInControl = controlledBy;
    }


    [PunRPC]
    public void GainPoints()
    {
        
        if(whatTeamInControl)
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
        if (alreadyContested) return;            
       StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(10f);
        photonView.RPC("GainPoints", RpcTarget.MasterClient);       
        yield return new WaitForSeconds(.5f);
        StartCoroutine(Wait());
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