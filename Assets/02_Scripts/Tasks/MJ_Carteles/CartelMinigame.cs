using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartelMinigame : MonoBehaviour
{
    BoxCollider2D col2d;
    //Rigidbody2D rb2d;
    Coroutine sgteCor;

    int esquinas;
    private void Awake()
    {
        //rb2d.GetComponent<Rigidbody2D>();
        col2d.GetComponent<BoxCollider2D>();
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
            Debug.Log("Esquina Pegada");
            esquinas++;
            collision.GetComponent<BoxCollider2D>().enabled = false;

            if (esquinas >= 4)
            {
                Debug.Log("Llego hasta esta linea.");
                sgteCor = StartCoroutine("SgteCartel");
                esquinas = 0;
            }
        }
    }

    IEnumerator SgteCartel()
    {
        Debug.Log("Entro a la corutina o.0!!!");
        Minijuegos.m_cartel(1);
        yield return new WaitForSeconds(.25f);
        Debug.Log("PASO EL WAIT FOR SECONDS");
        Destroy(gameObject);
        yield break;
    }
}
