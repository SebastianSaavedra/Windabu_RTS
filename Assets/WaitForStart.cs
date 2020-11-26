using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.MaluCompany.TestGame;

public class WaitForStart : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(waitfor());
    }
    IEnumerator waitfor() 
    {
        GetComponent<TEST_Movement>().enabled = false;
        yield return new WaitUntil(()=> ManagerMinijuegos.start==true);
        GetComponent<TEST_Movement>().enabled = true;
        yield break;
    }
}
