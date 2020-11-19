using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanLifeB : MonoBehaviour
{
    public bool piedra, tijera, papel;
    PlazaCentralManager plaza;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlazaCentralManager>()) 
        {
            plaza = collision.GetComponent<PlazaCentralManager>();
            if (piedra) 
            {
               plaza.AddFan(4);
                StartCoroutine(CalculateFanLifePiedra());
            }
            if (tijera) 
            {
               plaza.AddFan(3);
                StartCoroutine(CalculateFanLifeTijera());
            }
            if (papel) 
            {
               plaza.AddFan(5);
            StartCoroutine(CalculateFanLifePapel());
            }
        }
    }

    IEnumerator CalculateFanLifePiedra() 
    {
        float timer;
        timer = 60;
        timer = 50 * (100 / (100 + (float)PlazaCentralManager.Papel_A));
        Debug.Log("Papel A" + PlazaCentralManager.Papel_A);
        Debug.Log(timer);
        yield return new WaitForSeconds(timer);
        plaza.KillFan(4);
        Destroy(gameObject);
        yield break;
    }
    IEnumerator CalculateFanLifeTijera()
    {
        float timer;
        timer = 60;
        timer = 50 * (100 / (100 + (float)PlazaCentralManager.Piedra_A));
        Debug.Log("Piedras A "+PlazaCentralManager.Piedra_A);
        Debug.Log(timer);
        yield return new WaitForSeconds(timer);
        plaza.KillFan(3);
        Destroy(gameObject);
        yield break;
    }
    IEnumerator CalculateFanLifePapel()
    {
        float timer;
        timer = 60;
        timer = 50 * (100 / (100 + (float)PlazaCentralManager.Tijeras_A));
        Debug.Log("Tijeras " + PlazaCentralManager.Tijeras_A);
        Debug.Log(timer);
        yield return new WaitForSeconds(timer);
        plaza.KillFan(5);
        Destroy(gameObject);
        yield break;
    }

}
