using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    [SerializeField] bool team;
    private void Awake()
    {
        if (team) 
        {
            transform.position = GameObject.Find("ImpreA").transform.position;
        }
        else 
        {
            transform.position = GameObject.Find("ImpreA").transform.position;
        }
    }

    private void Start()
    {
        if (team)
        {
            transform.position = GameObject.Find("ImpreA").transform.position;
        }
        else
        {
            transform.position = GameObject.Find("ImpreA").transform.position;
        }
    }

    private void OnEnable()
    {
        if (team)
        {
            transform.position = GameObject.Find("ImpreA").transform.position;
        }
        else
        {
            transform.position = GameObject.Find("ImpreA").transform.position;
        }
    }
}
