using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    GameObject itemSpawner;
    Transform itemSpawnerPos;

    //Stats
    [SerializeField]
    float impresoraCost = 100f, impresoraDelay = 15f;

    //Prefabs
    public GameObject impresora;

    private void Start()
    {
        itemSpawnerPos = itemSpawner.transform;
    }

    public void Impresora()
    {
        if(gameObject.GetComponent<TeamManager>().money >= impresoraCost)
        {
            StartCoroutine(SpawnItem(impresoraDelay, impresora));
            gameObject.GetComponent<TeamManager>().money -= impresoraCost;
        }
        else
        {
            Debug.Log("Dinero Insuficiente Pe");
        }
    }

    IEnumerator SpawnItem(float waitTime, GameObject itemToSpawn)
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(itemToSpawn, itemSpawnerPos);
    }
}
