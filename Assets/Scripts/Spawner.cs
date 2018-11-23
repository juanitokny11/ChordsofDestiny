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
    private GameObject randomTarget;
    public bool spawning=false;
    public  MyGameManager manager;
    //int randenemy;
    public float counter;
    public int numSpawns;
    private void Awake()
    {
        spawn = GameObject.FindGameObjectsWithTag("Spawn"); 
        randomTarget = spawn[Random.Range(0, spawn.Length)]; 
       
    }
    void Start(){
        spawning = true;
        //SetSpawn(2);
        
    }
    void Update () {
        //timeToSpawn = Random.Range(maxTimeSpawn, minTimeSpawn);
        counter += Time.deltaTime;
        Enemies[0].GetComponent<EnemyBehaviour>().pathNodes = points;
        if (manager.pause == true && spawning==true && counter>=5) { 
            Instantiate(Enemies[0], randomTarget.transform.position ,gameObject.transform.rotation);
            Spawn();
            counter = 0;
        }
    }
    public void SetSpawn(int numEnemies){
        numEnemies=numSpawns;
        spawning=true;
    }
    private void Spawn(){
        numSpawns--;
        if (numSpawns<=0){
            spawning=false;
            numSpawns=0;
        }
    }
}
