using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBeat : MonoBehaviour
{
    private CharacterAnimation player_Anim;
    private Rigidbody myBody;

    public float run_Speed = 6.0f;
    public float z_Speed = 3.0f;
    public bool lockrotation;
    Quaternion actualrot;
    Vector3 newPosition;
    private float rotation_Y = -90.0f;
    public float rotation_Speed = 15f;
    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
        player_Anim = GetComponent<CharacterAnimation>();
        actualrot = transform.rotation;
        newPosition = transform.position;
    }
    void Update()
    {
        RotatePlayer();
        AnimatePlayerRun();
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
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                newPosition.x += animator.GetFloat("Runspeed") * Time.deltaTime;
                actualrot = Quaternion.Euler(0, 0, 0);
                lockrotation = false;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                actualrot = Quaternion.Euler(0,180,0);
                newPosition.x -= animator.GetFloat("Runspeed") * Time.deltaTime;
                lockrotation = true;
            }
            else if (Input.GetAxisRaw("Horizontal") == 0)
            {
                actualrot = Quaternion.Euler(Vector3.forward);
            }
            if (lockrotation == true)
            {
                transform.rotation= Quaternion.Euler(0, 180, 0);
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
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            player_Anim.Run(true);
        else
            player_Anim.Run(false);
    }
}
