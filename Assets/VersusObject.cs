using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersusObject : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Versus());
    }

    IEnumerator Versus()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        yield break;
    }
}
