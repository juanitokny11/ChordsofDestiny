using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBeat : MonoBehaviour
{
    private CharacterAnimation player_Anim;
    private Animator anim;
    private Rigidbody myBody;
    public bool inAir = false;
    public bool comboAereo = false;
    public float run_Speed = 8.0f;
    public float z_Speed = 3.0f;
    public bool lockrotation;
    Quaternion actualrot;
    Vector3 newPosition;
    private float rotation_Y = -90.0f;
    public float rotation_Speed = 15f;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        transform.rotation = Quaternion.Euler(0, 0, 0);
        myBody = GetComponent<Rigidbody>();
        player_Anim = GetComponent<CharacterAnimation>();
        actualrot = transform.rotation;
        newPosition = transform.position;
    }
    void Update()
    {
        RotatePlayer();
        if(!Input.GetButtonDown("Run"))
            AnimatePlayerWalk();
        else if(Input.GetButtonDown("Run"))
            AnimatePlayerRun();
        if (BeatEmupManager.instance.godmode == true)
            AnimatePlayerJump();
        //AnimateResetJump();
    }
    void FixedUpdate()
    {
        DetectMovement();
    }
    void OnAnimatorMove()
    {
        Animator animator = GetComponent<Animator>();
        if (animator)
        {
            Vector3 newPosition = transform.position;
            if (Input.GetAxisRaw("Jump") !=0 )
            {
                if (BeatEmupManager.instance.godmode == false)
                    newPosition.y += animator.GetFloat("Jumpspeed") * Time.deltaTime;
            }
            if (Input.GetAxisRaw("Submit") != 0)
            {
                if (BeatEmupManager.instance.godmode == false)
                    myBody.useGravity = true;
            }
            if (Input.GetAxisRaw("Horizontal") > 0 )
            {
                actualrot = Quaternion.Euler(0, 0, 0);
                lockrotation = false;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                actualrot = Quaternion.Euler(0,180,0);
                //newPosition.x -= animator.GetFloat("Walkspeed") * Time.deltaTime;
                lockrotation = true;
            } 
            else if (Input.GetAxisRaw("Horizontal") == 0)
            {
                if (lockrotation == true )
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    transform.rotation = actualrot;
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    transform.rotation = actualrot;
                }
            }
            if (lockrotation == true )
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.rotation = actualrot;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.rotation = actualrot;
            }
            transform.position = newPosition;
        }
    }
    void DetectMovement()
    {
        myBody.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * (-run_Speed), myBody.velocity.y, Input.GetAxisRaw("Vertical")* (-z_Speed) );
        //if (Input.GetAxisRaw("Jump") != 0)
            //myBody.velocity =new Vector3( myBody.velocity.x, Input.GetAxisRaw("Jump") * newPosition.y, myBody.velocity.z);
    }
    void RotatePlayer()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
            transform.rotation = Quaternion.Euler(0f,-Mathf.Abs(rotation_Y),0f);
        else if (Input.GetAxisRaw("Horizontal") < 0)
            transform.rotation = Quaternion.Euler(0f,Mathf.Abs(rotation_Y), 0f);
    }
    void AnimatePlayerRun()
    {
        run_Speed = 10f;
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 )
            player_Anim.Run(true);
        else
            player_Anim.Run(false);
    }
    void AnimatePlayerWalk()
    {
        run_Speed = 5f;
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 )
           player_Anim.Walk(true);
        else
           player_Anim.Walk(false);
    }
    void AnimatePlayerJump()
    {
        if (Input.GetAxisRaw("Jump") != 0)
        { 
            player_Anim.Jump();
            inAir = true;
        }
    }
    void AnimateResetJump()
    {
        if (comboAereo)
        {
           
        }
        else if (!comboAereo)
        {
            player_Anim.ResetJump();
            inAir = false;
        }
    }
    void ComboAereoRealizado()
    {
        comboAereo = false;
    }
}
