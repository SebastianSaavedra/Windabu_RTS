using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxProgression : MonoBehaviour
{
    [HideInInspector]
    public bool inRoom;
    bool inLvl1;

    // Tras comprar - Listo para armar - Disponible para producir - Mejorado
    public GameObject born, ready, upgraded;

    private void Start()
    {
        inRoom = false;
        inLvl1 = false;

        born.SetActive(true);
        born.GetComponent<TaskDropDown>().enabled = false;

        ready.SetActive(false);
        upgraded.SetActive(false);
    }

    public void Traveling()
    {
        GetComponentInChildren<BoxCollider2D>().enabled = false;
    }

    public void Delivered()
    {
        inRoom = true;
        born.GetComponent<TaskDropDown>().enabled = true;
    }
}
