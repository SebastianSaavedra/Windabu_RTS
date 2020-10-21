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

        #region Var
        [Tooltip("Prefab Player")]
        [SerializeField] GameObject playerPref;
        [SerializeField] GameObject joinedPlayer;
        public List<GameObject> players;
        #endregion

        private void Start()
        {
            if (playerPref == null) 
            {
                Debug.LogError("Bruh");
            }
            else 
            {
                Debug.LogFormat("Instantiating Player");
               PhotonNetwork.Instantiate(this.playerPref.name, new Vector3(0, 0, 0), Quaternion.identity, 0);
            }
        }
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
