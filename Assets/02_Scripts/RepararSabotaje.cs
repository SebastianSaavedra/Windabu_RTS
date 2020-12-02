using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class RepararSabotaje : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject boxcol2d;


    private void OnTriggerStay2D(Collider2D collision)
    {
            if (collision.GetComponent<ColSaver>().canRepair) 
            {

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            if (GetComponent<PlayerTeam>().TeamA && boxcol2d.GetComponent<ColSaver>().team)
            {
                photonView.RPC("LlamarCorutinaReparar", RpcTarget.AllViaServer);
            }
            if (GetComponent<PlayerTeam>().TeamB && !boxcol2d.GetComponent<ColSaver>().team)
            {
                photonView.RPC("LlamarCorutinaReparar", RpcTarget.AllViaServer);
            }
            }
        }


    }

    [PunRPC]
    void LlamarCorutinaReparar()
    {
        StartCoroutine("Reparar");
    }

    [PunRPC]
    IEnumerator Reparar()
    {
        Debug.Log("Empezo a reparar");
        yield return new WaitForSeconds(5f);
        boxcol2d.GetComponent<ColSaver>().RPCEnable();
        Debug.Log("Ya Reparo");
        yield break;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ColSaver>()) 
        {
            boxcol2d = collision.gameObject;
        }
    }

}
