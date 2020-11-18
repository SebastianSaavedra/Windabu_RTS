using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static int[] teamA_id;
    public static int[] teamB_id;
    public int[] teamA_Id_Var;
    public int[] teamB_Id_Var;
    void Start()
    {
        teamA_id = new int[3];
        teamB_id = new int[3];
    }    

    // Update is called once per frame
    void Update()
    {
        teamA_Id_Var = teamA_id;     
        teamB_Id_Var = teamB_id;     
    }
    
}
