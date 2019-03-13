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
