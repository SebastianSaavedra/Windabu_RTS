using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class RepararSabotaje : MonoBehaviourPunCallbacks
{
    [SerializeField] List<BoxCollider2D> boxCollider2Ds = new List<BoxCollider2D>();
    bool enBase;
    BoxCollider2D colliderDesactivado;

    private void Start()
    {
        foreach (GameObject col in GameObject.FindGameObjectsWithTag("Actividad B"))
        {
            boxCollider2Ds.Add(col.GetComponent<BoxCollider2D>());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Base") && !enBase)
        {
            enBase = true;
        }

        if (enBase)
        {
            Debug.Log("Esta en la base");
            photonView.RPC("BuscarCollider",RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    void BuscarCollider()
    {
        foreach (BoxCollider2D item in boxCollider2Ds)
        {
            if (item.enabled == false)
            {
                Debug.Log("El collider desactivado es: " + item);
                colliderDesactivado = item;
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        photonView.RPC("LlamarCorutinaReparar", RpcTarget.AllViaServer);
                    }
                }
            }
        }
    }

    [PunRPC]        //Llamarlo desde otros scripts
    public void ActualizarListaDeColliders(BoxCollider2D col)
    {
        boxCollider2Ds.Add(col);
    }

    [PunRPC]
    void LlamarCorutinaReparar()
    {
        StartCoroutine("Reparar");
    }


    IEnumerator Reparar()
    {
        Debug.Log("Empezo a reparar");
        yield return new WaitForSeconds(5f);
        colliderDesactivado.enabled = true;
        Debug.Log("Ya Reparo");
        yield break;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enBase = false;
        Debug.Log("El estado del bool de la base es: " + enBase);
    }
}
