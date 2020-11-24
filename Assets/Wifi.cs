using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wifi : MonoBehaviour
{
    [SerializeField] GameObject celu,wifi, wifint;
    [SerializeField] Animator celuAnim;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                celuAnim.SetTrigger("OpenCell");
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                celuAnim.SetTrigger("CloseCell");
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
