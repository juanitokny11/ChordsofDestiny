using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator anim;
    AnimatorStateInfo stateInfo;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnAnimatorMove()
    {
        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsTag("Run"))
        {
            anim.applyRootMotion = false;
        }
        else
        {
            anim.applyRootMotion = true;
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
    public void Death()
    {
        anim.SetTrigger("Death");
    }
    public void AirCombo1()
    {
        anim.SetTrigger("AirCombo1");
    }
    public void AirCombo2()
    {
        anim.SetTrigger("AirCombo2");
    }
    public void AirCombo3()
    {
        anim.SetTrigger("AirCombo3");
    }
    public void AirCombo4()
    {
        anim.SetTrigger("AirCombo4");
    }
    public void AirCombo5()
    {
        anim.SetTrigger("AirCombo5");
    }
    public void Solo()
    {
        anim.SetTrigger("");
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
}
