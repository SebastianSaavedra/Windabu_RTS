using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Minijuego", menuName = "Minijuego", order = 1)]
public class Minijuego : ScriptableObject
{
    public enum Estados { Disponible, SiendoJugado, Bloqueado}

    public Estados estadoDelMinijuego = Estados.Disponible;

    public int numeroDeJugadores = 0;
    public bool completado;

    //0 no hay jugador
    public int jugadorUno = 0;
    public int jugadorDos = 0;

    //Valores Barra Minijuegos
    public int barraVersusA;
    public int barraVersusB;

    public int rondaA;
    public int rondaB;
    public int cantidadDeFallosLightstickA;
    public int cantidadDeFallosLightstickB;

    public bool falloLighstickA;
    public bool falloLighstickB;

    public void ResetearValoresMinijuego()
    {
        numeroDeJugadores = 0;
        jugadorUno = 0;
        jugadorDos = 0;
        barraVersusA = 0;
        barraVersusB = 0;
        rondaA = 0;
        rondaB = 0;
        cantidadDeFallosLightstickA = 0;
        cantidadDeFallosLightstickB = 0;
        falloLighstickA = false;
        falloLighstickB = false;
        MinijuegoDisponible();
    }

    public void MinijuegoCompletado()
    {
        estadoDelMinijuego = Estados.Bloqueado;
        //comenzar timer;
        //resetea minijuego
    }

    public void ComenzarMinijuego()
    {
        estadoDelMinijuego = Estados.SiendoJugado;
    }

    public void MinijuegoDisponible()
    {
        estadoDelMinijuego = Estados.Disponible;
    }
}
