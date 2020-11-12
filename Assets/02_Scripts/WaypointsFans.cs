using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsFans : MonoBehaviour
{
    public Transform[] waypoints;
    [SerializeField] float speed = 2f;

    int waypointIndex = 0;
    bool llego = true;

    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;;
    }

    private void Update()
    {
        if (llego)
        {
            Move();
        }
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position,waypoints[waypointIndex].transform.position, speed * Time.deltaTime);

        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex += 1;
        }

        if (waypointIndex == waypoints.Length)
        {
            llego = false;
        }
    }

}
