using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour {

	public Collider[] patrolPoints;
	private int currentPoint = 0;
	// Use this for initialization
	private NavMeshAgent agent;
	

	void Start(){
		agent = GetComponent<NavMeshAgent> ();
		agent.SetDestination(patrolPoints[currentPoint].gameObject.transform.position);
	}
	
	void OnTriggerEnter(Collider col){
		currentPoint++;
		currentPoint = currentPoint%patrolPoints.Length;
		agent.SetDestination(patrolPoints[currentPoint].gameObject.transform.position);
	}
}
