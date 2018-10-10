using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColisionsPlayer : MonoBehaviour
{
    public CapsuleCollider colider;
    public SoundPlayer sound;

  public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "corchera")
        {
            FPSInputManager.getInstance().AddMoney(5);
            //sound.Play(1, 1);
        }
        if (other.tag == "negra")
        {
            FPSInputManager.getInstance().AddMoney(10);
            //sound.Play(1, 1);
        }
        if (other.tag == "blanca")
        {
            FPSInputManager.getInstance().AddMoney(20);
            //sound.Play(1, 1);
        }
        if (other.tag == "redonda")
        {
            FPSInputManager.getInstance().AddMoney(40);
            //sound.Play(1, 1);
        }
    }

}
