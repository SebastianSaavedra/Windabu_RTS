using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    [SerializeField] bool team;
    [SerializeField] bool impre;
    [SerializeField] bool impre3d;
    [SerializeField] bool chapita;
    private void Awake()
    {
        if (team) 
        {
            if (impre)
            {
                transform.position = GameObject.Find("ImpreA").transform.position;
            }
            if (impre3d)
            {
                transform.position = GameObject.Find("Impre3DA").transform.position;
            }
            if (chapita) 
            {
                transform.position = GameObject.Find("ChaA").transform.position;
            }
        }
        else 
        {
            if (impre)
            {
                transform.position = GameObject.Find("ImpreB").transform.position;
            }
            if (impre3d)
            {
                transform.position = GameObject.Find("Impre3DB").transform.position;
            }
            if (chapita)
            {
                transform.position = GameObject.Find("ChaB").transform.position;
            }
        }
    }

    private void Start()
    {
        if (team)
        {
            if (impre)
            {
                transform.position = GameObject.Find("ImpreA").transform.position;
            }
            if (impre3d)
            {
                transform.position = GameObject.Find("Impre3DA").transform.position;
            }
            if (chapita)
            {
                transform.position = GameObject.Find("ChaA").transform.position;
            }
        }
        else
        {
            if (impre)
            {
                transform.position = GameObject.Find("ImpreB").transform.position;
            }
            if (impre3d)
            {
                transform.position = GameObject.Find("Impre3DB").transform.position;
            }
            if (chapita)
            {
                transform.position = GameObject.Find("ChaB").transform.position;
            }
        }
    }

    private void OnEnable()
    {
        if (team)
        {
            if (impre)
            {
                transform.position = GameObject.Find("ImpreA").transform.position;
            }
            if (impre3d)
            {
                transform.position = GameObject.Find("Impre3DA").transform.position;
            }
            if (chapita)
            {
                transform.position = GameObject.Find("ChaA").transform.position;
            }
        }
        else
        {
            if (impre)
            {
                transform.position = GameObject.Find("ImpreB").transform.position;
            }
            if (impre3d)
            {
                transform.position = GameObject.Find("Impre3DB").transform.position;
            }
            if (chapita)
            {
                transform.position = GameObject.Find("ChaB").transform.position;
            }
        }
    }
}
