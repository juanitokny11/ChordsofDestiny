using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float health = 100f;

    private CharacterAnimation animationScript;
    private EnemyMovement enemyMovement;
    private HealthUI health_UI;
    private EnemyHealthUI enemy_Health_UI;
    public bool characterDied;
    public bool inAir=false;

    public bool is_Player;
    private void Awake()
    {
        animationScript = GetComponent<CharacterAnimation>();
        enemyMovement = GetComponent<EnemyMovement>();
        if (is_Player)
            health_UI = GetComponent<HealthUI>();
        else if (!is_Player)
            enemy_Health_UI = GetComponent<EnemyHealthUI>();

    }
    public void ApplyDamage(float damage,bool knockDown)
    {
        if (characterDied)
            return;
        health -= damage;
        if(is_Player)
            health_UI.DisplayHealth(health);
        else if(!is_Player)
            enemy_Health_UI.DisplayHealth(health);
        if (health <= 0)
        {
            animationScript.Death();
            characterDied = true;
            if (is_Player)
            {
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyMovement>().enabled = false;
            }
            return;
        }
        if (!is_Player)
        {
            if (knockDown)
            {
                if (inAir)
                    animationScript.Tirar();
                else
                    animationScript.KnockDown();
            }
            else if (!knockDown)
            {
                    animationScript.Hit(Random.Range(0, 3));
            }
        }
    }
}
