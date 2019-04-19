using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeControler : MonoBehaviour
{
    public List<GameObject> enemiesLifes;
    public AttackUniversal playerAttack;

    public void ShowDamagedUI(GameObject enemylife)
    {
        enemylife = playerAttack.hit[0].gameObject.GetComponent<EnemyMovement>().enemyLife;
        /*if(enemylife!=currentLife)
            enemylife = currentLife;*/
        enemylife.SetActive(true);
        Debug.Log(enemylife);
    }
}
