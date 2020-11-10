using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamManager : MonoBehaviourPunCallbacks
{
    public float money;
    public float fameA;
    public float fameB;
    [SerializeField] TextMeshProUGUI text;


    private void Start()
    {
        StartCoroutine(SelectWinnerA());
        StartCoroutine(SelectWinnerB());
    }
    IEnumerator SelectWinnerA() 
    {
        yield return new WaitUntil(() => fameA >= 50);
        print("TeamA Gano");
        photonView.RPC("WinA", RpcTarget.AllViaServer);
        StopAllCoroutines();
        yield break;
    }
    IEnumerator SelectWinnerB()
    {
        yield return new WaitUntil(() => fameB >= 50);
        photonView.RPC("WinB", RpcTarget.AllViaServer);
        print("TeamB Gano");
        StopAllCoroutines();
        yield break;
    }
    [PunRPC]
    public void WinA() 
    {
        text.gameObject.SetActive(true);
        text.text = "Team A gano";
    }
    [PunRPC]
    public void WinB()
    {
        text.gameObject.SetActive(true);
        text.text = "Team B gano";
    }
}
