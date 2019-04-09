using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIA : MonoBehaviour
{
    public enum Estados { Attack,Invoke,Default,Death}
    public Estados current_Boss_State;
    public GameObject[] enemiesTospawn;
    public Transform[] positionTospawn;
    public int random;
    public int fase ;
    public int porcentajeAtaque;
    public int porcentajeInvocar;
    public bool followPlayer, attackPlayer;
    public Transform playerTarget;
    private Rigidbody myBody;
    public float chaseDistance = 20.0f;
    public float attack_Distance = 1.0f;
    public float chase_Player_After_Attack = 1f;
    public float speed = 5.0f;
    private CapsuleCollider capsuleCollider;
    private CharacterAnimation enemyAnim;
    private HealthScript healthScript;
    public BoxCollider mainCamera_col;
    public BoxCollider mainCamera_col2;
    public BoxCollider mainCamera_backgroundcol;
    private EnemyHealthUI enemyHealth;


    void Start()
    {

        capsuleCollider = this.GetComponent<CapsuleCollider>();
        healthScript =GetComponent<HealthScript>();
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
        porcentajeInvocar = 8;
    }
    void Update()
    {
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
        if (enemyHealth.HealthBar.fillAmount <= 0.5)
        {
            enemyAnim.RomperEspada();
        }
        random = Random.Range(1, 101);
        if (random >= porcentajeAtaque)
        {
            if (fase == 1)
                enemyAnim.Attack2arms(0);
            else
                enemyAnim.Attack1arm(0);    
        }
        else if(random < porcentajeAtaque)
        {
            if (fase == 1)
                enemyAnim.Attack2arms(1);
           else
                enemyAnim.Attack1arm(1);
        }
    }
    void DefaultState()
    {
        if (!followPlayer || healthScript.characterDied )
        {
            speed = 0;
            return;
        }
        if (Vector3.Distance(transform.position, playerTarget.position) < chaseDistance && Vector3.Distance(transform.position, playerTarget.position) > attack_Distance)
        {
            transform.LookAt(playerTarget);
            myBody.velocity = transform.forward * speed;
            if (myBody.velocity.sqrMagnitude != 0)
            {
                if (fase == 1)
                    enemyAnim.Walk2arm(true);
                else
                    enemyAnim.Walk1arm(true);     
            }
            followPlayer = true;
            attackPlayer = false;
        }
        else if (Vector3.Distance(transform.position, playerTarget.position) > chaseDistance && Vector3.Distance(transform.position, playerTarget.position) > attack_Distance)
        {
            if (followPlayer == true)
            {
                myBody.velocity = Vector3.zero;
                if (fase == 1)
                    enemyAnim.Walk2arm(false);
               else
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
            else
                enemyAnim.Walk1arm(false);
            //followPlayer = true;
            attackPlayer = true;
            SetAttack();
        }
    }
    void Invoke()
    {
        if (fase == 1)
            Instantiate(enemiesTospawn[Random.Range(3, 4)], positionTospawn[Random.Range(0, 1)].position, Quaternion.identity);
        else
            Instantiate(enemiesTospawn[Random.Range(4, 5)], positionTospawn[Random.Range(0, 1)].position, Quaternion.identity);
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
        porcentajeInvocar = 4;
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
}
