using Com.MaluCompany.TestGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerId : MonoBehaviourPunCallbacks
{
    public int id;

    private void Start()
    {
        id= photonView.OwnerActorNr;
    }
}
