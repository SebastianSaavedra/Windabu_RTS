using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaControl : MonoBehaviour
{
    public GameObject palancaUp, palancaDown, palancaMisc;

    public void Palanca()
    {
        StartCoroutine(Anim());
    }

    public void PalancaWithStages()
    {
        StartCoroutine(WithStagesAnim());
    }

    IEnumerator Anim()
    {
        palancaUp.SetActive(false);
        palancaDown.SetActive(true);
        yield return new WaitForSeconds(.5f);
        palancaUp.SetActive(true);
        palancaDown.SetActive(false);
    }

    IEnumerator WithStagesAnim()
    {
        palancaUp.SetActive(false);
        palancaDown.SetActive(true);
        palancaMisc.SetActive(false);
        yield return new WaitForSeconds(1f);

        palancaUp.SetActive(false);
        palancaDown.SetActive(false);
        palancaMisc.SetActive(true);
        yield return new WaitForSeconds(.4f);

        palancaUp.SetActive(true);
        palancaDown.SetActive(false);
        palancaMisc.SetActive(false);
    }
}
