using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public GameObject[] enemyprefab;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        //SpawnEnemy();
    }
    // Update is called once per frame
    public void SpawnEnemy()
    {
        Instantiate(enemyprefab[Random.Range(0, enemyprefab.Length)], transform.position, Quaternion.identity);
        Instantiate(enemyprefab[Random.Range(0, enemyprefab.Length)], transform.position, Quaternion.identity);
    }
}
