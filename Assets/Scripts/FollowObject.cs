using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {
	private Transform Other;
	
	//private float RotationSpeed=20f;
	public float speed;
	private Transform trans;
	public Transform pers;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		Other=GameObject.Find("Player").transform;
		 offset = Other.position - transform.position;
		 trans=GetComponent<Transform>();
		 
	}
	// Update is called once per frame
	void Update () {
		transform.position=Other.position-offset;
		//trans.rotation = Quaternion.RotateTowards(trans.rotation, pers.rotation, speed);
		//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pers.transform.forward), 0.4f);
		//transform.Rotate ( Vector3.up * ( RotationSpeed * Time.deltaTime ) );
		//transform.rotation=pers.transform.rotation;
	}
}
