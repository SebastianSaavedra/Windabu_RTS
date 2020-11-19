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
        if (PhotonNetwork.IsMasterClient)   //Tener en mente que este codigo puede producir errores a futuro.
        {
            //SetearUIMasterClient();
            playersActuales.Add(PhotonNetwork.LocalPlayer);
            Debug.Log("On joined room ha sido debugea2 (?");

        }
    }
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
        photonView.RPC("InstantiateInTeamA", RpcTarget.AllViaServer,0);
        //photonView.RPC("InstantiateInTeamA", TargetPlayerByActorNumber(PhotonManager.teamA_id[0]));
        //photonView.RPC("InstantiateInTeamA", TargetPlayerByActorNumber(PhotonManager.teamA_id[1]));
        //photonView.RPC("InstantiateInTeamA", TargetPlayerByActorNumber(PhotonManager.teamA_id[2]));
    }
    public void RPCInstantiateInTeamB()
    {
        photonView.RPC("InstantiateInTeamA", RpcTarget.AllViaServer);
        //photonView.RPC("InstantiateInTeamB", TargetPlayerByActorNumber(PhotonManager.teamB_id[0]));
        //photonView.RPC("InstantiateInTeamB", TargetPlayerByActorNumber(PhotonManager.teamB_id[1]));
        //photonView.RPC("InstantiateInTeamB", TargetPlayerByActorNumber(PhotonManager.teamB_id[2]));
    }

    public void InstantiateInTeamB(int toSpawn)
    {
        if (counterA < 4)
        {
            GameObject objectToQue = Instantiate(objectToSpawn, teamBHudParentTop.transform.position, objectToSpawn.transform.rotation);
            objectToQue.transform.parent = teamBHudParentTop.transform;
            objectToQue.transform.localScale = teamBHudParentTop.transform.localScale;
            objectToQue.GetComponent<QueueObjLogic>().ChangeAppearence(toSpawn);
        }
        else
        {
            GameObject objectToQue = Instantiate(objectToSpawn, teamBHudParentDown.transform.position, objectToSpawn.transform.rotation);
            objectToQue.transform.parent = teamBHudParentDown.transform;
            objectToQue.transform.localScale = teamBHudParentDown.transform.localScale;
            objectToQue.GetComponent<QueueObjLogic>().ChangeAppearence(toSpawn);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        playersActuales.Add(newPlayer);
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

}
