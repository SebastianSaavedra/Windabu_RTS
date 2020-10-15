using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Interact : MonoBehaviour
{
    [SerializeField]
    GameObject objectToInteract;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectToInteract.GetComponent<I_Interactable>()!=null)
            {
                objectToInteract.GetComponent<I_Interactable>().OnInteract();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            objectToInteract.GetComponent<I_Interactable>().OnLeavePanel();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectToInteract = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        objectToInteract.GetComponent<I_Interactable>().OnLeavePanel();
        objectToInteract = null;
    }

}
