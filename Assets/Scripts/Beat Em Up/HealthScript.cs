﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float health = 100f;
    public float solo = 0f;
    public BattleZone zone;
    private CharacterAnimation animationScript;
    private EnemyMovement enemyMovement;
    private HealthUI health_UI;
    private BossIA bossIA;
    private EnemyHealthUI enemy_Health_UI;
    public PlayerAttackList playerAttack_List;
    public bool canDoSolo = false;
    public bool characterDied;
    public bool inAir=false;
    public int hitCounter;

    public bool is_Player,is_Boss;
    private void Awake()
    {
        animationScript = GetComponent<CharacterAnimation>();
        enemyMovement = GetComponent<EnemyMovement>();
        if (is_Player)
        {
            health_UI = GetComponent<HealthUI>();
            playerAttack_List = GetComponent<PlayerAttackList>();
        }
        else if (!is_Player)
            enemy_Health_UI = GetComponent<EnemyHealthUI>();
        if (is_Boss)
        {
            bossIA = GetComponent<BossIA>();
            characterDied = false;
        }  
    }
    private void Update()
    {
        if (solo >= 200)
        {
            if (is_Player)
            {
                canDoSolo = true;
            }
        }
        else
        {
            canDoSolo = false;
        }
    }
    public void ApplyDamage(float damage,bool knockDown)
    {
        if (characterDied)
            return;
        health -= damage;
        if (is_Player)
        { 
            health_UI.DisplayHealth(health);
            if (!knockDown)
            {
                if (Random.Range(0, 3) > 1)
                {
                    animationScript.Hit(Random.Range(0, 3));
                    playerAttack_List.CanAttack();
                }
            }
        }
        else if(!is_Player)
            enemy_Health_UI.DisplayHealth(health);
        
        if (health <= 0)
        {
            animationScript.Death();
            if (is_Player)
            {
                if(!is_Boss)
                    GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyMovement>().enabled = false;
                else if(is_Boss)
                    GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossIA>().enabled = false;
                animationScript.Death();
            }
            else if(!is_Boss && !is_Player)
            {
               zone.enemiescounter--;
            }
            else if (is_Boss)
            {
                bossIA.Death();
            }
            characterDied = true;
            return;
        }
        if (!is_Boss && !is_Player)
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
                if (Random.Range(0, 3) > 1)
                    animationScript.Hit(Random.Range(0, 3));
            }

        }
        if (is_Boss)
        {
            if (!knockDown)
            {
                hitCounter++;
                if (Random.Range(0, 3) > 1) {
                    if (bossIA.fase == 1)
                    {
                        animationScript.Hit2arms(Random.Range(0, 3));    
                    }
                    else
                    {
                        animationScript.Hit1arm(Random.Range(0, 3));
                    }
                }
            }
            if(hitCounter>=bossIA.porcentajeInvocar)
            {
                bossIA.SetInvoke();
                hitCounter = 0;
            }

        }
    }
}
