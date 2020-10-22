using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPManager : MonoBehaviour
{
    [SerializeField]
    GameObject teamManager1, teamManager2;

    [SerializeField]
    float fameAdded;

    bool blueControlling;

    #region Testing Code
    Color colorNeutral, colorTeam1, colorTeam2;

    public void Awake()
    {
        colorNeutral = gameObject.GetComponent<SpriteRenderer>().color;

        colorTeam1 = Color.blue;
        colorTeam2 = Color.red;
    }
    #endregion

    //Change to team1
    public void Team1()
    {
        blueControlling = true;
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
            teamManager1.GetComponent<TeamManager>().fame += fameAdded;
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(10f);
        GainPoints();
        yield break;
    }
}