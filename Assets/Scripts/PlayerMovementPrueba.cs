﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovementPrueba : MonoBehaviour {

	private CharacterController controller;
    public Animator pers;
    private Camera cam;
    public float forwardSpeed;
    private float diagonalForwardSpeed;
    private float backSpeed;
    private float diagonalBackSpeed;
    public float jumpSpeed;
    public float gravity;

    private Transform movement; 
    public Transform obj;
    public float Speed;
    public Vector2 axis;
     private Vector3 desiredDirection;
    public Vector3 moveDirection;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        
        this.diagonalForwardSpeed = (float)Mathf.Sqrt(this.forwardSpeed * this.forwardSpeed / 2);
        this.backSpeed = this.forwardSpeed;
        this.diagonalBackSpeed = (float)Mathf.Sqrt(this.backSpeed * this.backSpeed / 2);
        moveDirection = Vector3.zero;
        movement=GetComponent<Transform> ();
        cam=Camera.main;
    }
     private void Update()
     { 
         
         
         //SetRotate(this.gameObject,cam.gameObject);
         Rotate();
      
         Move();
        

    }
    public void SetAxis(Vector2 inputAxis)
    {
        axis = inputAxis;
    }
    public void Move()
    {
        
        //if (Grounded()){
            gravity = 0;

           desiredDirection = (axis.x * transform.right * forwardSpeed) + (axis.y * transform.forward * forwardSpeed);

            if (desiredDirection != Vector3.zero){
                pers.SetBool("Walk",true);
            }else{
                 pers.SetBool("Walk",false);
            }
           /* if (MyGameManager.getInstance().inputAxis.x==0 && MyGameManager.getInstance().inputAxis.y== 0){
                moveDirection.Set(0, 0, 0);
                pers.SetBool("Walk",false);
            }
        else if(MyGameManager.getInstance().inputAxis.x > 0 && MyGameManager.getInstance().inputAxis.y == 0){
                moveDirection.Set(0, 0, MyGameManager.getInstance().inputAxis.x * this.forwardSpeed);
                pers.SetBool("Walk",true);
            }
        else if(MyGameManager.getInstance().inputAxis.x < 0 && MyGameManager.getInstance().inputAxis.y == 0){
                moveDirection.Set(0, 0, MyGameManager.getInstance().inputAxis.x * this.backSpeed);
                pers.SetBool("Walk",true);
            }
        else if(MyGameManager.getInstance().inputAxis.y > 0 && MyGameManager.getInstance().inputAxis.x == 0){
                moveDirection.Set(MyGameManager.getInstance().inputAxis.y * this.forwardSpeed, 0, 0);
                pers.SetBool("Walk",true);
            }
        else if (MyGameManager.getInstance().inputAxis.y < 0 && MyGameManager.getInstance().inputAxis.x == 0){
                moveDirection.Set(MyGameManager.getInstance().inputAxis.y * this.forwardSpeed, 0, 0);
                pers.SetBool("Walk",true);
            }
            else if (MyGameManager.getInstance().inputAxis.x > 0 && MyGameManager.getInstance().inputAxis.y > 0)
            {
                moveDirection.Set(MyGameManager.getInstance().inputAxis.y * this.diagonalForwardSpeed, 0, MyGameManager.getInstance().inputAxis.x * this.diagonalForwardSpeed);
                pers.SetBool("Walk",true);
            }
            else if (MyGameManager.getInstance().inputAxis.x > 0 && MyGameManager.getInstance().inputAxis.y < 0)
            {
                moveDirection.Set(MyGameManager.getInstance().inputAxis.y * this.diagonalForwardSpeed, 0, MyGameManager.getInstance().inputAxis.x * this.diagonalForwardSpeed);
                pers.SetBool("Walk",true);
            }
            else if (MyGameManager.getInstance().inputAxis.x < 0 && MyGameManager.getInstance().inputAxis.y > 0)
            {
                moveDirection.Set(MyGameManager.getInstance().inputAxis.y * this.diagonalBackSpeed, 0, MyGameManager.getInstance().inputAxis.x * this.diagonalBackSpeed);
                pers.SetBool("Walk",true);
            }
            else if (MyGameManager.getInstance().inputAxis.x < 0 && MyGameManager.getInstance().inputAxis.y < 0)
            {
                moveDirection.Set(MyGameManager.getInstance().inputAxis.y * this.diagonalBackSpeed, 0, MyGameManager.getInstance().inputAxis.x * this.diagonalBackSpeed);
                pers.SetBool("Walk",true);
            }
           
            moveDirection = transform.TransformDirection(moveDirection);
                */

            moveDirection = desiredDirection;
            if(MyGameManager.getInstance().jumpInput > 0){
                moveDirection.y = jumpSpeed;
            }
            else{
                gravity = 25f;

                if((controller.collisionFlags & CollisionFlags.Above) != 0){
                    moveDirection.y = 0;
                }
            }
        //moveDirection=transform.forward;
        controller.Move(moveDirection);
        moveDirection.y -= gravity * Time.deltaTime;
       // }
       
    }
    private void Rotate(){
        
        if(moveDirection != Vector3.zero && axis.x!=-1 || axis.x!=1){
          //movement.rotation = Quaternion.LookRotation(desiredDirection,Vector3.up);
          //movement.rotation = Quaternion.Lerp( movement.rotation,Quaternion.LookRotation(desiredDirection),0.05f);
         //movement.rotation=obj.rotation;
         //movement.forward=obj.forward;
         }else if (axis.x==-1 || axis.x==1){
            //movement.rotation= Quaternion.RotateTowards(Vector3.forward, desiredDirection,maxDegreesDelta:0.05f)  ;
         }
       // transform.rotation = Quaternion.LookRotation(desiredDirection);
        //transform.Rotate(0,desiredDirection.y,0);
       //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection,Vector3.up), 0.05f);
        
        
        //transform.Rotate(0,Input.GetAxis("Horizontal"),0);
       
    }
    private bool Grounded(){
        return Physics.Raycast(transform.position + this.controller.center, Vector3.down, this.controller.bounds.extents.y + 0.001f);
    }
    void SetRotate(GameObject toRotate, GameObject camera)
    {
        //You can call this function for any game object and any camera, just change the parameters when you call this function
        transform.rotation = Quaternion.Lerp(toRotate.transform.rotation, camera.transform.rotation, Speed * Time.deltaTime);
    }
}

