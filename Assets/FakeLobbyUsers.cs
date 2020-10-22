using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FakeLobbyUsers : MonoBehaviourPunCallbacks
{
    public static TextMeshProUGUI[] users;   
    [SerializeField] TextMeshProUGUI[] usersVar;

    private void Start()
    {
        users = usersVar;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
