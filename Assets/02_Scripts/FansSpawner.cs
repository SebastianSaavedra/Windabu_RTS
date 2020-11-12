using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FansSpawner : MonoBehaviour
{
    public GameObject tipoDeFanA;
    public GameObject tipoDeFanB;
    public Transform spawnPoint;
    public float timer = 1;
    [Space(5)]
    public bool puedeSpawnear;
    [SerializeField] Transform[] waypointsA;
    [SerializeField] Transform[] waypointsB;
    [SerializeField] CPManager cpManager;

    Coroutine spawnCor = null;

    public IEnumerator Spawner()
    {
        yield return new WaitUntil(() => cpManager.alreadyContested);
        while(puedeSpawnear)
        {
            Debug.Log("Aqui");
            if (cpManager.whatTeamInControl)
            {
         GameObject fan=  Instantiate(tipoDeFanA, spawnPoint.transform.position, Quaternion.identity);
            fan.GetComponent<WaypointsFans>().waypoints = waypointsA;
            }
            else if (!GetComponent<CPManager>().whatTeamInControl) 
            {
         GameObject fan=  Instantiate(tipoDeFanB, spawnPoint.transform.position, Quaternion.identity);
            fan.GetComponent<WaypointsFans>().waypoints = waypointsB;
            }
            Debug.Log("Aqui2");
            yield return new WaitForSeconds(timer);
        }
    }

    public IEnumerator SpawnerConParametros(float time, Transform spawnP)
    {
        while (puedeSpawnear)
        {
            //Instantiate(tipoDeFan, spawnP.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(time);
        }
    }

    private void Start()
    {
        spawnCor = StartCoroutine(Spawner());
    }
}
