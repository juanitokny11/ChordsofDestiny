using System.Collections;
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
        FUERTE3
    }
    private CharacterAnimation player_Anim;
    private bool activateTimerToReset;

    private float default_Combo_Timer=0.4f;
    private float current_Combo_Timer;

    private ComboState current_Combo_State;

    // Start is called before the first frame update
    void Awake()
    {
        player_Anim = GetComponentInChildren<CharacterAnimation>();
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (current_Combo_State == ComboState.DEBIL3 || current_Combo_State == ComboState.FUERTE || current_Combo_State == ComboState.FUERTE2 || current_Combo_State == ComboState.FUERTE3)
                return;

            current_Combo_State++;
            activateTimerToReset = true;
            current_Combo_Timer = default_Combo_Timer;

            if(current_Combo_State == ComboState.DEBIL)
                player_Anim.Debil();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (current_Combo_State == ComboState.FUERTE3 || current_Combo_State == ComboState.DEBIL3)
                return;
            if (current_Combo_State == ComboState.NONE || current_Combo_State == ComboState.DEBIL || current_Combo_State == ComboState.DEBIL2)
                current_Combo_State = ComboState.FUERTE;
            else if (current_Combo_State == ComboState.FUERTE)
                current_Combo_State++;

            activateTimerToReset = true;
            current_Combo_Timer = default_Combo_Timer;

            if (current_Combo_State==ComboState.FUERTE)
                player_Anim.Fuerte();

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
