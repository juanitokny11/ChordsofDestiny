using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notas : MonoBehaviour {

	// Use this for initialization
	void OnColisionEnter(Collider other){
		 if (other.tag == "Player")
        {
            Destroy(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
