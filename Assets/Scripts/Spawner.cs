using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject[] Enemies;
    public Transform[] points;
    public GameObject[] spawn;
    public float timeToSpawn;
    public float maxTimeSpawn;
    public float minTimeSpawn;
    public float startWait;
    private GameObject randomTarget;
    public bool spawning=false;
    public  MyGameManager manager;
    int randenemy;
    public int numSpawns;
    private void Awake()
    {
        spawn = GameObject.FindGameObjectsWithTag("Spawn"); 
        randomTarget = spawn[Random.Range(0, spawn.Length)]; 
       
    }
    void Start(){
         SetSpawn(3);
    }
    void Update () {
        timeToSpawn = Random.Range(maxTimeSpawn, minTimeSpawn);
         Spawn();
        if (manager.pause == true && spawning==true) { 
            Instantiate(Enemies[randenemy], randomTarget.transform.position ,gameObject.transform.rotation);
           }
    }
    public void SetSpawn(int numEnemies){
        numEnemies=numSpawns;
        spawning=true;
        Enemies[0].GetComponent<EnemyBehaviour>().pathNodes = points;
        
    }
    private void Spawn(){
        numSpawns--;
        if (numSpawns<=0){
            spawning=false;
            numSpawns=0;
        }
    }
}
