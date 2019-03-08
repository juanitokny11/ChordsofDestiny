using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private CharacterAnimation enemyAnim;
    private Rigidbody myBody;
    public float speed = 5.0f;

    private Transform playerTarget;
    public float attack_Distance = 1.0f;
    private float chase_Player_After_Attack = 1f;

    private float current_Attack_Time;
    private float default_Attack_Time = 2.0f;

    private bool followPlayer, attackPlayer;

    void Awake()
    {
        enemyAnim = GetComponentInChildren<CharacterAnimation>();
        myBody = GetComponent<Rigidbody>();

        playerTarget = GameObject.FindWithTag("Player").transform;
    }
    private void Start()
    {
        followPlayer = true;
        current_Attack_Time = default_Attack_Time;
    }
    void Update()
    {
        Attack();
    }
    void FixedUpdate()
    {
        FollowTarget();
    }
    void FollowTarget()
    {
        if (!followPlayer)
            return;
        if(Vector3.Distance(transform.position,playerTarget.position) > attack_Distance)
        {
            transform.LookAt(playerTarget);
            myBody.velocity = transform.forward * speed;

            if (myBody.velocity.sqrMagnitude != 0)
            {
                enemyAnim.Run(true);
            }
            
        }else if (Vector3.Distance(transform.position, playerTarget.position) < attack_Distance)
        {
            myBody.velocity = Vector3.zero;
            enemyAnim.Run(false);

            followPlayer = true;
            attackPlayer = true;
        }

    }
    void Attack()
    {
        if (!attackPlayer)
            return;
        current_Attack_Time += Time.deltaTime;
        if (current_Attack_Time > default_Attack_Time)
        {
            enemyAnim.EnemyAttack(Random.Range(0, 2));
            current_Attack_Time = 0;

        }
        if(Vector3.Distance(transform.position, playerTarget.position) > attack_Distance + chase_Player_After_Attack)
        {
            attackPlayer = false;
            followPlayer = true;
        }
    }
}
