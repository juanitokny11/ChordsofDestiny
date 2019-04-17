using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeControler : MonoBehaviour
{
    public List<GameObject> enemiesLifes;
    private GameObject[] enemieslife2;
    
    void Update()
    {
        enemieslife2 = GameObject.FindGameObjectsWithTag("HealthUI");
        for (int i = 0; i < enemieslife2.Length; i++)
        {
            enemiesLifes.Add(enemieslife2[i]);
            enemiesLifes[i].SetActive(false);
        } 
    }
}
