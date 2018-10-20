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
    public Vector2 axis;

    public Vector3 moveDirection;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        this.diagonalForwardSpeed = (float)Mathf.Sqrt(this.forwardSpeed * this.forwardSpeed / 2);
        this.backSpeed = this.forwardSpeed / 2;
        this.diagonalBackSpeed = (float)Mathf.Sqrt(this.backSpeed * this.backSpeed / 2);

        //moveDirection = Vector3.zero;
    }
     private void Update()
     {
         Rotate();
         Move();
        controller.Move(moveDirection);

    }
    public void SetAxis(Vector2 inputAxis)
    {
        axis = inputAxis;
    }
    public void Move()
    {
        Debug.Log("MUEVETE JODER");
        if (Grounded()){
            this.gravity = 0;
           
            if (MyGameManager.getInstance().inputAxis.x==0 && MyGameManager.getInstance().inputAxis.y== 0){
                this.moveDirection.Set(0, 0, 0);
            }
        else if(MyGameManager.getInstance().inputAxis.x > 0 && MyGameManager.getInstance().inputAxis.y == 0){
                this.moveDirection.Set(0, 0, MyGameManager.getInstance().inputAxis.x * this.forwardSpeed);
            }
        else if(MyGameManager.getInstance().inputAxis.x < 0 && MyGameManager.getInstance().inputAxis.y == 0){
                this.moveDirection.Set(0, 0, MyGameManager.getInstance().inputAxis.x * this.backSpeed);
            }
        else if(MyGameManager.getInstance().inputAxis.y > 0 && MyGameManager.getInstance().inputAxis.x == 0){
                this.moveDirection.Set(MyGameManager.getInstance().inputAxis.y * this.forwardSpeed, 0, 0);
            }
        else if (MyGameManager.getInstance().inputAxis.y < 0 && MyGameManager.getInstance().inputAxis.x == 0){
                this.moveDirection.Set(MyGameManager.getInstance().inputAxis.y * this.forwardSpeed, 0, 0);
            }
            else if (MyGameManager.getInstance().inputAxis.x > 0 && MyGameManager.getInstance().inputAxis.y > 0)
            {
                this.moveDirection.Set(MyGameManager.getInstance().inputAxis.y * this.diagonalForwardSpeed, 0, MyGameManager.getInstance().inputAxis.x * this.diagonalForwardSpeed);
            }
            else if (MyGameManager.getInstance().inputAxis.x > 0 && MyGameManager.getInstance().inputAxis.y < 0)
            {
                this.moveDirection.Set(MyGameManager.getInstance().inputAxis.y * this.diagonalForwardSpeed, 0, MyGameManager.getInstance().inputAxis.x * this.diagonalForwardSpeed);
            }
            else if (MyGameManager.getInstance().inputAxis.x < 0 && MyGameManager.getInstance().inputAxis.y > 0)
            {
                this.moveDirection.Set(MyGameManager.getInstance().inputAxis.y * this.diagonalBackSpeed, 0, MyGameManager.getInstance().inputAxis.x * this.diagonalBackSpeed);
            }
            else if (MyGameManager.getInstance().inputAxis.x < 0 && MyGameManager.getInstance().inputAxis.y < 0)
            {
                this.moveDirection.Set(MyGameManager.getInstance().inputAxis.y * this.diagonalBackSpeed, 0, MyGameManager.getInstance().inputAxis.x * this.diagonalBackSpeed);
            }
            this.moveDirection = transform.TransformDirection(this.moveDirection);

            if(MyGameManager.getInstance().jumpInput > 0){
                this.moveDirection.y = this.jumpSpeed;
            }
            else{
                this.gravity = 25f;

                if((this.controller.collisionFlags & CollisionFlags.Above) != 0){
                    this.moveDirection.y = 0;
                }
            }
            
        }
        this.moveDirection.y -= this.gravity * Time.deltaTime;
        
       
    }
    private void Rotate(){
        //transform.Rotate()
    }
    private bool Grounded(){
        return Physics.Raycast(transform.position + this.controller.center, Vector3.down, this.controller.bounds.extents.y + 0.001f);
    }
}

