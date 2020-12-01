using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BehindRenderShader : MonoBehaviour
{
    //[SerializeField] GameObject camara;
    //[SerializeField] GameObject target;
    //[SerializeField] LayerMask layerMask;

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Edificio"))
        {
            Debug.Log("Hiteo a: " + collision.name);
            gameObject.transform.DOScale(1f, .5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Salio del collider");
        gameObject.transform.localScale = new Vector3(0f, 0f, 1f); //.DOScale(0, .5f);
    }
}
