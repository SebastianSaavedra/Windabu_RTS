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
    [SerializeField] GameObject fake;
    [SerializeField] GameObject GanoA;
    [SerializeField] GameObject GanoB;


    private void Start()
    {
        StartCoroutine(SelectWinnerA());
        StartCoroutine(SelectWinnerB());
    }
    IEnumerator SelectWinnerA() 
    {
        yield return new WaitUntil(() => fameA >= 2);
        print("TeamA Gano");
        photonView.RPC("WinA", RpcTarget.AllViaServer);
        StopAllCoroutines();
        yield break;
    }
    IEnumerator SelectWinnerB()
    {
        yield return new WaitUntil(() => fameB >= 2);
        photonView.RPC("WinB", RpcTarget.AllViaServer);
        print("TeamB Gano");
        StopAllCoroutines();
        yield break;
    }
    [PunRPC]
    public void WinA() 
    {
        fake.SetActive(true);
        GanoA.SetActive(true);
    }
    [PunRPC]
    public void WinB()
    {
        fake.SetActive(true);
        GanoB.SetActive(true);
    }
}
