using UnityEngine;
using System.Collections;

public class DeleteObjects: MonoBehaviour {

    public int vida = 2;
    public GameObject[] notas;
    public int random;
    private Vector3 inipos;
    public EnemyCache[] enemies;
    public bool spawn=false;

    private void Start()
    {
        //notas = new GameObject[4];
        inipos = transform.position;
        enemies= new EnemyCache[4];
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ligero" || other.tag =="pesado")
        {
            vida--;
            if (vida == 0){ 
            Explode();
            }
        }
    }
    private void Update()
    {
        random = Random.Range(0, notas.Length);
        if (spawn == true) { 
        for (int i = 0; i < 1; i++)
        {
            enemies[i] = new EnemyCache(notas[random], inipos, transform, Random.Range(1, 3));
            spawn = false;
        }
        }

    }

    void Explode() {

        spawn = true;
        //Instantiate(notas[random], inipos,Quaternion.identity,transform);
        Invoke("Destroy", 0.5f);
	}
    private void Destroy()
    {
        Destroy(gameObject);
    }

}
