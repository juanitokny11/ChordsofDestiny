using UnityEngine;
using System.Collections;

public class DeleteObjects: MonoBehaviour {

    public int vida = 2;
    private Vector3 inipos;

    private void Start()
    {
        //notas = new GameObject[4];
        inipos = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ligero" || other.tag =="pesado")
        {
            vida--;
            if (vida <= 0){ 
            Explode();
            }
        }
    }
     public void Explode()
    {
        Invoke("Destroy", 0.5f);
	}
    public void Destroy()
    {
        Destroy(gameObject.transform.parent);
    }
}
