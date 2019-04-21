using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeControler : MonoBehaviour
{
    public List<GameObject> enemiesLifes;
    public AttackUniversal playerAttack;

    public void ShowDamagedUI(GameObject EnemyLife)
    {
        EnemyLife.SetActive(true);
    }
}
