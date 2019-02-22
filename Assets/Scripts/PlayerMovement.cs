﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float inputX;
    public float inputZ;
    public Vector3 desiredMoveDirection;
    public float desiredRotationSpeed;
    public bool blockRotationPlayer;
    public Animator anim;
    public float speed;
    public float allowPlayerRotation;
    public Camera cam;
    public CharacterController controller;
    public bool isGrounded;
    private float verticalVel;
    private Vector3 moveVector;
    private Vector3 moveVector2;
    public ParticleSystem particle;
    public SoundPlayer sound;
    private float counter;
    public bool jump=false;
    public bool suelo = false;
    private void Start()
    {
        //anim = GetComponent<Animator>();
        cam = Camera.main;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (MyGameManager.getInstance().pause == false)
            return;
    
        InputMagnitude();

        isGrounded = controller.isGrounded;
         if (Input.GetButton("Jump")&& isGrounded && suelo==true){
            verticalVel=0.5f;
            //jump = true;
        }
        if (isGrounded){
            //verticalVel -= 0;
            suelo = true;
            
        }
        else
        {
            jump = true;
            suelo = false;
            Invoke("AddGravity",0.1f);
        }
        
        moveVector = new Vector3(0, verticalVel, 0);

        controller.Move(moveVector);
       
        
    }
    void PlayerMovementAndRotation()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * inputZ + right * inputX;
        
        if(blockRotationPlayer == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
        }

    }
    void InputMagnitude()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
        
        //anim.SetFloat("InputZ", inputZ, 0.0f, Time.deltaTime * 2f);
        //anim.SetFloat("InputX", inputX, 0.0f, Time.deltaTime * 2f);

        speed = new Vector2(inputX, inputZ).sqrMagnitude;

        if (speed > allowPlayerRotation)
        {
           // anim.SetFloat("InputMagnitude", speed, 0.0f, Time.deltaTime * 2f);
            controller.SimpleMove(desiredMoveDirection*20f);
            anim.SetBool("Run", true);
            PlayerMovementAndRotation();
        }else if(speed < allowPlayerRotation)
        {
            anim.SetBool("Run", false);
            // anim.SetFloat("InputMagnitude", speed, 0.0f, Time.deltaTime * 2f);
        }
    }
    void AddGravity(){
        counter += 0.1f;
        verticalVel -= counter;
        //Invoke("ParticlePlay",0.2f);
        counter = 0;
    }
    void ParticlePlay(){
        if (jump == true && isGrounded) { 
        particle.Play();
        sound.Play(4,1);
        jump = false;
        }
    }
}