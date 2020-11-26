using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGrabController : MonoBehaviour
{
    public Transform boxHolder;
    private GameObject carriedBox;

    private bool hasBox;

    private void Start()
    {
        hasBox = false;
    }

    private void Update()
    {
        if (hasBox)
        {
            carriedBox.transform.position = boxHolder.position;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "MjBox" && !hasBox)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!col.GetComponent<BoxProgression>().inRoom)
                {
                    // Start carrying box
                    hasBox = true;
                    carriedBox = col.gameObject;
                    carriedBox.GetComponent<BoxProgression>().Traveling();
                }
            }
        }

        if(col.tag == "RoomSpace" && hasBox)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hasBox = false;

                //Modify carried box
                carriedBox.transform.position = col.transform.position;
                carriedBox.GetComponent<BoxProgression>().Delivered();
                carriedBox = null;

                //Kill room
                col.gameObject.SetActive(false);
            }
        }
    }
}
