using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BaseFanSpawner : MonoBehaviourPunCallbacks
{
    public GameObject tipoDeFanA;
    public GameObject tipoDeFanB;
    public Transform spawnPoint;
    public float timer = 20;
    [Space(5)]
    public bool puedeSpawnear;
    [SerializeField] Transform[] waypointsA;
    [SerializeField] Transform[] waypointsB;
    [Tooltip("0=Papel, 1=piedra, 2= tijera")]
    [SerializeField] int spawnAs;
    [SerializeField] bool team;
    Coroutine spawnCor = null;

    public IEnumerator Spawner()
    {
        while (puedeSpawnear)
        {
            if (team)
            {
                photonView.RPC("SpawnA", RpcTarget.AllViaServer);
            }
            else if (!team)
            {
                photonView.RPC("SpawnB", RpcTarget.AllViaServer);
            }
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
                fan.GetComponent<FanLifeA>().papel = true;
                break;
            case 1:
                fan.GetComponent<FanLifeA>().piedra = true;
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

    private void OnEnable()
    {
        spawnCor = StartCoroutine(Spawner());       
    }
}
