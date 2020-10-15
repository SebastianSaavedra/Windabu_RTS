using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Interact : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Entro al area");
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Agarro la pocion");
            Destroy(gameObject);
        }
    }
}
