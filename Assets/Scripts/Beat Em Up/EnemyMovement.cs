using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;
using TMPro;

public class EnemyMovement : MonoBehaviour
{
    private CharacterAnimation enemyAnim;
    private HealthScript healthScript;
    public GameObject enemyLife;
    public BoxCollider col;
    public LifeControler enemyUI;
    private Rigidbody myBody;
    public List<string> groupieNames;
    private CharacterAnimation animationScript;
    public List<string> fanNames;
    public string gname;
    public Image gimage;
    public int score;
    public bool soloHit = false;
    public bool isGroupie;
    public float speed = 10.0f;
    public BoxCollider mainCamera_col;
    public BoxCollider mainCamera_col2;
    private Transform playerTarget;
    private CapsuleCollider capsuleCollider;
    public float chaseDistance = 20.0f;
    public float attack_Distance = 1.0f;
    public float chase_Player_After_Attack = 1f;
    public float counter = 0f;
    private float current_Attack_Time;
    private float default_Attack_Time;

    public bool followPlayer, attackPlayer;

    void Awake()
    {
        Names();
        animationScript = GetComponent<CharacterAnimation>();
        enemyUI = GameObject.FindObjectOfType<LifeControler>();
        gimage = GetComponentInChildren<Image>();
        //gname = GetComponentInChildren<TextMeshProUGUI>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        mainCamera_col = GameObject.Find("Col1").GetComponent<BoxCollider>();
        mainCamera_col2 = GameObject.Find("Col2").GetComponent<BoxCollider>();
        enemyAnim = GetComponent<CharacterAnimation>();
        myBody = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        default_Attack_Time = Random.Range(4.0f, 7.0f);
        healthScript = GetComponent<HealthScript>();
        playerTarget = GameObject.FindWithTag("Player").transform;
    }
    public void Names()
    {
        groupieNames.Add("Amelia");
        groupieNames.Add("Chloe");
        groupieNames.Add("Myra");
        groupieNames.Add("Hannah");
        groupieNames.Add("Selina");
        groupieNames.Add("Charlotte");
        groupieNames.Add("Angy");
        groupieNames.Add("Sophia");
        groupieNames.Add("Magnolia");
        groupieNames.Add("Ruth");
        fanNames.Add("Charles");
        fanNames.Add("Henry");
        fanNames.Add("Gideon");
        fanNames.Add("Edmund");
        fanNames.Add("Austin");
        fanNames.Add("James");
        fanNames.Add("Noah");
        fanNames.Add("Dan");
        fanNames.Add("Titus");
        fanNames.Add("Vincent");
    }
    private void Start()
    {
        this.enabled = true;
        followPlayer = true;
        current_Attack_Time = default_Attack_Time;
        if (!isGroupie)
        {
            gname= fanNames[Random.Range(0, fanNames.Count)];
            speed = 6f;
        }
        else if (isGroupie)
        {
            gname= groupieNames[Random.Range(0, groupieNames.Count)];
            speed = 4.8f;
        } 
    }
    void Update()
    {
        if (playerTarget.GetComponent<PlayerMovementBeat>().is_Dead)
            this.enabled = false;
        default_Attack_Time = Random.Range(3.0f, 6.0f);
        if (healthScript.characterDied)
        {
            this.enabled = false;
        }
        Attack();
    }
    void FixedUpdate()
    {
        FollowTarget();
    }
    void FollowTarget()
    {
        if (!followPlayer || healthScript.characterDied  || !BeatEmupManager.instance.godmode|| !BeatEmupManager.instance.pause)
        {
            speed = 0;
            return;
        }
        if (Vector3.Distance(transform.position, playerTarget.position) < chaseDistance && Vector3.Distance(transform.position, playerTarget.position) > attack_Distance)
        {
            speed = 4.8f;
            transform.LookAt(playerTarget);
            myBody.velocity = transform.forward * speed;
            if (myBody.velocity.sqrMagnitude != 0)
            {
                enemyAnim.Run(true);
            }
            followPlayer = true;
            attackPlayer = false;
        }
        else if (Vector3.Distance(transform.position, playerTarget.position) > chaseDistance && Vector3.Distance(transform.position, playerTarget.position) > attack_Distance)
        {
            if (followPlayer == true)
            {
                myBody.velocity = Vector3.zero;
                enemyAnim.Run(false);
                counter += Time.deltaTime;
                if (counter >= 25f)
                {
                    enemyAnim.PlayLongIdle();
                    counter = 0f;
                }
                //followPlayer = false;
                attackPlayer = false;
            }
        }
        else if (Vector3.Distance(transform.position, playerTarget.position) < attack_Distance && Vector3.Distance(transform.position, playerTarget.position) < chaseDistance)
        {
            myBody.velocity = Vector3.zero;
            enemyAnim.Run(false);
            //followPlayer = true;
            attackPlayer = true;
        }
    }
    void Attack()
    {
        if (!attackPlayer || healthScript.characterDied == true || !BeatEmupManager.instance.pause)
            return;
        current_Attack_Time += Time.deltaTime;
        if (current_Attack_Time > default_Attack_Time)
        {
            enemyAnim.EnemyAttack(Random.Range(0, 2));
            current_Attack_Time = 0;
        }
        if (Vector3.Distance(transform.position, playerTarget.position) > attack_Distance + chase_Player_After_Attack)
        {
            attackPlayer = false;
            followPlayer = true;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 12)
        {
            Physics.IgnoreCollision(mainCamera_col, capsuleCollider);
            Physics.IgnoreCollision(mainCamera_col2, capsuleCollider);
        }
    }
    public void Death()
    {
        if (!soloHit)
            animationScript.Death(0);
        if (soloHit)
            animationScript.Death(1);
    }
}
