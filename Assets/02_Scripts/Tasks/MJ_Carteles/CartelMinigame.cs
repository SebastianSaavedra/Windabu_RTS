using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartelMinigame : MonoBehaviour
{
    [SerializeField] BoxCollider2D col2d;
    //Rigidbody2D rb2d;
    Coroutine sgteCor;
    [SerializeField] Cartel cartel;
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
        //Debug.Log("Entro a la corutina");
        Minijuegos.m_cartel(1);
        cartel.hayCartel = false;
        yield return new WaitForSeconds(.25f);
        Destroy(cartel.cartelito);
        yield break;
    }
}
