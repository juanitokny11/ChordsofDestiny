using UnityEngine;
using System.Collections;

public class DeleteObjects: MonoBehaviour {

    public int vida = 3;
    public GameObject[] notas;
    public int random;
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
    private void Update()
    {
        random = Random.Range(0, notas.Length);
    }

    void Explode() {

        Instantiate(notas[random], inipos,Quaternion.identity,transform);
        Destroy(gameObject);
	}
   
}
