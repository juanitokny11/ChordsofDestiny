using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {
	private GameObject Other;
	//private float RotationSpeed=20f;
	public GameObject pers;
	private Vector3 offset;

	// Use this for initialization
	void Awake () {
		Other=GameObject.Find("Player");
		 offset = Other.transform.position - transform.position;
	}
	// Update is called once per frame
	void Update () {
		transform.position=Other.transform.position-offset;
		//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pers.transform.forward), 0.4f);
		//transform.Rotate ( Vector3.up * ( RotationSpeed * Time.deltaTime ) );
		//transform.rotation=pers.transform.rotation;
	}
}
