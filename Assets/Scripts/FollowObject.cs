using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {
	private Transform Other;
	
	//private float RotationSpeed=20f;
	public float speed;
	private Transform trans;
	public Transform pers;
	public Vector3 offset;
    Vector3 vel = Vector3.zero;
    public float smoothTime = .1f;
	// Use this for initialization
	void Start () {
		Other=GameObject.Find("Player").transform;
		 trans=GetComponent<Transform>();
		 
	}
	// Update is called once per frame
	void LateUpdate () {
		//trans.position=Other.position;
        trans.position = Vector3.SmoothDamp(trans.position, Other.position + offset, ref vel, smoothTime);
		//trans.rotation = Quaternion.RotateTowards(trans.rotation, pers.rotation, speed);
		//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pers.transform.forward), 0.4f);
		//transform.Rotate ( Vector3.up * ( RotationSpeed * Time.deltaTime ) );
		//transform.rotation=pers.transform.rotation;
	}
}
