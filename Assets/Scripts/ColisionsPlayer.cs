﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColisionsPlayer : MonoBehaviour
{
    public CapsuleCollider colider;
    private Vector3 force;
    public SoundPlayer sound;

    private void Awake()
    {
        //force.x = 50;
    }
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
        if (other.tag == "Ammo")
        {
           // FPSInputManager.getInstance().totalammo += 30;
        }
        if (other.tag == "Rocket")
        {
         //   FPSInputManager.getInstance().totalrocket += 3;
        }
        if (other.tag == "Botiquin")
        {
            //FPSInputManager.getInstance().Heal(50);
        }
        if (other.tag == "Explosion")
        {
           // FPSInputManager.getInstance().Damage(10);
            sound.Play(1, 1);
            //body.AddExplosionForce(explosionForce, colider.transform.position, explosionRadius, 1, ForceMode.Impulse);
        }
        if (other.tag == "EnemyBullet")
        {
           // FPSInputManager.getInstance().Damage(7);
            sound.Play(1, 1);
            //body.AddExplosionForce(explosionForce, colider.transform.position, explosionRadius, 1, ForceMode.Impulse);
        }
    }

}
