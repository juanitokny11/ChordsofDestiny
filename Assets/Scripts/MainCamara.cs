using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamara : MonoBehaviour {
 
 public Transform lookAt;
 Vector3 positionIncrease;
 public float distance;
 float fixedDist;

 float sensitivityWheel;

	
	void Start () {
		this.positionIncrease=this.lookAt.forward*this.distance;
		transform.position=this.lookAt.position-this.positionIncrease;
		transform.LookAt(this.lookAt);
		this.sensitivityWheel=3f;
	}
	
	// Update is called once per frame
	void Update () {
		this.distance+=Input.GetAxis("Mouse ScrollWheel")*this.sensitivityWheel;
		this.distance=Mathf.Clamp(this.distance,23f,25f);
		this.fixedDist=fixDistance();
	}
	void LateUpdate (){
		this.positionIncrease=this.lookAt.forward*this.fixedDist;
		transform.position=Vector3.Lerp(transform.position,this.lookAt.position-this.positionIncrease,25f*Time.deltaTime);
		transform.LookAt(this.lookAt);
	}
	float fixDistance(){
		RaycastHit hit;
		LayerMask layerMask=1 << 8 ;
		layerMask= ~layerMask;
		if(Physics.Raycast(this.lookAt.position,-this.lookAt.forward,out hit,this.distance,layerMask)){
			Debug.DrawLine(this.lookAt.position,hit.point,Color.red);
			return hit.distance;
		}
		return hit.distance;
	}
}
