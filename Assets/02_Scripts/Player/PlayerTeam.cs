using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : MonoBehaviour
{
    public bool TeamA;
    public bool TeamB;
    public bool team;

    private void Update()
    {
        if (TeamA==true && TeamB==false) 
        {
            team = true;
        }
        if (TeamA == false && TeamB == true)
        {
            team = false;
        }
    }
}
