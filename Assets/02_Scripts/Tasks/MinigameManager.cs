using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
public static class Minijuegos
{
    public static Action<int> m_cartel;
    public static Action<int> m_clicks;
}

public class MinigameManager : MonoBehaviourPunCallbacks
{
    #region Variables
    int carteles;
    public static int dinero;
    #endregion

    void Start()
    {
        Minijuegos.m_cartel += Carteles;
        Minijuegos.m_clicks += Clicks;
    }

    void Carteles(int valor)
    {
        carteles += valor;
        Debug.Log("Numero de carteles: " + carteles);

        if (carteles >= 10)
        {
            // Gano algun equipo (?
        }
    }

    void Clicks(int valor)
    {
        dinero += valor;
    }
}
