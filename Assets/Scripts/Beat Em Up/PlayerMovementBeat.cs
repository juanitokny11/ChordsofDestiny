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
    public AudioSource caminarS;
    public AudioSource soloS;
    public bool enableMovement;
    public PlayerAttackList attackList;
    public PlayerAttack2 playerAttack;
    public float run_Speed;
    public float z_Speed;
    public bool lockrotation;
    public bool move;
    public bool walk=false;
    public bool running;
    public bool canRotate = true;
    public bool is_Dead=false;
    Quaternion actualrot;
    Vector3 newPosition;
    public float counter;
    private float rotation_Y = -90.0f;
    public float rotation_Speed = 15f;
    private void Awake()
    {
        enableMovement = true;
        //canRotate = false;
        attackList = GetComponent<PlayerAttackList>();
        anim = GetComponent<Animator>();
        transform.rotation = Quaternion.Euler(0, 0, 0);
        myBody = GetComponent<Rigidbody>();
        player_Anim = GetComponent<CharacterAnimation>();
        actualrot = transform.rotation;
        newPosition = transform.position;
        playerAttack = GetComponent<PlayerAttack2>();
    }
    void Update()
    {

        if (enableMovement)
        {
            if(!walk)
                counter++;
                if (counter == 500f)
                {
                    player_Anim.PlayLongIdle();
                }
            else if (walk)
            {
                counter = 0;
            }

            RotatePlayer();
            if (playerAttack.current_Combo_State == PlayerAttack2.ComboState.NONE)
            {
                move = true;
            }
            if (!is_Dead)
            {
                AnimatePlayerRun();
                AnimatePlayerWalk();
                if (walk == true && running == false)
                    run_Speed = 5;
                else if (walk == true && running == true)
                    run_Speed = 10f;
                if (Input.GetButtonDown("Run"))
                {
                    running = true;
                }
                if (Input.GetButtonUp("Run"))
                {
                    caminarS.Stop();
                    running = false;
                    counter = 0;
                }
            }
            if (BeatEmupManager.instance.godmode == true)
                AnimatePlayerJump();
            //AnimateResetJump();
        }
    }
    public void Start()
    {
        move = true;
    }
    void FixedUpdate()
    {
        
        if (enableMovement)
        {
            if (move)
                DetectMovement();
        }
    }
    public void CounterReset()
    {
        counter = 0;
    }
    void OnAnimatorMove()
    {
        Animator animator = GetComponent<Animator>();
        if (!is_Dead || enableMovement)
        {
            if (animator)
            {
                Vector3 newPosition = transform.position;
                if (BeatEmupManager.instance.godmode == false)
                    myBody.useGravity = true;
                if (Input.GetAxisRaw("Vertical") > 0)
                    walk = true;
                else if (Input.GetAxisRaw("Vertical") < 0)
                    walk = true;
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    actualrot = Quaternion.Euler(0, 0, 0);
                    walk = true;
                    if(canRotate)
                        lockrotation = false;
                }
                else if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    actualrot = Quaternion.Euler(0, 180, 0);
                    walk = true;
                    //newPosition.x -= animator.GetFloat("Walkspeed") * Time.deltaTime;
                    if (canRotate)
                        lockrotation = true;
                }
                else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
                {
                    walk = false;
                    player_Anim.Run(false);
                    caminarS.Stop();
                }
                if (canRotate)
                {
                    if (lockrotation == true)
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        transform.rotation = actualrot;
                    }
                    else if (!lockrotation)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        transform.rotation = actualrot;
                    }
                }
                transform.position = newPosition;
            }
        }
    }
    void caminar()
    {
        caminarS.pitch = Random.Range(0.7f,1.3f);
        caminarS.volume = Random.Range(0.15f,0.45f);
        caminarS.Play();
    }
    void Ssolo()
    {
        soloS.Play();
    }
    void DetectMovement()
    {
        if (!is_Dead)
        {
            myBody.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * (-run_Speed), myBody.velocity.y, Input.GetAxisRaw("Vertical") * (-z_Speed));
            if (Input.GetAxisRaw("Jump") != 0)
            {
                myBody.velocity = new Vector3(myBody.velocity.x, Input.GetAxisRaw("Jump") * newPosition.y, myBody.velocity.z);
            }    
        }
    }
    void RotatePlayer()
    {
        if (canRotate)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
                transform.rotation = Quaternion.Euler(0f, -Mathf.Abs(rotation_Y), 0f);
            else if (Input.GetAxisRaw("Horizontal") < 0)
                transform.rotation = Quaternion.Euler(0f, Mathf.Abs(rotation_Y), 0f);
        }
    }
    void AnimatePlayerRun()
    {
        if (walk == true && running ==true)
            player_Anim.Run(true);
        else if (running == false)
            player_Anim.Run(false);
    }
    void AnimatePlayerWalk()
    {
        if (walk==true && running==false)
           player_Anim.Walk(true);
        else if(walk==false)
           player_Anim.Walk(false);
    }
    void AnimatePlayerJump()
    {
        if (Input.GetAxisRaw("Jump") != 0 && inAir==false)
        {
            playerAttack.current_Combo_State = PlayerAttack2.ComboState.JUMP;
            player_Anim.Jump();
            inAir = true;
            playerAttack.blockActivated = false;
        }
    }
    void AnimateResetJump()
    {
         if (!comboAereo)
        {
            player_Anim.ResetJump();
        }
    }
    void ComboAereoRealizado()
    {
        comboAereo = false;
    }
    public void NotMove()
    {
        move = false;
        attackList.Attack = false;
    }
    public void Move()
    {
        move = true;
        attackList.Attack = true;
    }
}
