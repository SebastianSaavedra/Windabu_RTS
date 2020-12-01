using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    [SerializeField] bool team, impre, impre3d, chapita, ploter, merch, impIndus;
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
            if (ploter)
            {
                transform.position = GameObject.Find("PlotA").transform.position;
            }
            if (merch)
            {
                transform.position = GameObject.Find("MerchA").transform.position;
            }
            if (impIndus)
            {
                transform.position = GameObject.Find("ImpIndusA").transform.position;
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
            if (ploter)
            {
                transform.position = GameObject.Find("PlotB").transform.position;
            }
            if (merch)
            {
                transform.position = GameObject.Find("MerchB").transform.position;
            }
            if (impIndus)
            {
                transform.position = GameObject.Find("ImpIndusB").transform.position;
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
            if (ploter)
            {
                transform.position = GameObject.Find("PlotA").transform.position;
            }
            if (merch)
            {
                transform.position = GameObject.Find("MerchA").transform.position;
            }
            if (impIndus)
            {
                transform.position = GameObject.Find("ImpIndusA").transform.position;
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
            if (ploter)
            {
                transform.position = GameObject.Find("PlotB").transform.position;
            }
            if (merch)
            {
                transform.position = GameObject.Find("MerchB").transform.position;
            }
            if (impIndus)
            {
                transform.position = GameObject.Find("ImpIndusB").transform.position;
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
            if (ploter)
            {
                transform.position = GameObject.Find("PlotA").transform.position;
            }
            if (merch)
            {
                transform.position = GameObject.Find("MerchA").transform.position;
            }
            if (impIndus)
            {
                transform.position = GameObject.Find("ImpIndusA").transform.position;
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
            if (ploter)
            {
                transform.position = GameObject.Find("PlotB").transform.position;
            }
            if (merch)
            {
                transform.position = GameObject.Find("MerchB").transform.position;
            }
            if (impIndus)
            {
                transform.position = GameObject.Find("ImpIndusB").transform.position;
            }
        }
    }
}
