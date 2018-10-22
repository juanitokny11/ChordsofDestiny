using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {
	private GameObject Other;
	private Vector3 offset;
	// Use this for initialization
	void Awake () {
		Other=GameObject.Find("Player");
		 offset = Other.transform.position - transform.position;
	}
	// Update is called once per frame
	void Update () {
		transform.position=Other.transform.position-offset;
		transform.rotation=Other.transform.rotation;
	}
}
