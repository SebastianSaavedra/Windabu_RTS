using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPManager : MonoBehaviour
{
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
        gameObject.GetComponent<SpriteRenderer>().color = colorTeam1;
    }


    //Change to team2
    public void Team2()
    {
        gameObject.GetComponent<SpriteRenderer>().color = colorTeam2;
    }
}