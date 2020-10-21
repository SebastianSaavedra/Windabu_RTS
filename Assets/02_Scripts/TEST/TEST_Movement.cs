using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Com.MaluCompany.TestGame
{
    public class TEST_Movement : MonoBehaviourPunCallbacks
    {
    //Editables
    [SerializeField] float movSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] GameObject playerUiPrefab;
        //Privadas
        Rigidbody2D rb2d;
    Vector2 objVelocity;

    void Start()
    {
            if (photonView.IsMine) 
            {
        rb2d = GetComponent<Rigidbody2D>();
            }
            if (playerUiPrefab != null)
            {
                GameObject _UI = Instantiate(playerUiPrefab);
                _UI.transform.parent = transform;
                _UI.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
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
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            /*GameObject _UI = Instantiate(this.playerUiPrefab);
            _UI.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);]*/
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
