using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;
using TMPro;

namespace Com.MaluCompany.TestGame
{
    public class PlayerUIPhoton : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject cameraPlayer;
    [SerializeField] CinemachineVirtualCamera playerCamera;
     private TEST_Movement target;
    // Start is called before the first frame update
    void Start()
    {
            if (photonView.IsMine)
            {
                if (playerCamera == null)
                {
                    GameObject camera = Instantiate(cameraPlayer, cameraPlayer.transform.position, Quaternion.identity);
                    playerCamera = camera.gameObject.GetComponent<CinemachineVirtualCamera>();
                    //camera.tag = "CinemaCamera";
                    playerCamera.Follow = this.transform;
                }
            }
        }

        // Update is called once per frame
}
}
