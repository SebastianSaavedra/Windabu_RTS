using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RaycastBehind : MonoBehaviour
{
    [SerializeField] GameObject camara;
    [SerializeField] GameObject target;
    [SerializeField] LayerMask layerMask;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(camara.transform.position, (target.transform.position - camara.transform.position).normalized, Mathf.Infinity, layerMask);

        if (hit.collider.gameObject.tag == "EsferaMask")
        {
            Debug.Log("Hiteo al circulo");
            target.transform.DOScale(0, .5f);
        }

        else
        {
            Debug.Log("Esta hiteando: " + hit.collider.name);
            target.transform.DOScale(.5f, .5f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(camara.transform.position,(target.transform.position - camara.transform.position).normalized);
    }
}
