using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovementPrueba : MonoBehaviour {

	private CharacterController controller;
    
    public GameObject personaje;
    public Animator pers;
    public float forwardSpeed;
    private float diagonalForwardSpeed;
    private float backSpeed;
    private float diagonalBackSpeed;
    public float jumpSpeed;
    public float gravity;
    public Vector2 axis;
     private Vector3 persview;
		 
    public Vector3 moveDirection;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        this.diagonalForwardSpeed = (float)Mathf.Sqrt(this.forwardSpeed * this.forwardSpeed / 2);
        this.backSpeed = this.forwardSpeed;
        this.diagonalBackSpeed = (float)Mathf.Sqrt(this.backSpeed * this.backSpeed / 2);
        moveDirection = Vector3.zero;
    }
     private void Update()
     { 
         
         if(moveDirection!= Vector3.zero){
         Rotate();
         }
         Move();
        

    }
    public void SetAxis(Vector2 inputAxis)
    {
        axis = inputAxis;
    }
    public void Move()
    {
        
        //if (Grounded()){
            this.gravity = 0;
            
            if (MyGameManager.getInstance().inputAxis.x==0 && MyGameManager.getInstance().inputAxis.y== 0){
                this.moveDirection.Set(0, 0, 0);
                pers.SetBool("Walk",false);
            }
        else if(MyGameManager.getInstance().inputAxis.x > 0 && MyGameManager.getInstance().inputAxis.y == 0){
                this.moveDirection.Set(0, 0, MyGameManager.getInstance().inputAxis.x * this.forwardSpeed);
                pers.SetBool("Walk",true);
            }
        else if(MyGameManager.getInstance().inputAxis.x < 0 && MyGameManager.getInstance().inputAxis.y == 0){
                this.moveDirection.Set(0, 0, MyGameManager.getInstance().inputAxis.x * this.backSpeed);
                pers.SetBool("Walk",true);
            }
        else if(MyGameManager.getInstance().inputAxis.y > 0 && MyGameManager.getInstance().inputAxis.x == 0){
                this.moveDirection.Set(MyGameManager.getInstance().inputAxis.y * this.forwardSpeed, 0, 0);
                pers.SetBool("Walk",true);
            }
        else if (MyGameManager.getInstance().inputAxis.y < 0 && MyGameManager.getInstance().inputAxis.x == 0){
                this.moveDirection.Set(MyGameManager.getInstance().inputAxis.y * this.forwardSpeed, 0, 0);
                pers.SetBool("Walk",true);
            }
            else if (MyGameManager.getInstance().inputAxis.x > 0 && MyGameManager.getInstance().inputAxis.y > 0)
            {
                this.moveDirection.Set(MyGameManager.getInstance().inputAxis.y * this.diagonalForwardSpeed, 0, MyGameManager.getInstance().inputAxis.x * this.diagonalForwardSpeed);
                pers.SetBool("Walk",true);
            }
            else if (MyGameManager.getInstance().inputAxis.x > 0 && MyGameManager.getInstance().inputAxis.y < 0)
            {
                this.moveDirection.Set(MyGameManager.getInstance().inputAxis.y * this.diagonalForwardSpeed, 0, MyGameManager.getInstance().inputAxis.x * this.diagonalForwardSpeed);
                pers.SetBool("Walk",true);
            }
            else if (MyGameManager.getInstance().inputAxis.x < 0 && MyGameManager.getInstance().inputAxis.y > 0)
            {
                this.moveDirection.Set(MyGameManager.getInstance().inputAxis.y * this.diagonalBackSpeed, 0, MyGameManager.getInstance().inputAxis.x * this.diagonalBackSpeed);
                pers.SetBool("Walk",true);
            }
            else if (MyGameManager.getInstance().inputAxis.x < 0 && MyGameManager.getInstance().inputAxis.y < 0)
            {
                this.moveDirection.Set(MyGameManager.getInstance().inputAxis.y * this.diagonalBackSpeed, 0, MyGameManager.getInstance().inputAxis.x * this.diagonalBackSpeed);
                pers.SetBool("Walk",true);
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
        //moveDirection=transform.forward;
        controller.Move(moveDirection);
        //this.moveDirection.y -= this.gravity * Time.deltaTime;
       // }
       
    }
    private void Rotate(){
      personaje.transform.rotation = Quaternion.Slerp(personaje.transform.rotation, Quaternion.LookRotation(moveDirection), 2f);
         //transform.rotation=Quaternion.Euler(0,0,0);
         //personaje.transform.Rotate(0,Input.GetAxis("Horizontal"),0);
       
    }
    private bool Grounded(){
        return Physics.Raycast(transform.position + this.controller.center, Vector3.down, this.controller.bounds.extents.y + 0.001f);
    }
}

