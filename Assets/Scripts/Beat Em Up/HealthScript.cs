﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float health = 100f;

    private CharacterAnimation animationScript;
    private EnemyMovement enemyMovement;

    private bool characterDied;

    public bool is_Player;
    private void Awake()
    {
        animationScript = GetComponent<CharacterAnimation>();
        enemyMovement = GetComponent<EnemyMovement>();
    }
    public void ApplyDamage(float damage,bool knockDown)
    {
        if (characterDied)
            return;
        health -= damage;

        if (health <= 0)
        {
            animationScript.Death();
            characterDied = true;
            if (is_Player)
            {

            }
            return;
        }
        if (!is_Player)
        {
            if (knockDown)
            {

                    animationScript.KnockDown(); 
            }
            else if (!knockDown)
            {
                    animationScript.Hit(Random.Range(0, 3));
            }
            
        }
    }
}
