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
    public bool stop;
    public  MyGameManager manager;
    int randenemy;
    private void Awake()
    {
        spawn = GameObject.FindGameObjectsWithTag("Spawn");

    }
    void Update () {
        timeToSpawn = Random.Range(maxTimeSpawn, minTimeSpawn);
        
    }
    IEnumerator Start()
    {
        
            yield return new WaitForSeconds(startWait);
        while (!stop)
        {
			randenemy = 0;// Random.Range(0, 2);
            randomTarget = spawn[Random.Range(0, spawn.Length)];
            if (manager.pause == true) { 
            Instantiate(Enemies[randenemy], randomTarget.transform.position ,gameObject.transform.rotation);
           }
            
        Enemies[0].GetComponent<EnemyBehaviour>().pathNodes = points;
           
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
}
