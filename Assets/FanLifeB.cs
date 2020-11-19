using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanLifeB : MonoBehaviour
{
    public bool piedra, tijera, papel;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlazaCentralManager>()) 
        {
            if (piedra) 
            {
                StartCoroutine(CalculateFanLifePiedra());
            }
            if (tijera) 
            {
                StartCoroutine(CalculateFanLifePapel());
            }
            if (papel) 
            {
            StartCoroutine(CalculateFanLifePapel());
            }
        }
    }

    IEnumerator CalculateFanLifePiedra() 
    {
        float timer;
        timer = 60;
        timer = 50 * (100 / (100 + PlazaCentralManager.Papel_A));
        Debug.Log(timer);
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
        yield break;
    }
    IEnumerator CalculateFanLifeTijera()
    {
        float timer;
        timer = 60;
        timer = 50 * (100 / (100 + PlazaCentralManager.Piedra_A));
        Debug.Log(timer);
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
        yield break;
    }
    IEnumerator CalculateFanLifePapel()
    {
        float timer;
        timer = 60;
        timer = 50 * (100 / (100 + PlazaCentralManager.Tijeras_A));
        Debug.Log(timer);
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
        yield break;
    }

}
