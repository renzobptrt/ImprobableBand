using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Complements")]
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundLayer;


    [Header("Settings Player")]
    public float speed = 8;
    public float jumpForce = 10;
    public float gravity = -20;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Velocity X
        float hInput = Input.GetAxis("Horizontal");
        direction.x = hInput * speed;

        //Check is Grounded
        bool isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);

        if (isGrounded)
        {
            direction.y = 0f;

            ableToMakeADoubleJump = true;

            //Jump
            if (Input.GetButtonDown("Jump"))
            {
                direction.y = jumpForce;
            }
        }
        else
        {
            //Gravity
            direction.y += gravity * Time.deltaTime;

            if (ableToMakeADoubleJump & Input.GetButtonDown("Jump"))
            {
                direction.y = jumpForce;
                ableToMakeADoubleJump = false;
            }
        }

        //Move
        controller.Move(direction * Time.deltaTime);
    }

    private Vector3 direction;
    private bool ableToMakeADoubleJump = true;
}
