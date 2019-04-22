﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackList : MonoBehaviour
{
    public List<PlayerAttack2.ComboState> attacks;
    public PlayerMovementBeat player_Move;
    public CharacterAnimation player_anim;
    public Animator anim;
    public bool Attack = true;
  
    void Start()
    {
        player_Move = GetComponent<PlayerMovementBeat>();
        attacks = GetComponent<PlayerAttack2>().attacks;
        player_anim = GetComponent<CharacterAnimation>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attacks.Count>0)
            DoAttacks();
    }
    void DoAttacks()
    {
        if (attacks[0] == PlayerAttack2.ComboState.DEBIL)
        {
            if (Attack)
            {
                player_anim.Debil();
                player_Move.move = false;
                Attack = false;
            }
        }
        else if (attacks[0] == PlayerAttack2.ComboState.DEBIL2)
        {
            if (Attack)
            {
                player_anim.Debil2();
                player_Move.move = false;
                Attack = false;
            }
        }
        else if (attacks[0] == PlayerAttack2.ComboState.DEBIL3)
        {
            if (Attack)
            {
                player_anim.Debil3();
                player_Move.move = false;
                Attack = false;
            }
        }
        else if (attacks[0] == PlayerAttack2.ComboState.FUERTE)
        {
            if (Attack)
            {
                player_anim.Fuerte();
                player_Move.move = false;
                Attack = false;
            }
        }
        else if (attacks[0] == PlayerAttack2.ComboState.FUERTE2)
        {
            if (Attack)
            {
                player_anim.Fuerte2();
                player_Move.move = false;
                Attack = false;
            }
        }
        else if (attacks[0] == PlayerAttack2.ComboState.FUERTE3)
        {
            if (Attack)
            {
                player_anim.Fuerte3();
                player_Move.move = false;
                Attack = false;
            }
        }
        else if(attacks[0] == PlayerAttack2.ComboState.AIRCOMBO1)
        {
            if (Attack)
            {
                player_anim.AirCombo1();
                player_Move.move = false;
                Attack = false;
            }
        }
    }
    public void CanAttack()
    {
        Attack = true;
        player_Move.move = true;
        attacks.RemoveAt(0);
    }
    public void RemoveAllList()
    {
        attacks.RemoveRange(0,attacks.Count);
    }
}
