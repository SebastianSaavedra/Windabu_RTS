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
    public class TEST_Movement : MonoBehaviourPunCallbacks
    {
    //Editables
    [SerializeField] float movSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] GameObject cameraPlayer;
    [SerializeField] CinemachineVirtualCamera playerCamera;
    [SerializeField] TextMeshProUGUI playerName;

        //Privadas
        Rigidbody2D rb2d;
    Vector2 objVelocity;

    void Start()
    {
            if (photonView.IsMine) 
            {
                playerName = GetComponentInChildren<TextMeshProUGUI>();
                playerName.text = photonView.Owner.NickName;
        rb2d = GetComponent<Rigidbody2D>();
            if (playerCamera== null) 
            {
                    GameObject camera = Instantiate(cameraPlayer, cameraPlayer.transform.position, Quaternion.identity);
                    playerCamera = camera.gameObject.GetComponent<CinemachineVirtualCamera>();
                    playerCamera.Follow = this.transform;                
            }
            }
    }


    void Update()
    {

       if (photonView.IsMine)
       {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        objVelocity = moveInput.normalized * movSpeed;
            
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movSpeed += runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movSpeed -= runSpeed;
        }
        }
    }

    private void FixedUpdate()
    {
            if (photonView.IsMine) 
            {
        rb2d.MovePosition(rb2d.position + objVelocity * Time.fixedDeltaTime);
            }
    }
}
}
