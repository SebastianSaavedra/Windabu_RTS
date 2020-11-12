using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FansSpawner : MonoBehaviour
{
    public GameObject tipoDeFan;
    public Transform spawnPoint;
    public float timer = 1;
    [Space(5)]
    public bool puedeSpawnear;

    Coroutine spawnCor = null;

    public IEnumerator Spawner()
    {
        while(puedeSpawnear)
        {
            Instantiate(tipoDeFan, spawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timer);
        }
    }

    public IEnumerator SpawnerConParametros(float time, Transform spawnP)
    {
        while (puedeSpawnear)
        {
            Instantiate(tipoDeFan, spawnP.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(time);
        }
    }

    private void Start()
    {
        spawnCor = StartCoroutine(Spawner());
    }
}
