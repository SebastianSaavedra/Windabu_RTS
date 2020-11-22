using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Collections;
using UnityEngine.UI;

public class Cartel : MonoBehaviour
{
    [SerializeField] GameObject cartel;
    [SerializeField] GameObject theParent;
    [HideInInspector] public GameObject cartelito;
    public bool hayCartel = false;

    [SerializeField] Sprite[] imgCartel;
    public List<GameObject> colliders = new List<GameObject>();
    [HideInInspector] public int iteracion;
    Coroutine cor;

    public void SpawnCartel()
    {
        if(!hayCartel)
        {
            Debug.Log("Dio click al Cartel");
            hayCartel = true;
            iteracion = 0;
            colliders.Clear();
            //Vector3 screenPos = Input.mousePosition;
            //Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
            cartelito = Instantiate(cartel, transform.position, Quaternion.identity);
            cartelito.transform.parent = theParent.transform;
            //cartelito.transform.localScale = new Vector3(1f,1f,1f);
            cartelito.transform.DOLocalMove(new Vector3(0f, 173f, 0f), 1f, true);
            foreach (Transform collider in cartelito.transform)
            {
                colliders.Add(collider.gameObject);
            }
            cor = StartCoroutine("MiniTimer");
            Debug.Log("La cantidad de colliders en la lista es: " + colliders.Count);
            //cartelito.transform.DOScale(new Vector2(1.25f, 1.25f),1f);
        }
    }

    IEnumerator MiniTimer()
    {
        yield return new WaitForSeconds(1f);
        colliders[0].SetActive(true);
        yield break;
    }

    public void Iterar()
    {
        if (iteracion < 2)
        {

            cartelito.GetComponent<Image>().sprite = imgCartel[iteracion];
            Debug.Log("El numero de imgCartel en la iteracion es: " + imgCartel[iteracion]);
            colliders[iteracion].SetActive(false);
            //Debug.Log("La cantidad de iteraciones1 fueron: " + iteracion);
            iteracion++;
            Debug.Log("La cantidad de iteraciones fueron: " + iteracion);
            colliders[iteracion].SetActive(true);
        }
    }
}
