using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using DG.Tweening;

public class BehindRenderShader : MonoBehaviourPunCallbacks         //Twekear shader
{

    //[SerializeField] GameObject camara;
    //[SerializeField] GameObject target;
    //[SerializeField] LayerMask layerMask;
    [SerializeField] GameObject player;

    #region Raycast2D Fail
    //private void Update()
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(camara.transform.position, (target.transform.position - camara.transform.position).normalized, Mathf.Infinity, layerMask);

    //    if (hit.collider.gameObject.tag == "EsferaMask")
    //    {
    //        Debug.Log("Hiteo al circulo");
    //        target.transform.DOScale(0, .5f);
    //    }

    //    else
    //    {
    //        Debug.Log("Esta hiteando: " + hit.collider.name);
    //        target.transform.DOScale(.5f, .5f);
    //    }
    //}
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Edificio"))
        {
            if (player.GetComponent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                Debug.Log("Hiteo a: " + collision.name);
                gameObject.transform.DOScale(1f, 1f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Edificio"))
        {
            if (player.GetComponent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                Debug.Log("Salio del collider");
                this.transform.DOScale(0, 1f);
            }
        }
    }
}
