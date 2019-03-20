﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2 : MonoBehaviour
{
    public List<ComboState> attacks;
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
        AIRCOMBO1,
        AIRCOMBO2,
        AIRCOMBO3,
        AIRCOMBO4,
        AIRCOMBO5,
        SOLO
    }
    public CharacterAnimation player_Anim;
    private PlayerMovementBeat player_Move;
    private bool activateTimerToReset;
    public bool is_Player;
    private float default_Combo_Timer = 0.95f;
    private float current_Combo_Timer;

    private ComboState current_Combo_State;

    // Start is called before the first frame update
    void Awake()
    {
        player_Anim = GetComponent<CharacterAnimation>();
        player_Move = GetComponent<PlayerMovementBeat>();
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
            if (current_Combo_State == ComboState.DEBIL3 || current_Combo_State == ComboState.FUERTE2 || current_Combo_State == ComboState.FUERTE3 || current_Combo_State == ComboState.GUARD || current_Combo_State == ComboState.SOLO)
                return;
            if (current_Combo_State == ComboState.FUERTE)
                current_Combo_State = ComboState.DEBIL;
            current_Combo_State++;
            activateTimerToReset = true;
            current_Combo_Timer = default_Combo_Timer;
            if (!player_Move.inAir) { 
                if (current_Combo_State == ComboState.DEBIL)
                {
                    //player_Anim.Debil();
                    AddToTheList(ComboState.DEBIL);
                }
                if (current_Combo_State == ComboState.DEBIL2)
                {
                    //player_Anim.Debil2();
                    AddToTheList(ComboState.DEBIL2);
                }
                if (current_Combo_State == ComboState.DEBIL3)
                {
                    //player_Anim.Debil3();
                    AddToTheList(ComboState.DEBIL3);
                }
            }
            else if (player_Move.inAir)
            {
                player_Move.comboAereo = true;
                if (current_Combo_State == ComboState.AIRCOMBO1)
                {
                    AddToTheList(ComboState.AIRCOMBO1);
                }
                if (current_Combo_State == ComboState.AIRCOMBO2)
                {
                    AddToTheList(ComboState.AIRCOMBO2);
                }
                if (current_Combo_State == ComboState.AIRCOMBO3)
                {
                    AddToTheList(ComboState.AIRCOMBO3);
                }
                if (current_Combo_State == ComboState.AIRCOMBO4)
                {
                    AddToTheList(ComboState.AIRCOMBO4);
                }
                if (current_Combo_State == ComboState.AIRCOMBO5)
                {
                    AddToTheList(ComboState.AIRCOMBO5);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (current_Combo_State == ComboState.FUERTE3 || current_Combo_State == ComboState.DEBIL3 || current_Combo_State == ComboState.GUARD || current_Combo_State == ComboState.SOLO)
                return;
            if (current_Combo_State == ComboState.NONE)
                current_Combo_State = ComboState.FUERTE;
            else if (current_Combo_State == ComboState.FUERTE || current_Combo_State == ComboState.FUERTE2)
                current_Combo_State++;
            else if (current_Combo_State == ComboState.DEBIL || current_Combo_State == ComboState.DEBIL2)
                current_Combo_State = ComboState.FUERTE2;
            activateTimerToReset = true;
            current_Combo_Timer = default_Combo_Timer;
            if (current_Combo_State == ComboState.FUERTE)
            {
                //player_Anim.Fuerte();
                AddToTheList(ComboState.FUERTE);
            }
            if (current_Combo_State == ComboState.FUERTE2)
            {
                //player_Anim.Fuerte2();
                AddToTheList(ComboState.FUERTE2);
            }
            if (current_Combo_State == ComboState.FUERTE3)
            {
                //player_Anim.Fuerte3();
                AddToTheList(ComboState.FUERTE3);
            }
            if (player_Move.inAir == true)
            {
                player_Move.comboAereo = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            current_Combo_State = ComboState.SOLO;
            if (current_Combo_State == ComboState.SOLO && !player_Move.inAir)
            {
                player_Anim.Solo();
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            current_Combo_State = ComboState.GUARD;
            if (current_Combo_State == ComboState.GUARD)
                player_Anim.Block();
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            current_Combo_State = ComboState.GUARD;
            if (current_Combo_State == ComboState.GUARD)
                player_Anim.ResetBlock();
        }
    }
    void ResetComboState()
    {
        if (activateTimerToReset)
        {
            current_Combo_Timer -= Time.deltaTime;
            if (current_Combo_Timer <= 0f)
            {
                current_Combo_State = ComboState.NONE;

                activateTimerToReset = false;
                current_Combo_Timer = default_Combo_Timer;
            }
        }
    }
    public void AddToTheList(ComboState state)
    {
        attacks.Add(state);
    }
}
