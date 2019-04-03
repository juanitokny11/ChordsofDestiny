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
    private float current_Attack_Time;
    private float default_Attack_Time;
    private CharacterAnimation enemyAnim;
    private HealthScript healthScript;

    void Start()
    {
        healthScript=GetComponent<HealthScript>();
        myBody = GetComponent<Rigidbody>();
        enemyAnim = GetComponent<CharacterAnimation>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        current_Boss_State = Estados.Default;
        fase = 1;
        porcentajeAtaque = 40;
        porcentajeInvocar = 15;
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
        if (!attackPlayer || healthScript.characterDied == true)
            return;
        current_Attack_Time += Time.deltaTime;
        if (current_Attack_Time > default_Attack_Time)
        {
            random = Random.Range(1, 101);

            if (random >= porcentajeAtaque)
            {
                if (fase == 1)
                    enemyAnim.Attack2arms(2);
                else if (fase == 2)
                    enemyAnim.Attack1arm(2);
            }
            else
            {
                if (fase == 1)
                    enemyAnim.Attack2arms(1);
                else if (fase == 2)
                    enemyAnim.Attack2arms(1);
            }
            current_Attack_Time = 0;
        }
        if (Vector3.Distance(transform.position, playerTarget.position) > attack_Distance + chase_Player_After_Attack)
        {
            attackPlayer = false;
            followPlayer = true;
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
    void Invoke()
    {
        if (fase == 1)
            Instantiate(enemiesTospawn[Random.Range(3, 4)], positionTospawn[Random.Range(0, 1)].position, Quaternion.identity);
        else
            Instantiate(enemiesTospawn[Random.Range(4, 5)], positionTospawn[Random.Range(0, 1)].position, Quaternion.identity);

        current_Boss_State = Estados.Default;
    }
    public void Death()
    {
        this.enabled = false;
    }
    void SetAttack()
    {
        current_Boss_State = Estados.Attack;
        if (fase == 2)
            porcentajeAtaque = 50;
        Attack();
    }
    public void SetInvoke()
    {
        current_Boss_State = Estados.Invoke;
        if (fase == 2)
            porcentajeInvocar = 10;
        Invoke();
    }
    public void SetDefault()
    {
        current_Boss_State = Estados.Default;
    }
}
