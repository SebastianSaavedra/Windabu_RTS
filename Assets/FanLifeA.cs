using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanLifeA : MonoBehaviour
{
    // Start is called before the first frame update
    public bool piedra, tijera, papel;
    PlazaCentralManager plaza;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlazaCentralManager>())
        {
            plaza = collision.GetComponent<PlazaCentralManager>();
            {
            if (piedra)
            {
               plaza.AddFan(1);
                StartCoroutine(CalculateFanLifePiedra());
            }
            if (tijera)
            {
                plaza.AddFan(0);
                StartCoroutine(CalculateFanLifeTijera());
            }
            if (papel)
            {
                plaza.AddFan(2);
                StartCoroutine(CalculateFanLifePapel());
            }
        }
    }

    IEnumerator CalculateFanLifePiedra()
    {
        float timer;
        timer = 60;
        timer = 50 * (100 / (100 + (float)PlazaCentralManager.Papel_B));
        Debug.Log(timer);
        yield return new WaitForSeconds(timer);
            plaza.KillFan(1);
        Destroy(gameObject);
        yield break;
    }
    IEnumerator CalculateFanLifeTijera()
    {
        float timer;
        timer = 60;
        timer = 50 * (100 / (100 + (float)PlazaCentralManager.Piedra_B));
        Debug.Log(timer);
        yield return new WaitForSeconds(timer);
            plaza.KillFan(0);
        Destroy(gameObject);
        yield break;
    }
    IEnumerator CalculateFanLifePapel()
    {
        float timer;
        timer = 60;
        timer = 50 * (100 / (100 + (float)PlazaCentralManager.Tijeras_B));
        Debug.Log(timer);
        yield return new WaitForSeconds(timer);
            plaza.KillFan(2);
        Destroy(gameObject);
        yield break;
    }
}
}
