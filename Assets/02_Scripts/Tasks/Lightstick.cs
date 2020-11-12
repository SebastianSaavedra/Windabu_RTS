using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightstick : MonoBehaviour
{
    CapsuleCollider2D col2d;

    [SerializeField] float velRot;

    void Start()
    {
        col2d.GetComponent<CapsuleCollider2D>();
    }


    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, .1f * velRot));

        if (transform.rotation.eulerAngles == new Vector3(0f,0f,70f))
        {
            velRot = 1f;
        }
        else if (transform.rotation.eulerAngles == new Vector3(0f, 0f, -70f))
        {
            velRot = -1f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            col2d.enabled = true;
        }
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
}
