using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovementPrueba : MonoBehaviour {

	private CharacterController controller;

    public float forwardSpeed;
    private float diagonalForwardSpeed;
    private float backSpeed;
    private float diagonalBackSpeed;
    public float jumpSpeed;
    public float gravity;

    private Vector3 moveDirection;

    private void Start()
    {
        this.controller = GetComponent<CharacterController>();

        this.diagonalForwardSpeed = (float)Mathf.Sqrt(this.forwardSpeed * this.forwardSpeed / 2);
        this.backSpeed = this.forwardSpeed / 2;
        this.diagonalBackSpeed = (float)Mathf.Sqrt(this.backSpeed * this.backSpeed / 2);

        this.moveDirection = Vector3.zero;
    }
    private void Update()
    {
        MyGameManager.getInstance().GetInput();
        //Rotate();
        Move();
    }
    private void Move()
    {
        if (this.controller.isGrounded){
            this.gravity = 0;

        if (MyGameManager.getInstance().inputV==0 && MyGameManager.getInstance().inputH== 0){
                this.moveDirection.Set(0, 0, 0);
            }
        }
    }
}

