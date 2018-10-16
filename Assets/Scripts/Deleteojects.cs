using UnityEngine;
using System.Collections;

public class Deleteojects: MonoBehaviour {

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
