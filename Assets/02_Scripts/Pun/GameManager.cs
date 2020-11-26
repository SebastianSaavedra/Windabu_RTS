using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

namespace Com.MaluCompany.TestGame 
{
public class GameManager : MonoBehaviourPunCallbacks
{
        public static List<Player> playersActuales = new List<Player>();
        #region Var
        [Tooltip("Prefab Player")]
        [SerializeField] GameObject playerPref;
        [SerializeField] GameObject joinedPlayer;
        [SerializeField] GameObject lobbyCanvas;
        [SerializeField] GameObject buttonStart;
        #endregion

        private void Start()
        {
                lobbyCanvas.SetActive(true);
            if (PhotonNetwork.IsMasterClient) 
            {
                buttonStart.SetActive(true);
            }       
        }
        #region Photon call

        Player TargetPlayerByActorNumber(int playerActorNumber)
        {
            //Solamente se tiene para mostrar el número de jugadores y su actorNumber
            foreach (var item in playersActuales)
            {
                Debug.Log("Players: " + item.ActorNumber);
            }

            //Determina a qué jugador debemos enviarle el RPC según su actor number 
            //(identificador de cada jugador en esta sala de juego)
            Player playerToReturn = null;
            for (int i = 0; i < playersActuales.Count; i++)
            {
                if (playersActuales[i].ActorNumber == playerActorNumber)
                {
                    playerToReturn = playersActuales[i];
                    break;
                }
            }
            return playerToReturn;
        }
        public override void OnLeftRoom() 
        {
            SceneManager.LoadScene(0);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.LogFormat("Player", newPlayer.NickName);

            if (PhotonNetwork.IsMasterClient) 
            {
                Debug.LogFormat("PlayerEnter MasterClient", PhotonNetwork.IsMasterClient);

                //LoadArena();
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.LogFormat("Player", otherPlayer.NickName);
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("PlayerLeft MasterClient", PhotonNetwork.IsMasterClient);

               // LoadArena();
            }
        }
        #endregion

        #region Private Methods
        void LoadArena() 
        {
            if (!PhotonNetwork.IsMasterClient) 
            {
                Debug.LogError("No master Client");
            }
            Debug.LogFormat("Loading Level" + PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("VarelaTest");
        }
        #endregion
        #region Public Methods
        public void LeaveRoom() 
        {
            PhotonNetwork.LeaveRoom();
        }
        #endregion
    }
}
