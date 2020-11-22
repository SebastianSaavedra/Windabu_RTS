using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMovement : MonoBehaviour
{
    public GameObject punto0, punto1;

    public bool scored;
    public float speed;
    public bool moving;

    private void Start()
    {
        scored = false;
    }

    private void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, punto1.transform.position, speed * Time.deltaTime);

            if(transform.position == punto1.transform.position)
            {
                transform.position = punto0.transform.position;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && moving)
        {
            StartCoroutine(Stopped());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        scored = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        scored = false;
    }

    IEnumerator Stopped()
    {
        moving = false;
        yield return new WaitForSeconds(0.1f);

        if (scored)
        {
            Debug.Log("Wena");
        }
        else
        {
            Debug.Log("Ksi");
        }

        yield return new WaitForSeconds(1.5f);

        transform.position = punto0.transform.position;
        moving = true;
    }
}
