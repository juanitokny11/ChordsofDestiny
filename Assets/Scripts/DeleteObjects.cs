using UnityEngine;
using System.Collections;

public class DeleteObjects: MonoBehaviour {

    public int life;
    private void Start()
    {
        life = 3;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ligero" || other.tag == "pesado")
        {
            life--;
            if (life <= 0){
                Explode();
            }
        }
    }
    
    void Explode() {
		
		Destroy(gameObject);
	}
   
}
