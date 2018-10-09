using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerControler : MonoBehaviour {

private CharacterController controler;

public float forwardSpeed;
private float diagonalForwardSpeed;
private float backSpeed;

private float diagonalBackSpeed;
public float jumpSpeed;
public float gravity;
 private Vector3 moveDirection;

 private float inputV;
 private float inputH;
 private float jumpInput;

	// Use this for initialization
	void Start () {
		this.controler=GetComponent<CharacterController> ();

		this.diagonalForwardSpeed=(float)Math.Sqrt(this.forwardSpeed*this.forwardSpeed/2);
		this.backSpeed=this.forwardSpeed /2;
		this.diagonalBackSpeed=(float)Math.Sqrt(this.backSpeed*this.backSpeed/2);

		this.moveDirection= Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
		Rotate();
		Move();
	}
	private void GetInput(){
		this.inputV=Input.GetAxis("Vertical");
		this.inputH=Input.GetAxis("Horizontal");
		this.jumpInput=Input.GetAxisRaw("Jump");

	}
	private void Move(){
		if(this.controler.isGrounded){
			this.gravity=0;

			if(this.inputH==0 && this.inputV==0){
				this.moveDirection.Set(0,0,0);
			}
			else if(this.inputH == 0 && this.inputV > 0){
				this.moveDirection.Set(0,0,this.inputV*this.forwardSpeed);
			}
			else if(this.inputH == 0 && this.inputV < 0){
				this.moveDirection.Set(0,0,this.inputV*this.backSpeed);
			}
			else if(this.inputH > 0 && this.inputV==0){
				this.moveDirection.Set(this.inputH*this.forwardSpeed,0,0);
			}
			else if(this.inputH < 0 && this.inputV==0){
				this.moveDirection.Set(this.inputH*this.forwardSpeed,0,0);
			}
			else if(this.inputH > 0 && this.inputV > 0){
				this.moveDirection.Set(this.inputH*this.diagonalForwardSpeed,0,this.inputV*this.diagonalForwardSpeed);
			}
			else if(this.inputH < 0 && this.inputV > 0){
				this.moveDirection.Set(this.inputH*this.diagonalForwardSpeed,0,this.inputV*this.diagonalForwardSpeed);
			}
			else if(this.inputH > 0 && this.inputV < 0){
				this.moveDirection.Set(this.inputH*this.diagonalBackSpeed,0,this.inputV*this.diagonalBackSpeed);
			}
			else if(this.inputH < 0 && this.inputV < 0){
				this.moveDirection.Set(this.inputH*this.diagonalBackSpeed,0,this.inputV*this.diagonalBackSpeed);
			}
			this.moveDirection=transform.TransformDirection(this.moveDirection);
			if(this.jumpInput>0){
				this.moveDirection.y=this.jumpSpeed;
			}else{
				this.gravity=25f;
				if((this.controler.collisionFlags & CollisionFlags.Above)!=0){
					this.moveDirection.y=0;
				}
			}
			this.moveDirection.y-=this.gravity* Time.deltaTime;
			
			this.controler.Move(this.moveDirection*Time.deltaTime);
		}
	}
	private void Rotate(){
		transform.Rotate(0, Input.GetAxis("Mouse X"),0);
	}
}
