using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartelMinigame : MonoBehaviour
{
    Collider2D col2d;
    Rigidbody2D rb2d;
    Coroutine sgteCor;

    int esquinas;
    private void Awake()
    {
        rb2d.GetComponent<Rigidbody2D>();
        col2d.GetComponent<Collider2D>();
    }
    private void Start()
    {
        esquinas = 0;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Esquina"))
        {
            // Esquina es pegada;
            esquinas++;
            collision.GetComponent<Collider2D>().enabled = false;
        }

        else if(collision.CompareTag("Esquina") && esquinas >= 4)
        {
            sgteCor = StartCoroutine("SgteCartel");
            esquinas = 0;
        }
    }

    IEnumerator SgteCartel()
    {
        Minijuegos.m_cartel(1);
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);
        yield break;
    }
}
