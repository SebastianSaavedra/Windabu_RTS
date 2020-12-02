using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using DG.Tweening;

public class BehindRenderShader : MonoBehaviourPunCallbacks         //Tweakear shader
{
    public int posID = Shader.PropertyToID("_Position");
    public int sizeID = Shader.PropertyToID("_Size");

    [SerializeField] Camera camara;
    [SerializeField] Material wallMaterial;
    [SerializeField] GameObject player;
    //[SerializeField] GameObject target;
    //[SerializeField] LayerMask layerMask;

    #region Raycast2D Fail
    //private void Start()
    //{
    //    camara = GameObject.Find("Main Camera").GetComponent<Camera>();
    //}
    //private void Update()
    //{
    //    var dir = camara.transform.forward - transform.position;
    //    var ray = new Ray2D(transform.position, dir.normalized);
    //    Debug.DrawRay(ray.origin, ray.direction, Color.red, .1f);

    //    if (Physics2D.Raycast(ray.origin,ray.direction,layerMask))
    //    {
    //        Debug.Log("No hitteo la camara");
    //        wallMaterial.SetFloat(sizeID,1);
    //    }
    //    else
    //    {
    //        Debug.Log("Hitteo la camara");
    //        wallMaterial.SetFloat(sizeID, 0);
    //    }

    //    var view = camara.WorldToViewportPoint(transform.position);
    //    wallMaterial.SetVector(posID,view);


    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    var dir = camara.transform.forward - transform.position;
    //    Gizmos.DrawRay(transform.position, dir.normalized);
    //}
    #endregion

    #region Triggers
    private void Start()
    {
        camara = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    private void Update()
    {
        var view = camara.WorldToViewportPoint(transform.position);
        wallMaterial.SetVector(posID, view);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Edificio"))
        {
            if (player.GetComponent<PlayerId>().id == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                Debug.Log("Hiteo a: " + collision.name);
                wallMaterial.DOFloat(1, sizeID, .5f);
                //wallMaterial.SetFloat(sizeID, Mathf.Lerp(0,1,1));
                //gameObject.transform.DOScale(1f, 1f);
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
                wallMaterial.DOFloat(0, sizeID, .5f);
                //wallMaterial.SetFloat(sizeID, Mathf.Lerp(1, 0, 1));
                //this.transform.DOScale(0, 1f);
            }
        }
    }
    #endregion
}