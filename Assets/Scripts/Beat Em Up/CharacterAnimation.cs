using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Run(bool Run)
    {
        anim.SetBool("Run", Run);
    }
    public void Fuerte()
    {
        anim.SetTrigger("Fuerte");
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
    public void StandUp()
    {
        anim.SetTrigger("");

    }
    public void Death()
    {
        anim.SetTrigger("Death");
    }
    public void Hit()
    {
        anim.SetTrigger("");
    }
}
