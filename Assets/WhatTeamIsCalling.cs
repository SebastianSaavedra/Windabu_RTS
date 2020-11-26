using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
public class WhatTeamIsCalling : MonoBehaviourPunCallbacks
{
    public bool team;
    public int id;

    public bool mjFinished;

    public bool Finish()
    {
        return mjFinished;
    }

    
}
