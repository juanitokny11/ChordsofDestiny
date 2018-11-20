using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColisionsPlayer : MonoBehaviour
{
    private CharacterController colider;
    //public SoundPlayer sound;
private void Awake(){

        colider = GetComponent<CharacterController> ();
}
  public void OnTriggerEnter(Collider other)
    { 
        /*if(other.CompareTag("Enemy")){

            MyGameManager.getInstance().curHealth -=  MyGameManager.getInstance().Damage;
             MyGameManager.getInstance().HealthBar.fillAmount=  MyGameManager.getInstance().curHealth/  MyGameManager.getInstance().MaxHealth;
        }*/
         if (other.tag == "Enemy")
        {
             MyGameManager.getInstance().Daño(15f);
            //sound.Play(1, 1);
        }
        if (other.tag == "corchera")
        {

             MyGameManager.getInstance().AddMoney(1);
            MyGameManager.getInstance().notacogida=true;
            //sound.Play(1, 1);
        }
        if (other.tag == "negra")
        {
             MyGameManager.getInstance().AddMoney(5);
             MyGameManager.getInstance().notacogida=true;
            //sound.Play(1, 1);
        }
        if (other.tag == "blanca")
        {
             MyGameManager.getInstance().AddMoney(20);
             MyGameManager.getInstance().notacogida=true;
            //sound.Play(1, 1);
        }
        if (other.tag == "redonda")
        {
             MyGameManager.getInstance().AddMoney(40);
             MyGameManager.getInstance().notacogida=true;
            //sound.Play(1, 1);
        }
         if (other.tag == "Clavedesol")
        {
             MyGameManager.getInstance().CargaClave();
            //MyGameManager.getInstance().notacogida=true;
            //sound.Play(1, 1);
        }
    }
}
