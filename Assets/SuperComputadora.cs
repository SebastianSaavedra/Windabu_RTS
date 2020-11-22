using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperComputadora : MonoBehaviour
{
    [SerializeField] bool team;
    public float timer;
    public int moneyToGive;

    private void Start()
    {
        if (team) 
        {
            StartCoroutine(GiveMoneyA());
        }
        else 
        {
            StartCoroutine(GiveMoneyB());
        }
    }

    IEnumerator GiveMoneyA()
    {
        yield return new WaitForSeconds(timer);
        Minijuegos.m_clicksA(moneyToGive);
        StartCoroutine(GiveMoneyA());
        yield break;
    }
    IEnumerator GiveMoneyB()
    {
        yield return new WaitForSeconds(timer);
        Minijuegos.m_clicksB(moneyToGive);
        StartCoroutine(GiveMoneyB());
        yield break;
    }
}
