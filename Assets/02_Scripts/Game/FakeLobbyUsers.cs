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
    public static TextMeshProUGUI[] users2;   
    [SerializeField] TextMeshProUGUI[] usersVar2;

    private void Start()
    {
        users = usersVar;
        users2 = usersVar2;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
