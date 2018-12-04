using UnityEngine;
using System.Collections;

public class DeleteObjects: MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Explode();
        }
    }
    
    void Explode() {
		
		Destroy(gameObject);
	}
   
}
