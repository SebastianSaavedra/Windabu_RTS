using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackTrigger : MonoBehaviour
{
    public GameObject eKey;
    public GameObject tKey;
    public GameObject fKey;
    public GameObject rKey;
    public GameObject escKey;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<TaskDropDown>()) 
        {
            eKey.SetActive(true);
            escKey.SetActive(true);
        }
        if (collision.GetComponent<ColSaver>()) 
        {
            if (GetComponent<PlayerTeam>().TeamA && collision.CompareTag("Actividad B"))
            {
              //  fKey.SetActive(true);
            }
            if (GetComponent<PlayerTeam>().TeamB && collision.CompareTag("Actividad A"))
            {
               // fKey.SetActive(true);
            }
            if (GetComponent<PlayerTeam>().TeamA && collision.CompareTag("Actividad A"))
            {
            escKey.SetActive(true);
            eKey.SetActive(true);
                rKey.SetActive(true);
                tKey.SetActive(true);
            }
            if (GetComponent<PlayerTeam>().TeamB && collision.CompareTag("Actividad B"))
            {
            escKey.SetActive(true);
            eKey.SetActive(true);
                //rKey.SetActive(true);
                //tKey.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<TaskDropDown>())
        {
            eKey.SetActive(false);
            escKey.SetActive(false);
        }
        if (collision.GetComponent<ColSaver>())
        {
            if (GetComponent<PlayerTeam>().TeamA && collision.CompareTag("Actividad B"))
            {
                fKey.SetActive(false);
            }
            if (GetComponent<PlayerTeam>().TeamB && collision.CompareTag("Actividad A"))
            {
                fKey.SetActive(false);
            }
            if (GetComponent<PlayerTeam>().TeamA && collision.CompareTag("Actividad A"))
            {
                eKey.SetActive(false);
                escKey.SetActive(false);
                rKey.SetActive(false);
                tKey.SetActive(false);
            }
            if (GetComponent<PlayerTeam>().TeamB && collision.CompareTag("Actividad B"))
            {
                eKey.SetActive(false);
                escKey.SetActive(false);
                rKey.SetActive(false);
                tKey.SetActive(false);
            }
        }
    }
}
