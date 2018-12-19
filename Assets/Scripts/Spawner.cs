using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject[] Enemies;
    public Transform[] points;
    public GameObject[] spawn;
    public EnemyCache[] enemyCache;
    public GameObject cache;
    public float timeToSpawn;
    public float maxTimeSpawn;
    public float minTimeSpawn;
    //private GameObject randomTarget;
    public bool spawning=false;
    private  MyGameManager manager;
    //int randenemy;
    public float counter;
    public int numSpawns;
    public int enemyCounter;
   

    private BossBehaviour boss;
    private void Awake()
    {
        spawn = GameObject.FindGameObjectsWithTag("Spawn"); 
        //randomTarget = spawn[0]; 
        boss=FindObjectOfType<BossBehaviour>();
        manager = FindObjectOfType<MyGameManager>();
    }
    void Start(){
        enemyCache = new EnemyCache[Enemies.Length];
    }
    void Update () {
        //timeToSpawn = Random.Range(maxTimeSpawn, minTimeSpawn);
        counter += Time.deltaTime;
        Enemies[0].GetComponent<EnemyBehaviour>().pathNodes = points;
        if (manager.pause == true && counter>=10 && spawning==true) { 
            if(enemyCounter<=15 && numSpawns>0){
                for (int i = 0; i < enemyCache.Length; i++)
                {
                    enemyCache[i] = new EnemyCache(Enemies[0], spawn[0].transform.position, cache.transform, Random.Range(1, 3));
                }
                enemyCache[Random.Range(0, enemyCache.Length)].GetEnemy();
                enemyCounter +=2;
            Spawn();
            }

            //counter = 0;
        }
    }
    public void SetSpawn(int numEnemies){
        numSpawns=1;
        spawning=true;
    }
    private void Spawn(){
        if(enemyCounter<=15){
        numSpawns--;
        if (numSpawns<=0){
            spawning=false;
            numSpawns=0;
            counter=0;
        }
     }
    }
}

