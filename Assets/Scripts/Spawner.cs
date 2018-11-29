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
    public int enemyCounter;
    private EnemyCache[] enemyCache;
    public float tiempolanzar = 5.0f;
	private float tiempoactual;
	public Transform enemyspawn;
	public Transform enemyLaunchposition;
	public int maxenemy;
    private Vector3 creationPosEnemy;

    private BossBehaviour boss;
    private void Awake()
    {
        spawn = GameObject.FindGameObjectsWithTag("Spawn"); 
        randomTarget = spawn[Random.Range(0, spawn.Length)]; 
        boss=FindObjectOfType<BossBehaviour>();
        creationPosEnemy = enemyspawn.position;

        enemyCache = new EnemyCache[Enemies.Length];
		for (int i = 0; i < enemyCache.Length; i++) {
            enemyCache[i]= new EnemyCache(Enemies[i], creationPosEnemy, enemyLaunchposition, maxenemy);
            creationPosEnemy.y += 1;
		}
       
    }
    void Start(){
        spawning = true;
    }
    void Update () {
        //timeToSpawn = Random.Range(maxTimeSpawn, minTimeSpawn);
        counter += Time.deltaTime;
        Enemies[0].GetComponent<EnemyBehaviour>().pathNodes = points;
        if (manager.pause == true && counter>=10 && spawning==true) { 
            if(enemyCounter<=15 && numSpawns>0){
                tiempoactual += Time.deltaTime;
            Vector3 launch = enemyLaunchposition.position;
            if (tiempoactual > tiempolanzar) {
                enemyCache[Random.Range(0, enemyCache.Length)].GetEnemy();
				tiempoactual-=tiempolanzar;
		    }
            Instantiate(Enemies[0], randomTarget.transform.position ,gameObject.transform.rotation);
            Instantiate(Enemies[0], randomTarget.transform.position ,gameObject.transform.rotation);
            enemyCounter+=2;
            Spawn();
            }

            //counter = 0;
        }
    }
    public void SetSpawn(int numEnemies){
        if(enemyCounter<=15){
        numSpawns=1;
        spawning=true;
        }
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
	public void LaunchMeteor(int type,Vector3 position,Vector2 direction,float rotation){
        enemyCache[type].GetEnemy();
	}

}

