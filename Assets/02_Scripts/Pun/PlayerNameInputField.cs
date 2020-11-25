using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
namespace Com.MaluCompany.TestGame { 
[RequireComponent(typeof(TMP_InputField))]
public class PlayerNameInputField : MonoBehaviour
{
        #region Constants
        const string playerNamePrefKey = "PlayerName";
        #endregion

        #region Mono Callbacks
        private void Start()
        {
            string defaultName = string.Empty;
            InputField _inputField = this.GetComponent<InputField>();
            if (_inputField != null) 
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey)) 
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }
            PhotonNetwork.NickName = defaultName;
        }
        #endregion

        #region Public Methods
        public void SetPlayerName(string value) 
        {
            if (string.IsNullOrEmpty(value)) 
            {
                Debug.LogError("Player name is empty");
                return;
            }
            PhotonNetwork.NickName = value;
            PlayerPrefs.SetString(playerNamePrefKey, value);
        }
        #endregion
    }
}
