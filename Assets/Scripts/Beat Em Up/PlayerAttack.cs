﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public enum ComboState
    {
        NONE,
        DEBIL,
        DEBIL2,
        DEBIL3,
        FUERTE,
        FUERTE2,
        FUERTE3,
        GUARD,
        AIRCOMBO
    }
    private CharacterAnimation player_Anim;
    private bool activateTimerToReset;

    private float default_Combo_Timer=0.95f;
    private float current_Combo_Timer;

    private ComboState current_Combo_State;

    // Start is called before the first frame update
    void Awake()
    {
        player_Anim = GetComponent<CharacterAnimation>();
    }
    void Start()
    {
        current_Combo_Timer = default_Combo_Timer;
        current_Combo_State = ComboState.NONE;
    }
    void Update()
    {
        ComboAttacks();
        ResetComboState();
    }
    void ComboAttacks()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (current_Combo_State == ComboState.DEBIL3 || current_Combo_State == ComboState.FUERTE2 || current_Combo_State == ComboState.FUERTE3 || current_Combo_State ==ComboState.GUARD)
                return;
            if (current_Combo_State == ComboState.FUERTE)
                current_Combo_State = ComboState.DEBIL;
            current_Combo_State++;
            activateTimerToReset = true;
            current_Combo_Timer = default_Combo_Timer;
            if(current_Combo_State == ComboState.DEBIL)
                player_Anim.Debil();
            if (current_Combo_State == ComboState.DEBIL2)
                player_Anim.Debil2();
            if (current_Combo_State == ComboState.DEBIL3)
                player_Anim.Debil3();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if ( current_Combo_State == ComboState.FUERTE3 || current_Combo_State == ComboState.DEBIL3 || current_Combo_State == ComboState.GUARD)
                return;
            if (current_Combo_State == ComboState.NONE)
                current_Combo_State = ComboState.FUERTE;
            else if (current_Combo_State == ComboState.FUERTE || current_Combo_State == ComboState.FUERTE2)
                current_Combo_State++;
            else if (current_Combo_State == ComboState.DEBIL || current_Combo_State == ComboState.DEBIL2)
                current_Combo_State = ComboState.FUERTE2;
            activateTimerToReset = true;
            current_Combo_Timer = default_Combo_Timer;
            if (current_Combo_State==ComboState.FUERTE)
                player_Anim.Fuerte();
            if (current_Combo_State == ComboState.FUERTE2)
                player_Anim.Fuerte2();
            if (current_Combo_State == ComboState.FUERTE3)
                player_Anim.Fuerte3();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {

        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            current_Combo_State = ComboState.GUARD;
            if (current_Combo_State == ComboState.GUARD)
                player_Anim.Block();
        }
    }
    void ResetComboState()
    {
        if (activateTimerToReset)
        {
            current_Combo_Timer -= Time.deltaTime;
            if(current_Combo_Timer <= 0f)
            {
                current_Combo_State = ComboState.NONE;

                activateTimerToReset = false;
                current_Combo_Timer = default_Combo_Timer;
            }
        }
    }
}
