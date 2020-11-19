using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
public class PlazaCentralManager : MonoBehaviourPunCallbacks,IPunObservable
{
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
    [SerializeField] int papelB;
    [SerializeField] TextMeshProUGUI ta, tb, pia, pib, paa, pab;

    private void Awake()
    {
        PhotonNetwork.AddCallbackTarget(this);
        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 10;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            RPCCallFan();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            RPCKillFan();
        }
        tijerasA = Tijeras_A;
        ta.text = "" + tijerasA;
        tijerasB = Tijeras_B;
        tb.text = "" + tijerasB;
        piedrasA = Piedra_A;
        pia.text = "" + piedrasA;
        piedrasB = Piedra_B;
        pib.text = "" + piedrasB;
        papelA   = Papel_A;
        paa.text = "" + papelA;
        papelB   = Papel_B;
        pab.text = "" + papelB;
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
            stream.SendNext(Tijeras_A);
            stream.SendNext(Tijeras_B);
            stream.SendNext(Piedra_A);
            stream.SendNext(Piedra_B);
            stream.SendNext(Papel_A);
            stream.SendNext(Papel_B);
        }
        else if (stream.IsReading)
        {
            Tijeras_A = (int)stream.ReceiveNext();
            Tijeras_B = (int)stream.ReceiveNext();
            Piedra_A  = (int)stream.ReceiveNext();
            Piedra_B  = (int)stream.ReceiveNext();
            Papel_A   = (int)stream.ReceiveNext();
            Papel_B   = (int)stream.ReceiveNext();
        }
    }
}
