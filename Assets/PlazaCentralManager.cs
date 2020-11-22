using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
public class PlazaCentralManager : MonoBehaviourPunCallbacks,IPunObservable
{
    [SerializeField]
    GameObject teamManager1;
    [SerializeField]
    float fameAdded;
    [SerializeField]
    Image slider, slider2;
    [SerializeField]
    float timer;
    public static int Tijeras_A;
    [SerializeField] int tijerasA;
    public static int Tijeras_B;
    [SerializeField] int tijerasB;
    public static int Piedra_A;
    [SerializeField] int piedrasA;
    public static int Piedra_B;
    [SerializeField] int piedrasB;
    public static int Papel_A;
    [SerializeField] int papelA;
    public static int Papel_B;
    [SerializeField] int totalA, totalB;
    [SerializeField] int papelB;
    [SerializeField] TextMeshProUGUI ta, tb, pia, pib, paa, pab;
    [SerializeField] GameObject midPointA, midPointB;
    [SerializeField] bool ControlledBy;
    bool corAcalled;
    bool corBcalled;


    private void Awake()
    {
        PhotonNetwork.AddCallbackTarget(this);
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 10;
    }
    void Update()
    {
        #region UI
        totalA = Tijeras_A + Piedra_A + Papel_A;
        totalB = Tijeras_B + Piedra_B + Papel_B;
        tijerasA = Tijeras_A;
        ta.text = "" + tijerasA;
        tijerasB = Tijeras_B;
        tb.text = "" + tijerasB;
        piedrasA = Piedra_A;
        pia.text = "" + piedrasA;
        piedrasB = Piedra_B;
        pib.text = "" + piedrasB;
        papelA = Papel_A;
        paa.text = "" + papelA;
        papelB = Papel_B;
        pab.text = "" + papelB;
        #endregion
        if ((totalA > totalB) && !corAcalled) 
        {
            photonView.RPC("RPCcallCorA", RpcTarget.MasterClient);
            photonView.RPC("RPCStopCorB", RpcTarget.MasterClient);
            photonView.RPC("RPCActivateTeamA", RpcTarget.AllViaServer,true);
            corAcalled = true;
            corBcalled = false;
        }
       else if ((totalA < totalB) && !corBcalled) 
        {
            photonView.RPC("RPCcallCorB", RpcTarget.MasterClient);
            photonView.RPC("RPCStopCorA", RpcTarget.MasterClient);
            photonView.RPC("RPCActivateTeamB", RpcTarget.AllViaServer,false);
            corBcalled = true;
            corAcalled = false;
        }

    }

    public void RPCCallFan() 
    {
        photonView.RPC("AddFan", RpcTarget.MasterClient, 0);
    }

    public void RPCKillFan() 
    {
        photonView.RPC("KillFan", RpcTarget.MasterClient, 0);
    }
    [PunRPC]
    public void GiveA()
    {
        teamManager1.GetComponent<TeamManager>().fameA += fameAdded;
        slider.fillAmount += (fameAdded);
    }
    [PunRPC]
    public void GiveB()
    {
        teamManager1.GetComponent<TeamManager>().fameB += fameAdded;
        slider2.fillAmount += (fameAdded);
    }

    [PunRPC]
    void RPCActivateTeamA(bool controlledBy)
    {
        ControlledBy = controlledBy;
        midPointA.SetActive(true);
        midPointB.SetActive(false);
    }
    [PunRPC]
    void RPCActivateTeamB(bool controlledBy)
    {
        ControlledBy = controlledBy;
        midPointA.SetActive(false);
        midPointB.SetActive(true);
    }
    [PunRPC]
    public void RPCcallCorA()
    {
        StartCoroutine(GivePointsA());
    }
    [PunRPC]
    public void RPCcallCorB()
    {
        StartCoroutine(GivePointsB());
    }
    [PunRPC]
    public void RPCStopCorA()
    {
        StopCoroutine(GivePointsA());
    }
    [PunRPC]
    public void RPCStopCorB()
    {       
        StopCoroutine(GivePointsB());
    }
    [PunRPC]
    IEnumerator GivePointsA()
    {
        yield return new WaitForSeconds(timer);
        photonView.RPC("GiveA", RpcTarget.MasterClient);
        if (ControlledBy) 
        {
        StartCoroutine(GivePointsA());
        }
        else 
        {
            StopCoroutine(GivePointsA());
        }
        yield break;
    }

    [PunRPC]
    IEnumerator GivePointsB()
    {
        yield return new WaitForSeconds(timer);
        photonView.RPC("GiveB", RpcTarget.MasterClient);
        if (!ControlledBy)
        {
            StartCoroutine(GivePointsB());
        }
        else
        {
            StopCoroutine(GivePointsB());
        }
        yield break;
    }
    [PunRPC]
    public void AddFan(int whatFan) 
    {        
        switch (whatFan) 
        {
            case 0:
               Tijeras_A++;
            break; 
                
            case 1:
              Piedra_A++;
            break;

            case 2:
                Papel_A++;
                break;

            case 3:
                Tijeras_B++;
                break;

            case 4:
                Piedra_B++;
                break;

            case 5:
                Papel_B++;
                break;
        }               
    }
    [PunRPC]
    public void KillFan(int whatFan) 
    {
        switch (whatFan)
        {
            case 0:
                Tijeras_A--;
                break;

            case 1:
                Piedra_A--;
                break;

            case 2:
                Papel_A--;
                break;

            case 3:
                Tijeras_B--;
                break;

            case 4:
                Piedra_B--;
                break;

            case 5:
                Papel_B--;
                break;
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(totalA);
            stream.SendNext(totalB);
            stream.SendNext(Tijeras_A);
            stream.SendNext(Tijeras_B);
            stream.SendNext(Piedra_A);
            stream.SendNext(Piedra_B);
            stream.SendNext(Papel_A);
            stream.SendNext(Papel_B);
        }
        else if (stream.IsReading)
        {
            totalA    = (int)stream.ReceiveNext();
            totalB    = (int)stream.ReceiveNext();
            Tijeras_A = (int)stream.ReceiveNext();
            Tijeras_B = (int)stream.ReceiveNext();
            Piedra_A  = (int)stream.ReceiveNext();
            Piedra_B  = (int)stream.ReceiveNext();
            Papel_A   = (int)stream.ReceiveNext();
            Papel_B   = (int)stream.ReceiveNext();
        }
    }
}
