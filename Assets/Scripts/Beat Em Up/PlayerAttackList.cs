using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackList : MonoBehaviour
{
    public List<PlayerAttack2.ComboState> attacks;
    public CharacterAnimation player_anim;
    public Animator anim;
    public bool Attack = true;
  
    void Start()
    {
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
                Attack = false;
            }
        }
        else if (attacks[0] == PlayerAttack2.ComboState.DEBIL2)
        {
            if (Attack)
            {
                player_anim.Debil2();
                Attack = false;
            }
        }
        else if (attacks[0] == PlayerAttack2.ComboState.DEBIL3)
        {
            if (Attack)
            {
                player_anim.Debil3();
                Attack = false;
            }
        }
        else if (attacks[0] == PlayerAttack2.ComboState.FUERTE)
        {
            if (Attack)
            {
                player_anim.Fuerte();
                Attack = false;
            }
        }
        else if (attacks[0] == PlayerAttack2.ComboState.FUERTE2)
        {
            if (Attack)
            {
                player_anim.Fuerte2();
                Attack = false;
            }
        }
        else if (attacks[0] == PlayerAttack2.ComboState.FUERTE3)
        {
            if (Attack)
            {
                player_anim.Fuerte3();
                Attack = false;
            }
        }
        else if(attacks[0] == PlayerAttack2.ComboState.AIRCOMBO1)
        {
            if (Attack)
            {
                player_anim.AirCombo1();
                Attack = false;
            }
        }
        else if(attacks[0] == PlayerAttack2.ComboState.AIRCOMBO2)
        {
            if (Attack)
            {
                player_anim.AirCombo2();
                Attack = false;
            }
        }
        else if(attacks[0] == PlayerAttack2.ComboState.AIRCOMBO3)
        {
            if (Attack)
            {
                player_anim.AirCombo3();
                Attack = false;
            }
        }
        else if(attacks[0] == PlayerAttack2.ComboState.AIRCOMBO4)
        {
            if (Attack)
            {
                player_anim.AirCombo4();
                Attack = false;
            }
        }
        else if(attacks[0] == PlayerAttack2.ComboState.AIRCOMBO5)
        {
            if (Attack)
            {
                player_anim.AirCombo5();
                Attack = false;
            }
        }
    }
    private void CanAttack()
    {
        Attack = true;
        attacks.RemoveAt(0);
    }
    private void RemoveAllList()
    {
        attacks.RemoveRange(0,attacks.Count);
    }
}
