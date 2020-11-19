using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Lightstick : MonoBehaviourPunCallbacks
{   
    //privadas
    ManagerMinijuegos managerLocal;
    MinigameManager managerMinigame;
    bool estaJugando;
    Transform transInicial;
    //int vecesJugado = 1;
    float zRotation = 70;
    Coroutine colCor;

    [SerializeField] CapsuleCollider2D col2d;
    [SerializeField] float velRot;
    [SerializeField] RectTransform pivot;

    private void Awake()
    {
        col2d.GetComponent<CapsuleCollider2D>();
        managerMinigame = GameObject.Find("MinijuegosManager").GetComponent<MinigameManager>();
        managerLocal = GameObject.Find("MinijuegosManager").GetComponent<ManagerMinijuegos>();
        transInicial = pivot.GetComponent<RectTransform>();
    }

    private void Start()
    {
        transInicial.transform.rotation = pivot.transform.rotation;
        //vecesJugado = 0;
    }

    private new void OnEnable()
    {
        estaJugando = true;
        //vecesJugado = 0;
        //sgteCor = StartCoroutine("Actividad");
        Debug.Log("Lightstick se activo");
    }

    void Update()
    {
        if (estaJugando)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                colCor = StartCoroutine("ColliderTimer");
            }
        }
    }

    private void FixedUpdate()
    {
        if (estaJugando)
        {
            if (pivot.rotation.eulerAngles.z < 70)
            {
                Debug.Log("Llego al limite 1");
                velRot = -velRot;
            }
            else if (pivot.rotation.eulerAngles.z < -70)
            {
                Debug.Log("Llego al limite 2");
                velRot = velRot * -1;
            }

            pivot.transform.Rotate(new Vector3(0f, 0f, 1f * velRot * Time.deltaTime));
            Debug.Log(pivot.rotation.eulerAngles.z);
        }
    }

    void RepetirActividadAcelerado()
    {
        //pivot.transform.rotation = transInicial.transform.rotation;
        velRot += velRot;
        Debug.Log("se reinicio la actividad con mayor velocidad");
    }
    void RepetirActividad()
    {
        //pivot.transform.rotation = transInicial.transform.rotation;
        //velRot += velRot;
        Debug.Log("se reinicio la actividad");
    }

    IEnumerator ColliderTimer()
    {
        col2d.enabled = true;
        yield return new WaitForSeconds(.1f);
        col2d.enabled = false;
        yield break;
    }

    //public IEnumerator Actividad()
    //{
    //    yield break;
    //}

    //public void RepetirActividad()
    //{
    //    StartCoroutine(sgteCor.ToString());
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Negativo"))
        {
            Debug.Log("Te equivocaste");
            //activa icono de fallar
            RepetirActividad();
        }

        else if (collision.CompareTag("Positivo"))
        {
            Debug.Log("La hiciste mano");
            //activa icono de conseguido
            //
            RepetirActividadAcelerado();
        }
    }

    private new void OnDisable()
    {
        estaJugando = false;
    }
}
