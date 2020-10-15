using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_Movement : MonoBehaviour
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movSpeed += runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movSpeed -= runSpeed;
        }
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + objVelocity * Time.fixedDeltaTime);
    }
}
