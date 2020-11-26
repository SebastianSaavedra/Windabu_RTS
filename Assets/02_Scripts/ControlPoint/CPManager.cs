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
    Image slider,slider2;
    public
    bool whatTeamInControl,alreadyContested;
    bool contested;
    bool blueControlling;
    [SerializeField] TaskDropDown canInteract;
    [SerializeField] GameObject teamACartel, teamACartel_Locked, teamBCartel, teamBCartel_Locked;
    [SerializeField] GameObject teamAWon, teamBwon;
    [SerializeField] GameObject parentPanel;
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
        photonView.RPC("QueTeamGano", RpcTarget.AllBuffered, blueControlling);
        photonView.RPC("RPCGainPoints", RpcTarget.MasterClient);
        contested = true;
        photonView.RPC("ChangeControlledBy", RpcTarget.MasterClient, contested, blueControlling);
            RPCLockA();
        }
        else if (contested)
        {
            blueControlling = true;
            photonView.RPC("ChangeTeamColor", RpcTarget.AllBuffered, blueControlling);
        photonView.RPC("QueTeamGano", RpcTarget.AllBuffered, blueControlling);
            photonView.RPC("ChangeControlledBy", RpcTarget.MasterClient, contested, blueControlling);
            RPCLockA();
        }
    }
    //Change to team2
    public void Team2()
    {
        if (!contested)
        {
            blueControlling = false;
            photonView.RPC("ChangeTeamColor", RpcTarget.AllBuffered, blueControlling);
            photonView.RPC("QueTeamGano", RpcTarget.AllBuffered, blueControlling);
            photonView.RPC("RPCGainPoints", RpcTarget.MasterClient);
            contested = true;
            photonView.RPC("ChangeControlledBy", RpcTarget.MasterClient, contested, blueControlling);
            RPCLockB();
        }
       else if(contested)
        {
            blueControlling = false;
            photonView.RPC("ChangeTeamColor", RpcTarget.AllBuffered, blueControlling);
            photonView.RPC("QueTeamGano", RpcTarget.AllBuffered, blueControlling);
            photonView.RPC("ChangeControlledBy", RpcTarget.MasterClient, contested, blueControlling);
            RPCLockB();
        }
    }
    [PunRPC]
    public void QueTeamGano(bool team) 
    {
        if (team)
        {
            GameObject panel = Instantiate(teamAWon, parentPanel.transform);
            panel.transform.parent = parentPanel.transform;
        }
        else 
        {
            GameObject panel = Instantiate(teamBwon, parentPanel.transform);
            panel.transform.parent = parentPanel.transform;
        }
    }

    [PunRPC]
    public void ChangeTeamColor(bool whatTeam) 
    {
        if(whatTeam)
        {
        gameObject.GetComponent<SpriteRenderer>().color = colorTeam1;
            teamACartel.SetActive(true);
        }
        else 
        {
            gameObject.GetComponent<SpriteRenderer>().color = colorTeam2;
            teamBCartel.SetActive(true);
        }
    }
    [PunRPC]
    public void ChangeControlledBy(bool isContested,bool controlledBy) 
    {
        alreadyContested = isContested;
        whatTeamInControl = controlledBy;
    }
    public void RPCGainPointsViaFan(bool team,float points) 
    {
        photonView.RPC("GainPointsViaFans", RpcTarget.MasterClient,team,points);
    }

    [PunRPC]
    public void GainPointsViaFans(bool team, float points)
    {

        if (team)
        {
            teamManager1.GetComponent<TeamManager>().fameA += points;
            slider.fillAmount += (fameAdded * 0.2f);
        }
        else
        {
            teamManager1.GetComponent<TeamManager>().fameB += points;
            slider2.fillAmount += (fameAdded * 0.2f);
        }
    }
    [PunRPC]
    public void GainPoints()
    {
        
        if(whatTeamInControl)
        {
            teamManager1.GetComponent<TeamManager>().fameA += fameAdded;
            slider.fillAmount += (fameAdded*0.2f);
        }
        else 
        {
            teamManager1.GetComponent<TeamManager>().fameB += fameAdded;
            slider2.fillAmount += (fameAdded * 0.2f);
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

    public void RPCLockA() 
    {
        photonView.RPC("LockTeamA",RpcTarget.AllViaServer);
    }
    public void RPCLockB()
    {
        photonView.RPC("LockTeamB",RpcTarget.AllViaServer);
    }

    [PunRPC]
    public void LockTeamA() 
    {
        StopCoroutine(LockTeamACor());
        StartCoroutine(LockTeamACor());
    }
    [PunRPC]
    public void LockTeamB() 
    {
        StartCoroutine(LockTeamBCor());
    }


    [PunRPC]
    IEnumerator LockTeamACor() 
    {
        teamACartel_Locked.SetActive(true);
        teamBCartel.SetActive(false);
        yield return new WaitForSeconds(5f);
        teamACartel_Locked.SetActive(false);      
        yield break;
    }
    [PunRPC]
    IEnumerator LockTeamBCor()
    {
        teamBCartel_Locked.SetActive(true);
        teamACartel.SetActive(false);
        yield return new WaitForSeconds(5f);
        teamBCartel_Locked.SetActive(false);
        yield break;
    }

    [PunRPC]
    public void UnlockInteractable() 
    {
        teamACartel_Locked.SetActive(false);
        teamBCartel_Locked.SetActive(false);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
    {
       if (stream.IsWriting)
        {
            stream.SendNext(slider.fillAmount);
            stream.SendNext(slider2.fillAmount);
        }
        
       
      else if (stream.IsReading)
        {
           slider.fillAmount =(float)stream.ReceiveNext();
           slider2.fillAmount =(float)stream.ReceiveNext();
        }
      
      }        
}