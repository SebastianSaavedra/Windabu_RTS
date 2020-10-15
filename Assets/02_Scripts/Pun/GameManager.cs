using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

namespace Com.MaluCompany.TestGame 
{
public class GameManager : MonoBehaviourPunCallbacks
{
        #region Photon call
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

                LoadArena();
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.LogFormat("Player", otherPlayer.NickName);
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("PlayerLeft MasterClient", PhotonNetwork.IsMasterClient);

                LoadArena();
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
