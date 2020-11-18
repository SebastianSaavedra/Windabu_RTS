using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightstick : MonoBehaviour
{   
    //privadas
    ManagerMinijuegos managerLocal;
    MinigameManager managerMinigame;
    [SerializeField] CapsuleCollider2D col2d;
    bool estaJugando;
    Transform transInicial;

    Coroutine sgteCor;
    [SerializeField] float velRot;
    [SerializeField] Transform pivot;


    private void Awake()
    {
        col2d.GetComponent<CapsuleCollider2D>();
        managerMinigame = GameObject.Find("MinijuegosManager").GetComponent<MinigameManager>();
        managerLocal = GameObject.Find("MinijuegosManager").GetComponent<ManagerMinijuegos>();
    }

    private void Start()
    {
        transInicial.rotation = transform.rotation;
    }

    private void OnEnable()
    {
        estaJugando = true;
        transform.rotation = transInicial.rotation;
        sgteCor = StartCoroutine("Actividad");
    }

    void Update()
    {
        if (estaJugando)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                col2d.enabled = true;
            }
        }
    }

    public IEnumerator Actividad()
    {
        while (estaJugando)
        {
            pivot.transform.Rotate(new Vector3(0f, 0f, .1f * velRot));

            if (pivot.transform.rotation.eulerAngles == new Vector3(0f, 0f, 70f))
            {
                velRot = 1f;
            }
            else if (pivot.transform.rotation.eulerAngles == new Vector3(0f, 0f, -70f))
            {
                velRot = -1f;
            }
        }
        yield break;
    }

    public void RepetirActividad()
    {
        StartCoroutine(sgteCor.ToString());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Negativo"))
        {
            Debug.Log("Te equivocaste");
        }

        else if (collision.CompareTag("Positivo"))
        {
            Debug.Log("La hiciste mano");
        }
    }

    private void OnDisable()
    {
        estaJugando = false;
    }
}
