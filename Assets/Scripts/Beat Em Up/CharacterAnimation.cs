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

}
