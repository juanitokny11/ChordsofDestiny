using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBehaviour : MonoBehaviour
{
    public enum State { Idle, Invocar, Chase, Attack, Dead};
    public State state;

    public Spawner spawn;
    private NavMeshAgent agent;
    private Animator anim;
    public GameObject [] spawners;
    public float counter = 0.0f;
     public float counterInvoke = 0.0f;

    //public SoundPlayer sound;
    private CapsuleCollider colider;
    private BoxCollider attackcollider;
    public  MyGameManager manager;
    public bool activarcounter=false;
    //public metronomo met;

    [Header("Creeper properties")]
    public int life = 10;

    [Header("Target Detection")]
    public float radius;
    public float idleRadius;
    public float chaseRadius;
    public LayerMask targetMask;
    public bool targetDetected = false;
    private Transform targetTransform;

    [Header("Patrol path")]
    public bool stopAtEachNode = true;
    public float timeStopped = 1.0f;
    private float timeCounter = 0.0f;
    public Transform[] pathNodes;
    private int currentNode = 0;
    private bool nearNode = false;

    [Header("Explosion properties")]
    public float explodeDistance;
    public float explosionRadius;
    public float explosionForce;

    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        attackcollider = GetComponent<BoxCollider>();
        colider = GetComponent<CapsuleCollider>();
        /*for(int i=0;i<spawners.Length;i++)
        {
            spawners[i]= GameObject[i].FindGameObjectsWithTag("Spawn");
        }*/
       // sound = GetComponentInChildren<SoundPlayer>();

        nearNode = true;
        SetIdle();        
	}
	
	// Update is called once per frame
	void Update ()
    {   
        
        switch(state)
        {
            case State.Idle:
                Idle();
                break;
            case State.Invocar:
                Invocar();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Attack:
                Explode();
                break;
            case State.Dead:
                Dead();
                break;
            default:
                break;
        }
    }
    private void FixedUpdate()
    {
        targetDetected = false;
        Collider[] cols = Physics.OverlapSphere(this.transform.position, radius, targetMask);
        if(cols.Length != 0)
        {
            targetDetected = true;
            targetTransform = cols[0].transform;            
        }
    }

    void Idle()
    {
        if(targetDetected)
        {
            SetChase();
            return;
        }

        if(timeCounter >= timeStopped)
        {
            if(nearNode) GoNearNode();
            else GoNextNode();
           
        }
        else timeCounter += Time.deltaTime;
    }
    void Chase()
    {
        if(!targetDetected)
        {
            nearNode = true;
            SetIdle();
            return;
        }
        agent.SetDestination(targetTransform.position);

        if(Vector3.Distance(transform.position, targetTransform.position) <= explodeDistance)
        {
            SetExplode();
        }


    }
     void Invocar()
    {
        if(targetDetected)
        {
            SetInvocar();
            return;
        }else {
             SetChase();
             return;
        }
    }
    void Explode() { 
        if(counter>=3){
         state=State.Invocar;
         counter=0;
         }
         //state=State.Idle;
    }
    void Dead() {

     }

    void SetIdle()
    {
        agent.isStopped = true;        
       // anim.SetBool("Walk", false);
        //anim.SetBool("Run", false);
        radius = idleRadius;
        timeCounter = 0;
        /*if (manager.pause == true)
        {
            sound.Play(1, 1);
        }*/

        state = State.Idle;
    }
    void SetInvocar()
    {  
        spawn.SetSpawn(2);

        state=State.Attack;
    }
    void SetChase()
    {
        agent.isStopped = false;
        agent.SetDestination(targetTransform.position);
        agent.stoppingDistance = 2.0f;
        //anim.SetBool("Run", true);
        //anim.SetBool("Walk", false);
        radius = chaseRadius;
       /* if (manager.pause == true)
        {
            sound.Play(3, 1);
        }*/
        state = State.Chase;
    }
    void SetExplode()
    {
        
      /*  if (manager.pause == true)
        {
            sound.Play(4, 1);
        }*/
        agent.isStopped = true;
        //transform.tag = "Enemy";
        attackcollider.enabled = true;
        counter++;
       // anim.SetBool("Attack",true);
        Invoke("ResetAttack", 2);
        state = State.Attack;
    }
    void SetDead()
    {
       /* if (manager.pause == true)
        {
            sound.Play(0, 1);
        }*/
        agent.isStopped = true;
        state = State.Dead;
        //anim.SetTrigger("Die");
        Invoke("DestroyEnemy", 3);
    }

    void GoNextNode()
    {
        currentNode++;
        if(currentNode >= pathNodes.Length) currentNode = 0;

        agent.SetDestination(pathNodes[currentNode].position);
    }
    void GoNearNode()
    {
        nearNode = false;
        float minDistance = Mathf.Infinity;
        for(int i = 0; i < pathNodes.Length; i++)
        {
            if(Vector3.Distance(transform.position, pathNodes[i].position) < minDistance)
            {
                minDistance = Vector3.Distance(transform.position, pathNodes[i].position);
                currentNode = i;
            }
        }
        agent.SetDestination(pathNodes[currentNode].position);
    }

    public void Explosion()
    {
       /* Collider[] cols = Physics.OverlapSphere(this.transform.position, explosionRadius);

        foreach(Collider c in cols)
        {
            if(c.attachedRigidbody != null)
            {
                //c.attachedRigidbody.AddExplosionForce(explosionForce, this.transform.position, explosionRadius, 1, ForceMode.Impulse);
            }
        }
       // int random = Random.Range(4, 8);
       // sound.Play(random, 1);
       // explosionPS.Play();
       // explosionPS.transform.parent = null*/


       // SetDead();
    }
    private void OnDrawGizmos()
    {
        Color color = Color.green;
        if(targetDetected) color = Color.red;
        color.a = 0.1f;
        Gizmos.color = color;
        Gizmos.DrawSphere(this.transform.position, radius);
    }

    public void Damage(int hit)
    {
        if(state == State.Dead) return;
        life -= hit;
        //sound.Play(2, 1);
        if (life <= 0) SetDead();
    }
    private void OnTriggerEnter(Collider other)
    {
        /*if (other.tag == "ligero" && metronomo.getInstance().daño==true)
        {
            Damage(3);
            Debug.Log("dañoextra");
             FPSInputManager.getInstance().Carga();
        }*/
       if (other.tag == "ligero")
        {
             Damage(2);
              MyGameManager.getInstance().Carga();
        }
           /* if (other.tag == "pesado" && metronomo.getInstance().daño== true)
        {
            Damage(4);
            Debug.Log("dañoextra");
             FPSInputManager.getInstance().Carga();
        }Ç*/
         if (other.tag == "pesado")
        {
            Damage(3);
              MyGameManager.getInstance().Carga();
        }
    }
    /*void OnCollisionEnter(Collision other) {

		if(other.gameObject.tag == "arma") {
			Damage (2);	
		}
    }*/
    void ResetAttack()
    {
        //anim.SetBool("Attack", false);
        agent.isStopped = false;
        //transform.tag = "Passive";
        attackcollider.enabled = false;
    }
    void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
}
