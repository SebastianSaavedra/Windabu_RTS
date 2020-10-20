using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Com.MaluCompany.TestGame
{
    public class TEST_Movement : MonoBehaviourPunCallbacks
    {
    //Editables
    [SerializeField] float movSpeed;
    [SerializeField] float runSpeed;

    //Privadas
    Rigidbody2D rb2d;
    Vector2 objVelocity;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        objVelocity = moveInput.normalized * movSpeed;

       if (photonView.IsMine)
       {
            
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
        rb2d.MovePosition(rb2d.position + objVelocity * Time.fixedDeltaTime);
    }
}
}
