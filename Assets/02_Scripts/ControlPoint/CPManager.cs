using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class CPManager :MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject teamManager1, teamManager2;

    [SerializeField]
    float fameAdded;
    [SerializeField]
    Slider slider,slider2;

    bool blueControlling;
    PhotonView PV;
    #region Testing Code
    Color colorNeutral, colorTeam1, colorTeam2;

    public void Awake()
    {
        PV = GetComponent<PhotonView>();
        colorNeutral = gameObject.GetComponent<SpriteRenderer>().color;

        colorTeam1 = Color.blue;
        colorTeam2 = Color.red;
    }
    #endregion

    //Change to team1
    public void Team1()
    {
        blueControlling = true;
        //PV.RPC("ChangeMat", RpcTarget.AllBuffered, this.gameObject.GetComponent<SpriteRenderer>().color);
        gameObject.GetComponent<SpriteRenderer>().color = colorTeam1;
        GainPoints();
    }

    //Change to team2
    public void Team2()
    {
        blueControlling = false;
        gameObject.GetComponent<SpriteRenderer>().color = colorTeam2;
        GainPoints();
    }

    public void GainPoints()
    {
        if(blueControlling)
        {
            teamManager1.GetComponent<TeamManager>().fameA += fameAdded;
            slider.value += fameAdded;
            StartCoroutine(Wait());
        }
        else 
        {
            teamManager1.GetComponent<TeamManager>().fameB += fameAdded;
            slider2.value += fameAdded;
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(10f);
        GainPoints();
        yield break;
    }

    [PunRPC]
    void ChangeMat(GameObject sprite) 
    {
        sprite = sprite.GetComponent<SpriteRenderer>().gameObject;
    }

}