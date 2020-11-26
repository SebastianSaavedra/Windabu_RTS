using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsFans : MonoBehaviour
{
    public Transform[] waypoints;
    [SerializeField] float speed = 2f;

    int waypointIndex = 0;
    bool llego = true;

    [SerializeField] Animator fansAnim;

    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
        fansAnim.SetBool("Caminando",true);
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
        if (transform.position== waypoints[waypoints.Length-1].transform.position) 
        {
        fansAnim.SetBool("Caminando",false);
        }

        if (waypointIndex == waypoints.Length)
        {
            llego = false;
        }
    }

}
