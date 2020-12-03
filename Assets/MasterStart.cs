using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class MasterStart : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject startBut;
    // Start is called before the first frame update
public void StartGAME() 
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startBut.SetActive(true);
        }
    }
}
