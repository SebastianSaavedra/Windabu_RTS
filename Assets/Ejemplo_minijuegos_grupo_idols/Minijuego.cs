﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Minijuego", menuName = "Minijuego", order = 1)]
public class Minijuego : ScriptableObject
{
    public enum Estados { Disponible, SiendoJugado, Bloqueado}

    public Estados estadoDelJuego = Estados.Disponible;

    public int numeroDeJugadores = 0;

    //0 no hay jugador
    public int jugadorUno = 0;
    public int jugadorDos = 0;

    public void ResetearValoresMinijuego()
    {
        numeroDeJugadores = 0;
        jugadorUno = 0;
        jugadorDos = 0;
        MinijuegoDisponible();
    }

    public void MinijuegoCompletado()
    {
        estadoDelJuego = Estados.Bloqueado;
        //comenzar timer;
        //resetea minijuego
    }

    public void ComenzarMinijuego()
    {
        estadoDelJuego = Estados.SiendoJugado;
    }

    public void MinijuegoDisponible()
    {
        estadoDelJuego = Estados.Disponible;
    }
}
