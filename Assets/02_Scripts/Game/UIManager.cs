using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class UIManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI moneyText2;
    [SerializeField] GameObject teamAHudParent;
    [SerializeField] GameObject teamBHudParent;
    [SerializeField] GameObject objectToSpawn;
    public int counterA;
    public int counterB; 
    public static List<Player> playersActuales = new List<Player>();
    void Update()
    {
        moneyText.text = MinigameManager.dinero.ToString();
        moneyText2.text = MinigameManager.dinero.ToString();
    }
    Player TargetPlayerByActorNumber(int playerActorNumber)
    {
        //Solamente se tiene para mostrar el número de jugadores y su actorNumber
        foreach (var item in playersActuales)
        {
            Debug.Log("Players: " + item.ActorNumber);
        }

        //Determina a qué jugador debemos enviarle el RPC según su actor number 
        //(identificador de cada jugador en esta sala de juego)
        Player playerToReturn = null;
        for (int i = 0; i < playersActuales.Count; i++)
        {
            if (playersActuales[i].ActorNumber == playerActorNumber)
            {
                playerToReturn = playersActuales[i];
                break;
            }
        }
        return playerToReturn;
    }
    public void RPCInstantiateInTeamA() 
    {
            photonView.RPC("InstantiateInTeamA",RpcTarget.AllViaServer);
         foreach(int x in PhotonManager.teamA_id) 
        {
            if (x != 0) 
            {
                Debug.Log("Bruhhhhhhhh");
                Debug.Log(x);
            }
        }
    }

    public void InstantiateInTeamB()
    {

    }

    [PunRPC]
    public void InstantiateInTeamA()
    {
        GameObject objectToQue = Instantiate(objectToSpawn, teamAHudParent.transform.position, objectToSpawn.transform.rotation);
        objectToQue.transform.parent = teamAHudParent.transform;
    }
}
