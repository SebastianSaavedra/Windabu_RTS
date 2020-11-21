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
    [SerializeField] GameObject teamAHudParentTop;
    [SerializeField] GameObject teamAHudParentDown;
    [SerializeField] GameObject teamBHudParentTop;
    [SerializeField] GameObject teamBHudParentDown;
    [SerializeField] GameObject objectToSpawn;
    public int counterA;
    public int counterB; 
    public static List<Player> playersActuales = new List<Player>();


    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playersActuales.Add(PhotonNetwork.LocalPlayer);
        }
    }
    void Update()
    {
        moneyText.text = MinigameManager.dineroA.ToString();
        moneyText2.text = MinigameManager.dineroB.ToString();
    }
    #region InstLogic
    public void CallRpc(bool call, int toQue) 
    {
        if (call) 
        {
            RPCInstantiateInTeamA(toQue);
        }
        else 
        {
            RPCInstantiateInTeamB(toQue);
        }
    }
    public void RPCInstantiateInTeamA(int toSpawn)
    {
        photonView.RPC("InstantiateInTeamA", RpcTarget.AllViaServer,toSpawn);
        //photonView.RPC("InstantiateInTeamA", TargetPlayerByActorNumber(PhotonManager.teamA_id[0]));
        //photonView.RPC("InstantiateInTeamA", TargetPlayerByActorNumber(PhotonManager.teamA_id[1]));
        //photonView.RPC("InstantiateInTeamA", TargetPlayerByActorNumber(PhotonManager.teamA_id[2]));
    }
    public void RPCInstantiateInTeamB(int toSpawn)
    {
        photonView.RPC("InstantiateInTeamA", RpcTarget.AllViaServer,toSpawn);
        //photonView.RPC("InstantiateInTeamB", TargetPlayerByActorNumber(PhotonManager.teamB_id[0]));
        //photonView.RPC("InstantiateInTeamB", TargetPlayerByActorNumber(PhotonManager.teamB_id[1]));
        //photonView.RPC("InstantiateInTeamB", TargetPlayerByActorNumber(PhotonManager.teamB_id[2]));
    }
    public void InstantiateInTeamB(int toSpawn)
    {
        if (counterB < 4)
        {
            GameObject objectToQue = Instantiate(objectToSpawn, teamBHudParentTop.transform.position, objectToSpawn.transform.rotation);
            objectToQue.transform.parent = teamBHudParentTop.transform;
            objectToQue.transform.localScale = teamBHudParentTop.transform.localScale;
            objectToQue.GetComponent<QueueObjLogic>().ChangeAppearence(toSpawn);
            objectToQue.GetComponent<QueueObjLogic>().teamB=true;
        }
        else
        {
            GameObject objectToQue = Instantiate(objectToSpawn, teamBHudParentDown.transform.position, objectToSpawn.transform.rotation);
            objectToQue.transform.parent = teamBHudParentDown.transform;
            objectToQue.transform.localScale = teamBHudParentDown.transform.localScale;
            objectToQue.GetComponent<QueueObjLogic>().ChangeAppearence(toSpawn);
            objectToQue.GetComponent<QueueObjLogic>().teamB=true;
        }
    }

    [PunRPC]
    public void InstantiateInTeamA(int toSpawn)
    {
        counterA++;
        if (counterA < 4)
        {
            GameObject objectToQue = Instantiate(objectToSpawn, teamAHudParentTop.transform.position, Quaternion.identity);
            objectToQue.transform.parent = teamAHudParentTop.transform;
            objectToQue.transform.localScale = teamAHudParentTop.transform.localScale;
            objectToQue.GetComponent<QueueObjLogic>().ChangeAppearence(toSpawn);
            objectToQue.GetComponent<QueueObjLogic>().teamA = true;
        }
        else
        {
            GameObject objectToQue = Instantiate(objectToSpawn, teamAHudParentDown.transform.position, Quaternion.identity);
            objectToQue.transform.parent = teamAHudParentDown.transform;
            objectToQue.transform.localScale = teamAHudParentDown.transform.localScale;
            objectToQue.GetComponent<QueueObjLogic>().ChangeAppearence(toSpawn);
            objectToQue.GetComponent<QueueObjLogic>().teamA = true;
        }
    }


    public void DeValueCounterA() 
    {
        photonView.RPC("DeValueCounterARPC", RpcTarget.AllBuffered);
    }

    public void DeValueCounterB()
    {
        photonView.RPC("DeValueCounterBRPC", RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void DeValueCounterARPC()
    {
        counterA--;
    }

    [PunRPC]
    public void DeValueCounterBRPC()
    {
        counterB--;
    }
    #endregion
    Player TargetPlayerByActorNumber(int playerActorNumber)
    {
        //Solamente se tiene para mostrar el número de jugadores y su actorNumber
        foreach (var item in playersActuales)
        {
           
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
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        playersActuales.Add(newPlayer);
    }

}
