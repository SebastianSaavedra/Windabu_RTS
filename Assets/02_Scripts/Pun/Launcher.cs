using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

namespace Com.MaluCompany.TestGame 
{ 
public class Launcher : MonoBehaviourPunCallbacks
  {

        #region Private S Fields
        [Tooltip("Maximum player per room")]
        [SerializeField]
        private byte maxPlayerPerRoom = 6;
        [Tooltip("Ui Panel")]
        [SerializeField] private GameObject controlPanel;   
        [Tooltip("UI Label")]
        [SerializeField] private GameObject progressLabel;
        #endregion

        #region Private Field
        string gameVersion = "1";
        bool isConnecting;
        #endregion

        #region Mono CallBack

        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }
        #endregion

        #region Public Methods
        public void Connect() 
        {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            if (PhotonNetwork.IsConnected) 
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else 
            {
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }
        #endregion

        #region MohoBehavious Pun CallBacks
        public override void OnConnectedToMaster()
        {
            Debug.Log("Pun called");
            if (isConnecting)
            {
            PhotonNetwork.JoinRandomRoom();
                isConnecting = false;
            }
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            Debug.LogWarningFormat("Pun disconnected");
        }
       

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Pun Failed to join room");
            PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers= maxPlayerPerRoom});
        }
        public override void OnJoinedRoom()
        {
            //switch (PhotonNetwork.CurrentRoom.PlayerCount) 
            //{
            //    case 1:
            //        Debug.Log("Load for 1 person");
            //       PhotonNetwork.LoadLevel("VarelaTest");
            //        break;
            //    case 2:
            //        Debug.Log("Load for 2 person");
            //        PhotonNetwork.LoadLevel("VarelaTest");
            //        break;
            //    case 3:
            //        Debug.Log("Load for 3 person");
            //        PhotonNetwork.LoadLevel("VarelaTest");
            //        break;
            //    case 4:
            //        Debug.Log("Load for 4 person");
            //        PhotonNetwork.LoadLevel("VarelaTest");
            //        break;              
            //}



            PhotonNetwork.LoadLevel("VarelaTestarudo");

        }
        #endregion
    }
}

