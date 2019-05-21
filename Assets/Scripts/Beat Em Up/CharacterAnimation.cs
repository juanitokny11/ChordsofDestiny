using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator anim;
    AnimatorStateInfo stateInfo;
    public bool is_Groupie;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnAnimatorMove()
    {
        if (is_Groupie)
        {
            if (stateInfo.IsName("Atack") || stateInfo.IsName("atack2"))
            {
                anim.applyRootMotion = true;
            }
            else
            {
                anim.applyRootMotion = false;
            }
        }
    }
    public void Run(bool Run)
    {
        anim.SetBool("Run", Run);
    }
    public void Walk(bool walk)
    {
        anim.SetBool("Walk", walk);
    }
    public void Fuerte()
    {
        anim.SetTrigger("Fuerte");
    }
    public void Jump()
    {
        anim.SetTrigger("Jump");
    }
    public void ResetJump()
    {
        anim.SetTrigger("ResetJump");
    }
    public void Debil()
    {
        anim.SetTrigger("Debil");
    }
    public void Fuerte2()
    {
        anim.SetTrigger("Fuerte2");
    }
    public void Debil2()
    {
        anim.SetTrigger("Debil2");
    }
    public void Fuerte3()
    {
        anim.SetTrigger("Fuerte3");
    }
    public void Debil3()
    {
        anim.SetTrigger("Debil3");
    }
    public void Death(int death)
    {
        if(death==0)
        anim.SetTrigger("Death");
        if (death == 1)
            anim.SetTrigger("SoloDeath");
    }
    public void AirCombo1()
    {
        anim.SetTrigger("AirCombo1");
    }
    public void Solo()
    {
        anim.SetTrigger("Solo");
    }
    public void Block()
    {
        anim.SetTrigger("Block");
    }
    public void ResetBlock()
    {
        anim.SetTrigger("ResetBlock");
    }
    public void Hit(int hit)
    {
        if (hit == 0)
            anim.SetTrigger("Hit");
        if (hit == 1)
            anim.SetTrigger("Hit2");
    }
    //Enemy Animations
    public void Disolve()
    {
        anim.SetTrigger("Disolve");
    }
    public void Stuned()
    {
        anim.SetTrigger("Stuned");
    }
    public void EnemyAttack(int attack)
    {
        if (attack == 0)
            anim.SetTrigger("Attack");
        if (attack == 1)
            anim.SetTrigger("Attack2");
    }
    public void PlayIdleAnimation()
    {
        anim.Play("idlebase");
    }
    public void PlayLongIdle()
    {
        anim.SetTrigger("LongIdle");
    }
    public void KnockDown()
    {
        anim.SetTrigger("KnockUp");
    }
    public void Tirar()
    {
        anim.SetTrigger("Tirar");
    }
    public void StandUp()
    {
        anim.SetTrigger("StandUp");
    }
    public void SoloHit()
    {
        anim.SetTrigger("SoloHit");
    }
    //Boss Amin
    public void Walk1arm(bool walk1arm)
    {
        anim.SetBool("Walk1arm", walk1arm);
    }
    public void Walk2arm(bool walk2arm)
    {
        anim.SetBool("Walk2arms", walk2arm);
    }
    public void Attack1arm(int attack)
    {
        if (attack == 0)
            anim.SetTrigger("Attack_1arm");
        if (attack == 1)
            anim.SetTrigger("Attack2_1arm");
    }
    public void Attack2arms(int attack)
    {
        if (attack == 0)
            anim.SetTrigger("Attack_2arms");
        if (attack == 1)
            anim.SetTrigger("Attack2_2arms");
    }
    public void Hit1arm(int hit)
    {
        if (hit == 0)
            anim.SetTrigger("Hit_1arm");
        if (hit == 1)
            anim.SetTrigger("Hit2_1arm");
    }
    public void Hit2arms(int hit)
    {
        if (hit == 0)
            anim.SetTrigger("Hit_2arms");
        if (hit == 1)
            anim.SetTrigger("Hit2_2arms");
    }
    public void Jump1Arm()
    {
        anim.SetTrigger("Jump_2arms");
    }
    public void ResetJump2Arms()
    {
        anim.SetTrigger("ResetJump_2arms");
    }
    public void ResetJump1Arm()
    {
        anim.SetTrigger("ResetJump_1arm");
    }
    public void Jump2Arms()
    {
        anim.SetTrigger("Jump_2arms");
    }
    public void RomperEspada()
    {
        anim.SetTrigger("ChangeFase");
    }
    public void Invoke1arm()
    {
        anim.SetTrigger("Invoke_1arm");
    }
    public void Invoke2arms()
    {
        anim.SetTrigger("Invoke_2arms");
    }

}
