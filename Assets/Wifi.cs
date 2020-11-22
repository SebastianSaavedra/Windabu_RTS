using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wifi : MonoBehaviour
{
    [SerializeField] GameObject celu,wifi, wifint;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                celu.SetActive(true);
            }
            wifint.SetActive(false);
            wifi.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            wifi.SetActive(false);
            wifint.SetActive(true);
        }
    }
}
