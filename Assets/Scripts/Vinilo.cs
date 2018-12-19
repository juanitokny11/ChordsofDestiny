using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vinilo : MonoBehaviour {

	public GameObject pers;
	public float counter;
	void Start(){
		pers=GameObject.FindWithTag("Player");
	}
	void Update () {	   
 		this.GetComponent<Rigidbody>().AddForce(pers.transform.forward * 100);
		counter++;
		if (counter>=100){
			Invoke("Destroy", 0.1f);
		}
	}
	void OnTriggerEnter(Collider other){
		if(other.tag == "Enemy"){
			 Invoke("Destroy", 0.1f);
		}
	}
	 private void Destroy()
    {
        Destroy(gameObject);
    }
}
