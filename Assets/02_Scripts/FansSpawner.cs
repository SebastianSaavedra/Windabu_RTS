using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FansSpawner : MonoBehaviourPunCallbacks
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
    [Tooltip("0=Papel, 1=piedra, 2= tijera")]
    [SerializeField] int spawnAs;

    Coroutine spawnCor = null;

    public IEnumerator Spawner()
    {
        yield return new WaitUntil(() => cpManager.alreadyContested);
        while(puedeSpawnear)
        {
            Debug.Log("Aqui");
            if (cpManager.whatTeamInControl)
            {
                photonView.RPC("SpawnA", RpcTarget.AllViaServer);
            }
            else if (!GetComponent<CPManager>().whatTeamInControl) 
            {
                photonView.RPC("SpawnB", RpcTarget.AllViaServer);
            }
            Debug.Log("Aqui2");
            yield return new WaitForSeconds(timer);
        }
    }
    [PunRPC]
    public void SpawnA() 
    {
        GameObject fan = Instantiate(tipoDeFanA, spawnPoint.transform.position, Quaternion.identity);
        fan.GetComponent<WaypointsFans>().waypoints = waypointsA;
        switch (spawnAs) 
        {
            case 0:
                fan.GetComponent<FanLifeA>().papel=true;
                break;
            case 1:
                fan.GetComponent<FanLifeA>().piedra= true;
                break;
            case 2:
                fan.GetComponent<FanLifeA>().tijera = true;
                break;
        }
    }
    [PunRPC]
    public void SpawnB()
    {
        GameObject fan = Instantiate(tipoDeFanB, spawnPoint.transform.position, Quaternion.identity);
        fan.GetComponent<WaypointsFans>().waypoints = waypointsB;
        switch (spawnAs)
        {
            case 0:
                fan.GetComponent<FanLifeB>().papel = true;
                break;
            case 1:
                fan.GetComponent<FanLifeB>().piedra = true;
                break;
            case 2:
                fan.GetComponent<FanLifeB>().tijera = true;
                break;
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
