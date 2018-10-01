using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private CharacterController controller;
	private Vector2 axis;
	public GameObject personaje;
	public float speed;
    public SoundPlayer sound;
    public Vector3 moveDirection;
	private float forceToGround = 0;
    public Vector3 transformDirection;
	public Vector3 nullvector;
    public Animator pers;
    private bool ismoving=false;

    public float jumpSpeed;
	private bool jump;
	public float gravityMagnitude = 1.0f;

	void Start ()
	{
		controller = GetComponent<CharacterController>();
	}
   
    void Update ()
	{ 
        
		if(controller.isGrounded && !jump)
		{
			moveDirection.y = forceToGround;
        }
		else
		{
			jump = false;
			moveDirection.y += Physics.gravity.y * gravityMagnitude * Time.deltaTime;
        }

      Quaternion newRotation = Quaternion.LookRotation(moveDirection);
	  Quaternion persrotation=personaje.transform.rotation;
	  newRotation=persrotation;
	    transformDirection = axis.x * transform.right + axis.y * transform.forward;
		 Vector3 persview;
		 persview=personaje.transform.forward;
		moveDirection.x = transformDirection.x * speed;
		moveDirection.y=transformDirection.y * speed;
     	moveDirection.z = transformDirection.z * speed;
       if(ismoving==true){
		   if(moveDirection!=nullvector){
		personaje.transform.forward=moveDirection;
		}else{
			personaje.transform.forward=persview;
			}
		}
			
       if ( moveDirection.x > 0 || moveDirection.z > 0 || moveDirection.x < 0 || moveDirection.z < 0)
        {
            pers.SetBool("Walk",true);
            ismoving = true;
        }else
        {
            ismoving = false;
        }
        if (ismoving == false)
        {
            pers.SetBool("Walk", false);
        }
            controller.Move(moveDirection * Time.deltaTime);
			
        
	}

	public void SetAxis(Vector2 inputAxis)
	{
		axis = inputAxis;
	}

	public void StartJump()
	{
		if(!controller.isGrounded) return;

		moveDirection.y = jumpSpeed;
		jump = true;
	}
}
