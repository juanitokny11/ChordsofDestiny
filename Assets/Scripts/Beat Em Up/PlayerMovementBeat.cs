using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBeat : MonoBehaviour
{
    public DetectEnemy detector;
    public DetectEnemy2 detectorz;
    private CharacterAnimation player_Anim;
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
    public bool lockrotation; // left true right false
    public bool move;
    public bool attack;
    public bool jump;
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
        detector = GetComponent<DetectEnemy>();
        detectorz = GetComponent<DetectEnemy2>();
        enableMovement = false;
        jump = false;
        canRotate = false;
        attackList = GetComponent<PlayerAttackList>();
        transform.rotation = Quaternion.Euler(0, 0, 0);
        myBody = GetComponent<Rigidbody>();
        player_Anim = GetComponent<CharacterAnimation>();
        actualrot = transform.rotation;
        newPosition = transform.position;
        playerAttack = GetComponent<PlayerAttack2>();
    }
    void Update()
    {
        if (!is_Dead)
        {
            if (enableMovement)
        {
            if(!walk && !attack)
                counter++;
                if (counter == 500f)
                {
                    player_Anim.PlayLongIdle();
                }
            else if (walk || attack || jump)
            {
                counter = 0;
                if(lockrotation)
                    transform.rotation = Quaternion.Euler(0,180,0);
                if (!lockrotation)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            RotatePlayer();
            if (playerAttack.current_Combo_State == PlayerAttack2.ComboState.NONE)
            {
                move = true;
            }
                AnimatePlayerRun();
                AnimatePlayerWalk();
                if (detectorz.TargetDetected)
                {
                    z_Speed = 0;
                }  
                if (detector.TargetDetected)
                {
                    run_Speed = 0;
                }
                else if (walk == true && running == false && Input.GetAxisRaw("Evadir") == 1 && playerAttack.blockActivated)
                    run_Speed = 0;
                else if (walk == true && running == false && Input.GetAxisRaw("Evadir") == 0)
                {
                    run_Speed = 5f;
                    z_Speed = 3f;
                } 
                else if (walk == true && running == true)
                {
                    run_Speed = 10f;
                    z_Speed = 6f;
                }
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
        player_Anim.Inicio();
    }
    void FixedUpdate()
    {
        detector.MyFixedUpdate();
        detectorz.MyFixedUpdate();
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
                    //canRotate = true;
                    if(canRotate)
                        lockrotation = false;

                    detector.SetOrientation(lockrotation);
                }
                else if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    actualrot = Quaternion.Euler(0, 180, 0);
                    walk = true;
                    //newPosition.x -= animator.GetFloat("Walkspeed") * Time.deltaTime;
                    //canRotate = true;
                    if (canRotate)
                        lockrotation = true;

                    detector.SetOrientation(lockrotation);
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
        if (BeatEmupManager.instance.pause)
        {
            caminarS.pitch = Random.Range(0.7f, 1.3f);
            caminarS.volume = Random.Range(0.35f, 0.5f);
            caminarS.Play();
        }
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
        if (jump)
        {
            if (Input.GetAxisRaw("Jump") != 0 && inAir == false)
            {
                playerAttack.current_Combo_State = PlayerAttack2.ComboState.JUMP;
                player_Anim.Jump();
                inAir = true;
                playerAttack.blockActivated = false;
            }
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
        Invoke("CanMove",2f);
        attackList.Attack = true;
    }
    public void CanMove()
    {
        move = true;
    }
}
