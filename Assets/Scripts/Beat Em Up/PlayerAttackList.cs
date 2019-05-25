using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackList : MonoBehaviour
{
    public List<PlayerAttack2.ComboState> attacks;
    public PlayerMovementBeat player_Move;
    public CharacterAnimation player_anim;
    public Animator anim;
    public bool D3 = false;
    public bool Attack = true;
    public List<AudioSource> audios;
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
        if (Attack == false)
            player_Move.move = false;
        if (attacks.Count > 0)
        {
            DoAttacks();
        }
        if (attacks.Count <= 0 && Attack==true)
            player_Move.move = true;
    }
    void DoAttacks()
    {
        if (attacks[0] == PlayerAttack2.ComboState.DEBIL)
        {
            if (Attack)
            {
                player_anim.Debil();
                //audios[random].Play();
                Attack = false;
            }
        }
        else if (attacks[0] == PlayerAttack2.ComboState.DEBIL2)
        {
            if (Attack)
            {
                player_anim.Debil2();
                //audios[random].Play();
                Attack = false;
            }
        }
        else if (attacks[0] == PlayerAttack2.ComboState.DEBIL3)
        {
            if (Attack)
            {
                player_anim.Debil3();
                //audios[random].Play();
                D3 = true;
                Attack = false;
            }
        }
        else if (attacks[0] == PlayerAttack2.ComboState.FUERTE)
        {
            if (Attack)
            {
                player_anim.Fuerte();
                //audios[random2].Play();
                Attack = false;
            }
        }
        else if (attacks[0] == PlayerAttack2.ComboState.FUERTE2)
        {
            if (Attack)
            {
                player_anim.Fuerte2();
                //audios[random2].Play();
                Attack = false;
            }
        }
        else if (attacks[0] == PlayerAttack2.ComboState.FUERTE3)
        {
            if (Attack)
            {
                player_anim.Fuerte3();
                //audios[random2].Play();
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
    }
    public void CanAttack()
    {
        Attack = true;
        D3 = false;
        attacks.RemoveAt(0);
    }
    public void RemoveAllList()
    {
        attacks.RemoveRange(0,attacks.Count);
    }
}
