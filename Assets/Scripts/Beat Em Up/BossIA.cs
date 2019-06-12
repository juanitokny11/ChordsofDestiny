using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIA : MonoBehaviour
{
    public enum Estados { Attack, Invoke, Default, Death }
    public Estados current_Boss_State;
    public GameObject[] enemiesTospawn;
    public Transform[] positionTospawn;
    public int random;
    public int fase;
    public int porcentajeAtaque;
    public int porcentajeInvocar;
    public bool followPlayer, attackPlayer;
    public Transform playerTarget;
    private Rigidbody myBody;
    public float chaseDistance = 20.0f;
    public float attack_Distance = 1.0f;
    public float chase_Player_After_Attack = 1f;
    public float speed = 5.0f;
    public bool Jump = false;
    public bool ResetJump = false;
    public Transform waitingPlace;
    private CapsuleCollider capsuleCollider;
    private CharacterAnimation enemyAnim;
    private HealthScript healthScript;
    public BoxCollider mainCamera_col;
    public BoxCollider mainCamera_col2;
    public BoxCollider mainCamera_backgroundcol;
    private EnemyHealthUI enemyHealth;
    public Transform ResetPosition;
    public BattleZone BossZone;
    public BeatEmupManager gameManager;
    public int scoref1=500;
    public int scoref2=1000;
    public bool outside=true;
    public GameObject invokeEnemy;
    public GameObject invokeEnemy2;
    public GameObject invokeEnemy3;
    public GameObject llave;
    public Transform llavePos;
    public bool invoke = false;
    public bool Chase;
    public Sound sound;
    public GameObject espadaRota;
    public ParticleSystem brokenArm;

    void Start()
    {
        Chase = true;
        sound = GetComponent<Sound>();
        gameManager = FindObjectOfType<BeatEmupManager>();
        this.enabled = true;
        capsuleCollider = GetComponent<CapsuleCollider>();
        healthScript = GetComponent<HealthScript>();
        enemyHealth = GetComponent<EnemyHealthUI>();
        myBody = GetComponent<Rigidbody>();
        enemyAnim = GetComponent<CharacterAnimation>();
        mainCamera_backgroundcol = GameObject.Find("Colider2").GetComponent<BoxCollider>();
        mainCamera_col = GameObject.Find("Col1").GetComponent<BoxCollider>();
        mainCamera_col2 = GameObject.Find("Col2").GetComponent<BoxCollider>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        current_Boss_State = Estados.Default;
        fase = 1;
        porcentajeAtaque = 40;
        porcentajeInvocar = 6;
    }
    void Update()
    {
        if (playerTarget.GetComponent<PlayerMovementBeat>().is_Dead)
            this.enabled = false;
        if (brokenArm.time >= .1f && fase==2)
        {
            sound.Chispas();
        }
       
        if (!outside)
        {
            if (Jump == true)
            {
                Up();
                if (transform.position.y >= 9f)
                {
                    transform.position = waitingPlace.position;
                    Chase = true;
                    StopJumpUp();
                }
            }
            if (ResetJump == true)
            {
                if (fase == 1)
                    enemyAnim.ResetJump2Arms();
                else
                    enemyAnim.ResetJump1Arm();
                Down();
                if (transform.position.y <= -0.06041813f)
                {
                    outside = true;
                    StopJumpDown();
                }  
            }
        }
        else if (outside)
        {
            if (Jump == true)
            {
                Up();
                
                if (transform.position.y >= 9f)
                {
                    if(transform.position.x != playerTarget.transform.position.x)
                    {
                        transform.position = ResetPosition.position;
                        Chase = false;
                        StopJumpUp();
                    }
                    else if (transform.position.x == playerTarget.transform.position.x)
                    {
                        transform.position = new Vector3( ResetPosition.position.x+3f, ResetPosition.position.y, ResetPosition.position.z);
                        Chase = false;
                        StopJumpUp();
                    }
                }
            }
            if (ResetJump == true)
            {
                if (fase == 1)
                    enemyAnim.ResetJump2Arms();
                else
                    enemyAnim.ResetJump1Arm();
                Down();
                if (transform.position.y <= -0.06041813f)
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    outside = false;
                    StopJumpDown();
                } 
            }
        }
        switch (current_Boss_State)
        {
            case Estados.Default:
                DefaultState();
                break;
            case Estados.Death:
                Death();
                break;
            default:
                break;
        }
    }
    void Attack()
    {
        if (!BeatEmupManager.instance.pause)
            return;
        transform.LookAt(playerTarget);
        if (enemyHealth.HealthBar.fillAmount <= 0.35)
        {
            enemyAnim.RomperEspada();
        }
        random = Random.Range(1, 101);
        if (random >= porcentajeAtaque)
        {
            if (fase == 1)
                enemyAnim.Attack2arms(0);
            else if (fase == 2)
                enemyAnim.Attack1arm(0);
        }
        else if (random < porcentajeAtaque)
        {
            if (fase == 1)
                enemyAnim.Attack2arms(1);
            else if (fase == 2)
                enemyAnim.Attack1arm(1);
        }
    }
    void DefaultState()
    {
        if (!followPlayer || healthScript.characterDied || !BeatEmupManager.instance.pause )
        {
            speed = 0;
            return;
        }
        if (enemyHealth.HealthBar.fillAmount <= 0.35)
        {
            enemyAnim.RomperEspada();
        }
        if (Chase)
        {
            if (Vector3.Distance(transform.position, playerTarget.position) < chaseDistance && Vector3.Distance(transform.position, playerTarget.position) > attack_Distance)
            {
                transform.LookAt(playerTarget);
                myBody.velocity = transform.forward * speed;
                if (myBody.velocity.sqrMagnitude != 0)
                {
                    if (fase == 1)
                        enemyAnim.Walk2arm(true);
                    else if (fase == 2)
                        enemyAnim.Walk1arm(true);
                }
                followPlayer = true;
                attackPlayer = false;
            }
            else if (Vector3.Distance(transform.position, playerTarget.position) > chaseDistance && Vector3.Distance(transform.position, playerTarget.position) > attack_Distance)
            {
                if (followPlayer == true)
                {
                    transform.LookAt(playerTarget);
                    myBody.velocity = Vector3.zero;
                    if (fase == 1)
                        enemyAnim.Walk2arm(false);
                    else if (fase == 2)
                        enemyAnim.Walk1arm(false);
                    //followPlayer = false;
                    attackPlayer = false;
                }
            }
            else if (Vector3.Distance(transform.position, playerTarget.position) < attack_Distance && Vector3.Distance(transform.position, playerTarget.position) < chaseDistance)
            {
                myBody.velocity = Vector3.zero;
                if (fase == 1)
                    enemyAnim.Walk2arm(false);
                else if (fase == 2)
                    enemyAnim.Walk1arm(false);
                //followPlayer = true;
                attackPlayer = true;
                SetAttack();
            }
        }
        if (BossZone.enemiescounter == 2 && invoke == true)
            {
                if (fase == 1)
                {
                    enemyAnim.Jump2Arms();
                    Inside();
                    //enemyAnim.ResetJump2Arms();
                }
                else if (fase == 2)
                {
                    enemyAnim.Jump1Arm();
                    //enemyAnim.ResetJump1Arm();
                }
            }
    }
    void Invoke()
    {
        if (!BeatEmupManager.instance.pause)
            return;
        if (fase == 1)
        {
            invokeEnemy=Instantiate(enemiesTospawn[Random.Range(0, enemiesTospawn.Length)], positionTospawn[0].position, Quaternion.identity);
            invokeEnemy2 = Instantiate(enemiesTospawn[Random.Range(0, enemiesTospawn.Length)], positionTospawn[1].position, Quaternion.identity);
            invokeEnemy.GetComponent<HealthScript>().zone = BossZone;
            invokeEnemy2.GetComponent<HealthScript>().zone = BossZone;
            invokeEnemy.GetComponent<EnemyMovement>().chaseDistance = 40f;
            invokeEnemy2.GetComponent<EnemyMovement>().chaseDistance = 40f;
            BossZone.enemiescounter += 2;
            enemyAnim.Jump2Arms();
            //enemyAnim.ResetJump2Arms();
            Invoke("OUTSIDE", 0.7f);
            //invoke = true;
        }
        else if (fase == 2)
        {
            enemyAnim.Jump1Arm();
            //enemyAnim.ResetJump1Arm();
            invokeEnemy = Instantiate(enemiesTospawn[Random.Range(0, enemiesTospawn.Length)], positionTospawn[0].position, Quaternion.identity);
            invokeEnemy2 = Instantiate(enemiesTospawn[Random.Range(0, enemiesTospawn.Length)], positionTospawn[0].position, Quaternion.identity);
            invokeEnemy3 = Instantiate(enemiesTospawn[Random.Range(0, enemiesTospawn.Length)], positionTospawn[1].position, Quaternion.identity);
            invokeEnemy.GetComponent<HealthScript>().zone = BossZone;
            invokeEnemy2.GetComponent<HealthScript>().zone = BossZone;
            invokeEnemy3.GetComponent<HealthScript>().zone = BossZone;
            BossZone.enemiescounter += 3;
            Invoke("OUTSIDE", 0.7f);
            //invoke = true;
        }
        //invoke = true;
    }
    public void Death()
    {
        this.enabled = false;
    }
    void SetAttack()
    {
        current_Boss_State = Estados.Attack;
        Attack();
    }
    public void SetInvoke()
    {
        current_Boss_State = Estados.Invoke;
        Invoke();
    }
    public void SetDefault()
    {
        current_Boss_State = Estados.Default;
    }

    public void ChangeFase()
    {
        fase = 2;
        porcentajeAtaque = 50;
        porcentajeInvocar = 200;
        if(!playerTarget.GetComponent<PlayerMovementBeat>().lockrotation)
        espadaRota.transform.rotation = Quaternion.Euler(transform.rotation.x,transform.rotation.y, transform.rotation.z);
        else if (playerTarget.GetComponent<PlayerMovementBeat>().lockrotation)
            espadaRota.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        //espadaRota.transform.position = new Vector3(espadaRota.transform.position.x, 0, espadaRota.transform.position.z);
        gameManager.numScore += scoref1;
        brokenArm.Play();
        //sound.brazorot();
        SetDefault();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 12)
        {
            Physics.IgnoreCollision(mainCamera_col, capsuleCollider);
            Physics.IgnoreCollision(mainCamera_col2, capsuleCollider);
            Physics.IgnoreCollision(mainCamera_backgroundcol, capsuleCollider);
        }
    }
    private void Down()
    {
        StartCoroutine(DownInTheAir());
    }
    private void Up()
    {
        StartCoroutine(UpInTheAir());
    }
    IEnumerator DownInTheAir()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z);
        yield return new WaitForEndOfFrame();
    }
    IEnumerator UpInTheAir()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
        yield return new WaitForEndOfFrame();
    }
    private void JumpUp()
    {
        Jump = true;
    }
    private void StopJumpUp()
    {
        Jump = false;
        ResetJump = true;
    }
    private void JumpDown()
    {
        ResetJump = true;
    }
    private void StopJumpDown()
    {
        ResetJump = false;
    }
    private void OUTSIDE()
    {
        outside = true;
        invoke = true;
    }
    private void Inside()
    {
        outside = false;
        invoke = false;
    }
    public void InstantiateTarjeta()
    {
        Instantiate(llave, transform.position + new Vector3(0,2f,0), Quaternion.identity);
    }
}
