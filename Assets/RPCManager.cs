using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class RPCManager : MonoBehaviourPunCallbacks
{
    [SerializeField] ManagerMinijuegos managerMinijuegos;
    public void RPCActualizarDatosA(int take,int Out)       // Datos de las barras
    {
        Debug.Log("Inicio el RPCActualizarDatosA");
        photonView.RPC("ActualizarDatosA",RpcTarget.MasterClient,take,Out);
        Debug.Log("Debug del take: " + take + "Debug del out: " + Out);
    }

    public void RPCActualizarDatosB(int take, int Out)
    {
        Debug.Log("Inicio el RPCActualizarDatosB");
        photonView.RPC("ActualizarDatosB", RpcTarget.MasterClient, take, Out);
    }
    public void RPCActualizarDatosRondaA(int take, int Out)     // Datos de las rondas
    {
        Debug.Log("Inicio el RPCActualizarDatosRondaA");
        photonView.RPC("ActualizarDatosRondaA", RpcTarget.MasterClient, take, Out);
        Debug.Log("Debug del take: " + take + "Debug del out: " + Out);
    }

    public void RPCActualizarDatosRondaB(int take, int Out)
    {
        Debug.Log("Inicio el RPCActualizarDatosRondaB");
        photonView.RPC("ActualizarDatosRondaB", RpcTarget.MasterClient, take, Out);
    }

    public void RPCActualizarDatosFallosLightstickA(int take, int Out)      // Datos de la cantidad de veces que fallo el lightstick
    {
        Debug.Log("Player A fallo y se actualizo el RPC de fallos");
        photonView.RPC("ActualizarCantidadDeFallosLightstickA", RpcTarget.MasterClient, take, Out);
    }
    public void RPCActualizarDatosFallosLightstickB(int take, int Out)
    {
        Debug.Log("Player B fallo y se actualizo el RPC de fallos");
        photonView.RPC("ActualizarCantidadDeFallosLightstickB", RpcTarget.MasterClient, take, Out);
    }

    public void RPCActualizarFalloLighstickA(int take, bool lightstick)     // Booleanos de si esque fallo el lightstick
    {
        Debug.Log("Player A fallo Lightstick");
        photonView.RPC("ActualizarFalloLightstickA", RpcTarget.MasterClient, take, lightstick);
    }

    public void RPCActualizarFalloLighstickB(int take, bool lightstick)
    {
        Debug.Log("Player B fallo lighstick");
        photonView.RPC("ActualizarFalloLightstickB", RpcTarget.MasterClient, take, lightstick);
    }
    public void RPCActualizarIntentosA(int take, int Out)
    {
        Debug.Log("Player A hizo un intento de gameplay");
        photonView.RPC("ActualizarCantidadDeIntentosA", RpcTarget.MasterClient, take, Out);
    }
    public void RPCActualizarIntentosB(int take, int Out)
    {
        Debug.Log("Player B hizo un intento de gameplay");
        photonView.RPC("ActualizarCantidadDeIntentosB", RpcTarget.MasterClient, take, Out);
    }

    [PunRPC]
    void ActualizarDatosA(int take,int Out)
    {
        managerMinijuegos.minijuegos[take].barraVersusA = Out;
    }

    [PunRPC]
    void ActualizarDatosB(int take, int Out)
    {
        managerMinijuegos.minijuegos[take].barraVersusB = Out;
    }

    [PunRPC]
    void ActualizarDatosRondaA(int take, int Out)
    {
        managerMinijuegos.minijuegos[take].rondaA += Out;
    }

    [PunRPC]
    void ActualizarDatosRondaB(int take, int Out)
    {
        managerMinijuegos.minijuegos[take].rondaB += Out;
    }

    [PunRPC]
    void ActualizarCantidadDeFallosLightstickA(int take, int Out)
    {
        managerMinijuegos.minijuegos[take].cantidadDeFallosLightstickA = Out;
    }

    [PunRPC]
    void ActualizarCantidadDeFallosLightstickB(int take, int Out)
    {
        managerMinijuegos.minijuegos[take].cantidadDeFallosLightstickB = Out;
    }

    [PunRPC]
    void ActualizarCantidadDeIntentosA(int take, int Out)
    {
        managerMinijuegos.minijuegos[take].intentosA = Out;
    }

    [PunRPC]
    void ActualizarCantidadDeIntentosB(int take, int Out)
    {
        managerMinijuegos.minijuegos[take].intentosB = Out;
    }

    [PunRPC]
    void ActualizarFalloLightstickA(int take, bool lightstick)
    {
        managerMinijuegos.minijuegos[take].falloLighstickA = lightstick;
    }

    [PunRPC]
    void ActualizarFalloLightstickB(int take, bool lightstick)
    {
        managerMinijuegos.minijuegos[take].falloLighstickB = lightstick;
    }
}
